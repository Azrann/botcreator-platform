using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.Configuration;
using Core.Common.Extensions;
using Mentor.telegram.bot.api.services.Infrastructure;
using Mentor.telegram.bot.api.Services.UserInterface;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Triggers;
using Mentor.telegram.bot.api.services.Services.Configuration;
using Mentor.telegram.paypalpayments;
using Mentor.telegram.paypalpayments.Models;
using Mentor.telegram.shared.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Message = Telegram.Bot.Types.Message;
using Update = Telegram.Bot.Types.Update;

namespace Mentor.telegram.bot.api.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;
        private readonly IFunnelStorageService _funnelStorageService;
        private readonly ITagStorageService _tagStorageService;
        private readonly IPaymentInfoService _paymentInfoService;
        private readonly IStateStorageService _stateStorageService;
        private readonly IConfigurationCommon _commonConfig;
        private readonly IPaypalApiStorageService _paypalStorageService;
        private readonly IBotStorageService _botStorageService;
        private readonly IWidgetConfigStorageService _widgetConfigStorageService;

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(UpdateService));

        public UpdateService(IBotService botService,
            IFunnelStorageService funnelStorageService,
            ITagStorageService tagStorageService,
            IPaymentInfoService paymentInfoService,
            IStateStorageService stateStorageService,
            IConfigurationCommon commonConfig,
            IPaypalApiStorageService paypalStorageService,
            IBotStorageService botStorageService,
            IWidgetConfigStorageService widgetConfigStorageService)
        {
            _commonConfig = commonConfig;
            _botService = botService;
            _funnelStorageService = funnelStorageService;
            _tagStorageService = tagStorageService;
            _paymentInfoService = paymentInfoService;
            _stateStorageService = stateStorageService;
            _paypalStorageService = paypalStorageService;
            _botStorageService = botStorageService;
            _widgetConfigStorageService = widgetConfigStorageService;
        }
        
        public async Task ProcessAsync(string botId, Update update, Guid correlationId)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await ProcessCallback(botId, update.CallbackQuery, correlationId);
            }

            if (update.Type == UpdateType.Message)
            {
                await ProcessMessage(botId, update.Message, correlationId);
            }
        }

        public async Task ProcessPaymentAsync(string botId, EventInfo eventInfo, Guid correlationId)
        {
            Log.LogInf($"UpdateService: Received payment event id = {eventInfo.PaymentInfo.TransactionId}", correlationId);

            var paymentInfo = _paymentInfoService.GetPaymentInfoByTransactionId(eventInfo.PaymentInfo.TransactionId);

            if (paymentInfo == null)
            {
                Log.LogErr($"UpdateService: Transaction not found id = {eventInfo.PaymentInfo.TransactionId}", correlationId);
                return;
            }

            if (paymentInfo.PaymentStatus == eventInfo.PaymentInfo.PaymentStatus &&
                paymentInfo.Fee == eventInfo.PaymentInfo.Fee)
            {
                Log.LogInf($"UpdateService: Transaction event already processed id = {eventInfo.PaymentInfo.TransactionId}", correlationId);
                return;
            }

            _paymentInfoService.UpdatePaymentInfo(eventInfo.PaymentInfo);

            var properties = AddPropertiesEvent(botId, eventInfo);
            var tags = _tagStorageService.GetTagsById(botId, paymentInfo.UserId);
            var states = _stateStorageService.GetStateById(botId, paymentInfo.UserId);

            var data = new Data()
            {
                Properties = properties,
                Tags = tags.Tags,
                State = states.States
            };
            var bot = _botStorageService.GetById(botId);
            var parameters = AddParametersPayment(botId, eventInfo, paymentInfo, bot.AdminId.ToString(), correlationId);
            var funnel = _funnelStorageService.GetById(bot.GetFirstFunnelId(data).ToString());

            // TODO Data object es informacion para los triggers
            // Si necesitamos triggers que comprueben el status entonces tendremos que
            // pasar el status en Data.Properties

            // TODO Parameters es informacion para los actions
            // Necesitamos UserId, BotId, y demas en "parameters".

            await funnel.ProcessEvents(data, parameters, ServiceProviderStatic.Instance, correlationId);

            return;
        }

        public async Task ProcessPaypalExecution(string paymentId, string paypalPayerId, Guid correlationId)
        {
            var paymentInfo = _paymentInfoService.GetPaymentInfoById(paymentId);
            var paypalStorageCredentials = _paypalStorageService.GetPaypalApiDataById(paymentInfo.BotId);

            var client = new PaypalApi(PaypalApi.CreatePaypalEnvironment(_commonConfig, paypalStorageCredentials), paymentInfo.BotId, correlationId);
            await client.ExecutePayment(paypalPayerId, paymentInfo.TransactionId);

            return;
        }

        public async Task<bool> ValidatePaypalWebhook(string botId, VerifyWebhookSignatureRaw signature, Guid correlationId)
        {
            var paypalStorageCredentials = _paypalStorageService.GetPaypalApiDataById(botId);
            signature.WebhookId = paypalStorageCredentials.WebhookId;

            var client = new PaypalApi(PaypalApi.CreatePaypalEnvironment(_commonConfig, paypalStorageCredentials), botId, correlationId);
            return await client.ValidateWebhookAsync(signature, botId, correlationId);
        }

        public async Task ProcessCoinGateAsync(string botId, EventInfo eventInfo, Guid correlationId)
        {
            Log.LogInf($"UpdateService: Received coingate payment event id = {eventInfo.PaymentInfo.TransactionId}", correlationId);
            Log.LogInf($"UpdateService: Received coingate data = {JsonConvertExt.SerializeObject(eventInfo)}", correlationId);

            var paymentInfo = _paymentInfoService.GetPaymentInfoById(eventInfo.PaymentInfo.Id.ToString());

            if (paymentInfo == null)
            {
                // Ahora mismo no contemplamos el scenario de transaccion creada en la web
                Log.LogErr($"UpdateService: Transaction not found id = {eventInfo.PaymentInfo.TransactionId}", correlationId);
                return;
            }

            if (paymentInfo.PaymentStatus == eventInfo.PaymentInfo.PaymentStatus &&
                paymentInfo.ReceivedAmount == eventInfo.PaymentInfo.ReceivedAmount)
            {
                Log.LogInf($"UpdateService: Transaction event already proccessed id = {eventInfo.PaymentInfo.TransactionId}", correlationId);
                return;
            }

            eventInfo.PaymentInfo.ItemName = paymentInfo.ItemName;
            _paymentInfoService.UpdatePaymentInfoWithBuyerInfo(eventInfo.PaymentInfo);

            var properties = AddPropertiesEvent(botId, eventInfo);
            var tags = _tagStorageService.GetTagsById(botId, paymentInfo.UserId);
            var states = _stateStorageService.GetStateById(botId, paymentInfo.UserId);

            var data = new Data()
            {
                Properties = properties,
                Tags = tags.Tags,
                State = states.States
            };
            var bot = _botStorageService.GetById(botId);
            var parameters = AddParametersPayment(botId, eventInfo, paymentInfo, bot.AdminId.ToString(), correlationId);
            Log.LogInf($"UpdateService: Adding parameters", correlationId);
            var funnel = _funnelStorageService.GetById(bot.GetFirstFunnelId(data).ToString());

            // TODO Data object is information used in the triggers
            // If we need to check payment status in triggers we would pass that info in Data.Properties

            // TODO Parameters is information used in Actions
            // We could need UserId, BotId, so we pass them in "parameters".
            Log.LogInf($"UpdateService: Processing funnel", correlationId);
            await funnel.ProcessEvents(data, parameters, ServiceProviderStatic.Instance, correlationId);

            return;
        }

        private async Task ProcessMessage(string botId, Message message, Guid correlationId)
        {
            Log.LogInf($"UpdateService: Received Message from {message.Chat.Id}", correlationId);

            if (message.Type == MessageType.Text)
            {
                var tags = _tagStorageService.GetTagsById(botId, message.From.Id.ToString());
                var states = _stateStorageService.GetStateById(botId, message.From.Id.ToString());
                var data = new Data()
                {
                    Message = message.Text,
                    ReplyMessage = message.ReplyToMessage != null ? message.ReplyToMessage.Text : string.Empty,
                    Tags = tags.Tags,
                    State = states.States,
                    Properties = AddPropertiesMessage(message)
                };
                var bot = _botStorageService.GetById(botId);
                var parameters = AddParametersMessage(botId, message, states, bot.AdminId.ToString(), correlationId);
                var funnel = _funnelStorageService.GetById(bot.GetFirstFunnelId(data).ToString());

                await funnel.ProcessMessages(data, parameters, ServiceProviderStatic.Instance, correlationId);
            }
            else if (message.Type == MessageType.Photo)
            {
                await _botService.GetClient(botId).SendTextMessageAsync(message.Chat.Id, "Thx for the Pics");
            }
        }

        private async Task ProcessCallback(string botId, CallbackQuery updateCallbackQuery, Guid correlationId)
        {
            Log.LogInf($"UpdateService: Received Message from {updateCallbackQuery.Message.Chat.Id}", correlationId);

            var tags = _tagStorageService.GetTagsById(botId, updateCallbackQuery.From.Id.ToString());
            var states = _stateStorageService.GetStateById(botId, updateCallbackQuery.From.Id.ToString());
            var data = new Data
            {
                Message = updateCallbackQuery.Data,
                Tags = tags.Tags,
                State = states.States,
                Properties = AddPropertiesCallback(updateCallbackQuery)
            };

            var bot = _botStorageService.GetById(botId);
            var parameters = AddParametersCallback(botId, updateCallbackQuery, states, bot.AdminId.ToString());
            var funnel = _funnelStorageService.GetById(bot.GetFirstFunnelId(data).ToString());

            await funnel.ProcessCallbacks(data, parameters, ServiceProviderStatic.Instance, correlationId);
        }

        public async Task ProcessWidgetAsync(string botId, Models.WidgetUpdate update, Guid correlationId)
        {
            Log.LogInf($"UpdateService: Received WidgetMessage from {update.ChatId} with fromId {update.FromId}", correlationId);


                var tags = _tagStorageService.GetTagsById(botId, update.FromId);
                var states = _stateStorageService.GetStateById(botId, update.FromId);
                var widgetConfig = _widgetConfigStorageService.GetWidgetConfigById(botId);
                var data = new Data()
                {
                    Message = update.Text,
                    ReplyMessage = string.Empty,
                    Tags = tags.Tags,
                    State = states.States,
                    Properties = AddPropertiesMessage(update)
                };
                var bot = _botStorageService.GetById(botId);
                var parameters = AddParametersMessage(botId, update, states, bot.AdminId.ToString(), correlationId,
                    widgetConfig);
                var funnel = _funnelStorageService.GetById(bot.GetFirstFunnelId(data).ToString());

            if (update.Type == "message")
            {
                await funnel.ProcessMessages(data, parameters, ServiceProviderStatic.Instance, correlationId);
            }
            else
            {
                await funnel.ProcessCallbacks(data, parameters, ServiceProviderStatic.Instance, correlationId);
            }
        }

        public async Task ProcessEvent(string botId, EventInfo eventInfo, Guid correlationId)
        {
            Log.LogInf($"UpdateService: Received event from {eventInfo.SubscriptionInfo.ChatId}", correlationId);

            var tags = _tagStorageService.GetTagsById(botId, eventInfo.SubscriptionInfo.UserId);
            var states = _stateStorageService.GetStateById(botId, eventInfo.SubscriptionInfo.UserId);
            var data = new Data
            {
                Message = eventInfo.Command,
                Tags = tags.Tags,
                State = states.States
            };

            var bot = _botStorageService.GetById(botId);
            var parameters = AddParametersEvent(botId, eventInfo, states, bot.AdminId.ToString(), correlationId);

            var funnel = _funnelStorageService.GetById(bot.GetFirstFunnelId(data).ToString());

            await funnel.ProcessEvents(data, parameters, ServiceProviderStatic.Instance, correlationId);
        }

        private static Dictionary<string, object> AddParametersCallback(string botId, CallbackQuery updateCallbackQuery,
            UserStates userStates, string adminId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddBotId(botId);
            parameters.AddChatId(updateCallbackQuery.Message.Chat.Id);
            parameters.AddMessageId(updateCallbackQuery.Message.MessageId);
            parameters.AddUserId(updateCallbackQuery.From.Id.ToString());
            parameters.AddUsername(updateCallbackQuery.From.Username);
            parameters.AddMessageText(updateCallbackQuery.Message.Text);
            parameters.AddCallbackData(updateCallbackQuery.Data);

            foreach (var userState in userStates.States)
            {
                parameters.Add(userState.Key, userState.Value);
            }
            return parameters;
        }

        private static Dictionary<string, object> AddParametersMessage(string botId, Message message, UserStates userStates
            , string adminId, Guid correlationId)
        {
            var parameters = new Dictionary<string, object>();
            if (message.Text == "/start")
            {
                parameters.AddFirstname(message.From.FirstName);
                parameters.AddLastname(message.From.LastName);
            }

            if (!string.IsNullOrEmpty(message.From.Username))
            {
                parameters.AddUsername(message.From.Username);
            }

            parameters.AddBotId(botId);
            parameters.AddChatId(message.Chat.Id);
            parameters.AddMessageId(message.MessageId);
            parameters.AddMessageText(message.Text);
            parameters.AddUserId(message.From.Id.ToString());
            parameters.AddadminId(adminId);
            foreach (var userState in userStates.States)
            {
                parameters.Add(userState.Key, userState.Value);
            }
            parameters.AddCorrelationId(correlationId);
            return parameters;
        }

        private static Dictionary<string, object> AddParametersMessage(string botId, Models.WidgetUpdate update, UserStates userStates
            , string adminId, Guid correlationId, WidgetConfigData widgetConfigData)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddFirstname(update.FirstName);
            parameters.AddLastname(update.LastName);

            parameters.AddBotId(botId);
            parameters.AddChatId(long.Parse(update.ChatId));
            parameters.AddUserId(update.FromId);
            parameters.AddMessageText(update.Text);
            parameters.AddadminId(adminId);
            parameters.AddClientId(update.WebUserId);
            if (!string.IsNullOrEmpty(widgetConfigData?.Language))
            {
                parameters.AddLanguage(widgetConfigData.Language);
            }
            foreach (var userState in userStates.States)
            {
                parameters.Add(userState.Key, userState.Value);
            }
            parameters.AddCorrelationId(correlationId);
            return parameters;
        }

        private Dictionary<string, object> AddParametersPayment(string botId, EventInfo eventInfo,
            PaymentInfo paymentInfo, string adminId, Guid correlationId)
        {
            var parameters = new Dictionary<string, object>();

            if (eventInfo.PaymentInfo.TransactionId == null)
            {
                eventInfo.PaymentInfo.TransactionId = string.Empty;
            }
            parameters.AddBotId(botId);
            parameters.AddUserId(paymentInfo.UserId);
            parameters.AddChatId(paymentInfo.ChatId);
            parameters.AddItemName(paymentInfo.ItemName);
            parameters.AddBuyerAmount(eventInfo.PaymentInfo.BuyerAmount);
            parameters.AddReceivedAmount(eventInfo.PaymentInfo.ReceivedAmount);
            parameters.AddBuyerCurrency(eventInfo.PaymentInfo.BuyerCurrency);
            parameters.AddadminId(adminId);
            parameters.AddUsername(paymentInfo.Username);
            parameters.AddTransactionId(eventInfo.PaymentInfo.TransactionId);
            parameters.AddCorrelationId(correlationId);
            parameters.AddOriginalAmount(eventInfo.PaymentInfo.OriginalAmount);
            parameters.AddOriginalCurrency(eventInfo.PaymentInfo.OriginalCurrency);
            return parameters;
        }

        private Dictionary<string, object> AddParametersEvent(string botId, EventInfo eventInfo
           , UserStates userStates, string adminId, Guid correlationId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddBotId(botId);
            if (eventInfo.SubscriptionInfo.ChatId != 0)
            {
                parameters.AddChatId(eventInfo.SubscriptionInfo.ChatId);
            }

            if (eventInfo.SubscriptionInfo.SubscriptionId != Guid.Empty)
            {
                parameters.AddSubscriptionId(eventInfo.SubscriptionInfo.SubscriptionId);
            }

            if (!string.IsNullOrEmpty(eventInfo.Username))
            {
                parameters.AddUsername(eventInfo.Username);
            }

            int userId;
            if (int.TryParse(eventInfo.SubscriptionInfo.UserId, out userId))
            {
                parameters.AddUserId(userId.ToString());
            }
            foreach (var userState in userStates.States)
            {
                parameters.Add(userState.Key, userState.Value);
            }
            parameters.AddadminId(adminId);
            parameters.AddItemName(eventInfo.SubscriptionInfo.ItemName);
            parameters.AddUsername(eventInfo.Username);
            parameters.AddChannels(eventInfo.Channels);
            /* TODO  Hack para poder usar commands */
            /* Ya he anadido a parameters lo que necesito , ahora falta crear el action*/
            parameters.AddCorrelationId(correlationId);
            parameters.AddMessageText(eventInfo.Command);
            return parameters;
        }

        private Dictionary<string, object> AddPropertiesEvent(string botId, EventInfo eventInfo)
        {
            var properties = new Dictionary<string, object>();

            properties.AddBuyerAmount(eventInfo.PaymentInfo.BuyerAmount);
            properties.AddReceivedAmount(eventInfo.PaymentInfo.ReceivedAmount);
            properties.AddReceivedConfirms(eventInfo.PaymentInfo.ReceivedConfirms);
            properties.AddPaymentStatus(eventInfo.PaymentInfo.PaymentStatus);
            properties.AddBotId(botId);
            int userId;
            if (int.TryParse(eventInfo.PaymentInfo.UserId, out userId))
            {
                properties.AddUserId(userId.ToString());
            }
            properties.AddItemName(eventInfo.PaymentInfo.ItemName);
            return properties;
        }

        private Dictionary<string, object> AddPropertiesMessage(Message message)
        {
            var properties = new Dictionary<string, object>();
            properties.AddFirstname(message.From.FirstName);
            properties.AddLastname(message.From.LastName);
            properties.AddUsername(message.From.Username);

            return properties;
        }

        private Dictionary<string, object> AddPropertiesMessage(Models.WidgetUpdate update)
        {
            var properties = new Dictionary<string, object>();
            properties.AddFirstname(update.FirstName);
            properties.AddLastname(update.LastName);

            return properties;
        }

        private Dictionary<string, object> AddPropertiesCallback(CallbackQuery updateCallbackQuery)
        {
            var properties = new Dictionary<string, object>();
            properties.AddFirstname(updateCallbackQuery.From.FirstName);
            properties.AddLastname(updateCallbackQuery.From.LastName);
            properties.AddUsername(updateCallbackQuery.From.Username);

            return properties;
        }
    }
}
