using Mentor.telegram.bot.boundedContext.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.paypalpayments;
using Mentor.telegram.paypalpayments.Models;
using Bot.Configuration;

namespace Bot.telegram.funnel.Actions
{
    public class PaypalPaymentCreateProcessor : ActionProcessorAsync<PaypalPaymentCreateActionAsync>
    {
        private readonly IClientService _clientService;
        private readonly IPaymentInfoService _paymentInfoService;
        private readonly IPaypalApiStorageService _storageService;
        private readonly IConfigurationCommon _configCommon;
        private readonly IConfigurationFunnel _configurationFunnel;

        public PaypalPaymentCreateProcessor(IPaymentInfoService paymentInfoService, IPaypalApiStorageService storageService,
             IClientService clientService, IConfigurationFunnel configurationFunnel, IConfigurationCommon configCommon)
        {
            _configCommon = configCommon;
            _storageService = storageService;
            _paymentInfoService = paymentInfoService;
            _clientService = clientService;
            _configurationFunnel = configurationFunnel;
        }

        public async override Task<bool> ExecuteAsync(PaypalPaymentCreateActionAsync action, Dictionary<string, object> parameters)
        {
            var botId = parameters.GetBotId();
            var userId = parameters.GetUserId();
            var chatId = parameters.GetChatId();
            var correlationId = parameters.GetCorrelationId();

            var paypalApiData = _storageService.GetPaypalApiDataById(botId);

            if (paypalApiData == null ||
                string.IsNullOrEmpty(paypalApiData.ClientId) ||
                string.IsNullOrEmpty(paypalApiData.SecretId))
            {
                Log.LogErr($"Could not find paypalApi data for bot {botId}", action.CorrelationId);
                return false;
            }
            var paymentGuid = Guid.NewGuid();
            var paypalEnvironemnt = PaypalApi.CreatePaypalEnvironment(_configCommon, paypalApiData);

            var paypalApi = new PaypalApi(paypalEnvironemnt, botId, action.CorrelationId);

            var result = await paypalApi.CreatePaymentAsync(new PaypalPaymentInfo
            {
                Amount = action.Amount,
                CancelUrl = $"{_configurationFunnel.GetServerUrl}/api/paypal/{paymentGuid}/cancel",
                SuccessUrl = $"{_configurationFunnel.GetServerUrl}/api/paypal/{paymentGuid}/success",
                Currency = action.Currency,
                Description = action.Description,
                Items = action.Items
            });

            if (result == null)
                return false;

            var paymentInfo = CreatePaymentInfo(action, paymentGuid, botId, chatId,
                userId.ToString(), correlationId, result.Id);

            if (paymentInfo == null)
                return false;



            parameters.AddBuyerAmount(action.Amount);
            parameters.AddBuyerCurrency(action.Currency);
            parameters.AddCallbackData(result.CheckoutLink);

            return true;
        }

        private Mentor.telegram.bot.models.PaymentInfo CreatePaymentInfo(PaypalPaymentCreateActionAsync action, Guid paymentId,
            string botId, long chatId, string userId, Guid correlationId, string transactionId)
        {
            try
            {
                var info = new Mentor.telegram.bot.models.PaymentInfo
                {
                    Id = paymentId,
                    BotId = botId,
                    BuyerAmount = action.Amount,
                    BuyerCurrency = action.Currency,
                    ChatId = chatId,
                    UserId = userId,
                    ItemName = action.Title,
                    PaymentStatus = 0,
                    Provider = Mentor.telegram.bot.models.PaymentProvider.Paypal,
                    TransactionId = transactionId
                };
                _paymentInfoService.AddPaymentInfo(info);

                return info;
            }
            catch (Exception ex)
            {
                Log.LogErr($"Exception during CreatePaymentInfo for bot: {botId}, exception: ${JsonConvertExt.SerializeObject(ex)}", correlationId);
            }

            return null;
        }
    }
}
