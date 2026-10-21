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
    public class PaymentInfoCreateProcessor : ActionProcessorAsync<PaymentInfoCreateActionAsync>
    {
        private readonly IPaymentInfoService _paymentInfoService;
        private readonly ICoinApiStorageService _coinApiStorageService;
        private readonly IClientService _clientService;
        private readonly IConfigurationFunnel _configurationFunnel;

        public PaymentInfoCreateProcessor(IPaymentInfoService paymentInfoService, ICoinApiStorageService
            coinApiStorageService, IClientService clientService, IConfigurationFunnel configurationFunnel)
        {
            _paymentInfoService = paymentInfoService;
            _coinApiStorageService = coinApiStorageService;
            _clientService = clientService;
            _configurationFunnel = configurationFunnel;
        }

        public override async Task<bool> ExecuteAsync(PaymentInfoCreateActionAsync action, Dictionary<string, object> parameters)
        {
            var botId = parameters.GetBotId();
            var userId = parameters.GetUserId();

            var coinApiData = _coinApiStorageService.GetCoinApiDataById(botId);

            if (coinApiData == null || string.IsNullOrEmpty(coinApiData.PrivateKey))
            {
                Log.LogErr($"Could not find coinAPI data for bot {botId}", action.CorrelationId);
                return false;
            }

            var clientInfo = _clientService.GetClient(botId, userId.ToString());

            CoinApi api = new CoinApi(coinApiData.PublicKey, coinApiData.PrivateKey);

            Guid paymentInfoId = Guid.NewGuid();

            action.BuyerCurrency = string.IsNullOrEmpty(action.BuyerCurrency)
                ? parameters["BuyerCurrency"].ToString()
                : action.BuyerCurrency;

            var transaction = new ReceiveTransaction()
            {
                Amount = action.Amount,
                Currency1 = action.OriginalCurrency,
                Currency2 = action.BuyerCurrency,
                BuyerEmail = clientInfo.UserEmail,
                BuyerName = clientInfo.User.FirstName,
                Cmd = "create_transaction",
                CallBackUrl = $"{_configurationFunnel.GetServerUrl}/api/payment/{botId}",
                ItemName = action.ItemName,
                Invoice = paymentInfoId.ToString()
            };
            
            Log.LogInf($"PaymentInfoCreateProcessor: ReceiveTransaction sending transaction {JsonConvertExt.SerializeObject(transaction)}", action.CorrelationId);
            try
            {
                var result = await api.SendPayment(transaction, action.CorrelationId);
                if (result.Error == "ok")
                {
                    PaymentInfo paymentInfo = CreatePaymentInfo(result, action, paymentInfoId, botId, long.Parse(userId), clientInfo.ChatId);
                    parameters.AddBuyerAmount(paymentInfo.BuyerAmount);
                    parameters.AddBuyerCurrency(paymentInfo.BuyerCurrency);
                    parameters.AddWalletAddress(paymentInfo.WalletAddress);
                    parameters.AddTransactionId(paymentInfo.TransactionId);
                }
                else
                {
                    Log.LogErr($"PaymentInfoCreateProcessor: Error sending transaction {JsonConvertExt.SerializeObject(result)}", action.CorrelationId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.LogErr($"PaymentInfoCreateProcessor: Exception sending transaction {JsonConvertExt.SerializeObject(ex)}", action.CorrelationId);
                return false;
            }

            return true;
        }

        private PaymentInfo CreatePaymentInfo(ReceiveTransactionResponse transactionResponse, 
            PaymentInfoCreateActionAsync action, Guid paymentInfoId, string botId, long userId,
            long chatId)
        {
            PaymentInfo paymentInfo = new PaymentInfo()
            {
                Id = paymentInfoId,
                BotId = botId,
                UserId = userId.ToString(),
                PaymentStatus = 0,
                TransactionId = transactionResponse.Result.TransactionId,
                WalletAddress = transactionResponse.Result.Address,
                BuyerAmount = Convert.ToDecimal(transactionResponse.Result.Amount),
                OriginalAmount = Convert.ToDecimal(action.Amount),
                OriginalCurrency = action.OriginalCurrency,
                BuyerCurrency = action.BuyerCurrency,
                ItemName = action.ItemName,
                ChatId = chatId,
                Provider = PaymentProvider.Coinpayments,
                SubscriptionMonths = (short)action.MonthsDuration,
                SubscriptionDays = (short)action.DaysDuration,
            };
            _paymentInfoService.AddPaymentInfo(paymentInfo);
            return paymentInfo;
        }
    }
}
