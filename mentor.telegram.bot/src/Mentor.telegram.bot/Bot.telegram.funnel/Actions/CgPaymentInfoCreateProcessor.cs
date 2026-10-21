using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.Configuration;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Mentor.telegram.coinpayments;
using Core.Common.Extensions;

namespace Bot.telegram.funnel.Actions
{
    public class CgPaymentInfoCreateProcessor : ActionProcessorAsync<CgPaymentInfoCreateActionAsync>
    {
        private readonly IPaymentInfoService _paymentInfoService;
        private readonly ICoinApiStorageService _coinApiStorageService;
        private readonly IConfigurationFunnel _configurationFunnel;

        public CgPaymentInfoCreateProcessor(IPaymentInfoService paymentInfoService, ICoinApiStorageService
            coinApiStorageService, IConfigurationFunnel configurationFunnel)
        {
            _paymentInfoService = paymentInfoService;
            _coinApiStorageService = coinApiStorageService;
            _configurationFunnel = configurationFunnel;
        }

        public override async Task<bool> ExecuteAsync(CgPaymentInfoCreateActionAsync action, Dictionary<string, object> parameters)
        {
            var botId = parameters.GetBotId();
            var userId = parameters.GetUserId();
            var username = parameters.GetUsername();

            var data = _coinApiStorageService.GetCoinApiDataById(botId);

            if (data == null || string.IsNullOrEmpty(data.PrivateKey))
            {
                Log.LogErr($"Could not find coinGate data for bot {botId}", action.CorrelationId);
                return false;
            }
            
            CoinGateApi api = new CoinGateApi(data.PrivateKey);

            Guid paymentInfoId = Guid.NewGuid();

            var transaction = new CreateOrder()
            {
                order_id = paymentInfoId.ToString(),
                price_amount = action.Amount,
                price_currency = action.OriginalCurrency, // Which currency do you price your Item could be EUR ..
                receive_currency = action.OriginalCurrency, // Which currency do you want to receive your Item
                callback_url = $"{_configurationFunnel.GetServerUrl}/api/coingate/{botId}",
                title = action.Title,
                description = action.Description,
                token = data.IpnSecret
            };

            Log.LogInf($"CgPaymentInfoCreateProcessor: ReceiveTransaction sending transaction {JsonConvertExt.SerializeObject(transaction)}", action.CorrelationId);
            try
            {
                var result = await api.SendPayment(transaction, action.CorrelationId);
                if (result.errors == null || result.errors.Count == 0)
                {
                    PaymentInfo paymentInfo = CreatePaymentInfo(result, action, paymentInfoId, botId, long.Parse(userId), long.Parse(userId),username);
                    parameters.AddBuyerAmount(paymentInfo.BuyerAmount);
                    parameters.AddBuyerCurrency(paymentInfo.BuyerCurrency);
                    parameters.AddPaymentUrl(result.payment_url);
                    parameters.AddTransactionId(paymentInfo.TransactionId);
                    parameters.AddItemName(paymentInfo.ItemName);
                }
                else
                {
                    Log.LogErr($"CgPaymentInfoCreateProcessor: Error sending transaction {JsonConvertExt.SerializeObject(result)}", action.CorrelationId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.LogErr($"CgPaymentInfoCreateProcessor: Exception sending transaction {JsonConvertExt.SerializeObject(ex)}", action.CorrelationId);
                return false;
            }

            return true;
        }

        private PaymentInfo CreatePaymentInfo(CreateOrderResponse transactionResponse, 
            CgPaymentInfoCreateActionAsync action, Guid paymentInfoId, string botId, long userId,
            long chatId,string username)
        {
            PaymentInfo paymentInfo;
            try
            {
                paymentInfo = new PaymentInfo()
                {
                    Id = paymentInfoId,
                    BotId = botId,
                    UserId = userId.ToString(),
                    Username = username,
                    PaymentStatus = 0,
                    TransactionId = transactionResponse.id,
                    WalletAddress = "",
                    BuyerAmount = Convert.ToDecimal(transactionResponse.receive_amount),
                    OriginalAmount = Convert.ToDecimal(action.Amount),
                    OriginalCurrency = action.OriginalCurrency,
                    BuyerCurrency = transactionResponse.receive_currency,
                    ItemName = action.Title,
                    ChatId = chatId,
                    Provider = PaymentProvider.CoingGate,
                    SubscriptionMonths = (short)action.MonthsDuration,
                    SubscriptionDays = (short)action.DaysDuration,
                };
                _paymentInfoService.AddPaymentInfo(paymentInfo);
            }
            catch (Exception)
            {
                return null;
            }
            
            return paymentInfo;
        }
    }
}
