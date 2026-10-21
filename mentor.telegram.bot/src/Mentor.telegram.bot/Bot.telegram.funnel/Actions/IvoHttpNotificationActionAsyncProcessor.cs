using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common;
using Core.Common.Extensions;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class IvoHttpNotificationActionAsyncProcessor : ActionProcessorAsync<IvoHttpNotificationActionAsync>
    {
        private readonly ICoinApiStorageService _coinApiStorageService;
        private const string IvoBaseAddress = "https://verifiedcryptotraders.com/member-area/membership/update";
        private const string RequestUri = "";

        public IvoHttpNotificationActionAsyncProcessor(ICoinApiStorageService coinApiStorageService)
        {
            _coinApiStorageService = coinApiStorageService;
        }

        public override async Task<bool> ExecuteAsync(IvoHttpNotificationActionAsync action, Dictionary<string, object> parameters)
        {
            Log.LogInf($"IvoHttpNotificationAction Processing",action.CorrelationId);

            var data = _coinApiStorageService.GetCoinApiDataById(parameters.GetBotId());
            var username = parameters.GetUsername();
            var splitData = parameters.GetItemName().Split(".");
            var itemName = splitData[0];
            var discountCode = splitData.Length == 2 ? splitData[1] : string.Empty;
            var payload = new
            {
                IpnSecret = data.PublicKey,
                Payload = new
                {
                    Username = username,
                    action.Command,
                    action.MonthsDuration,
                    Amount = parameters.GetBuyerAmount(),
                    Currency = parameters.GetBuyerCurrency(),
                    ItemName = parameters.GetItemName(),
                    TransactionId = parameters.GetTransactionId(),
                    Coupon = discountCode,
                    OriginalAmount = parameters.GetOriginalAmount(),
                    OriginalCurrency = parameters.GetOriginalCurrency()
                }
            };

            var result = await HttpClientHelper.PostAsync(payload, IvoBaseAddress, RequestUri);

            if (!result.IsSuccessStatusCode)
            {
                Log.LogErr($"Error sending request to IvoWebApi object {JsonConvertExt.SerializeObject(payload)} with error {JsonConvertExt.SerializeObject(result.StatusCode)}",action.CorrelationId);
            }

            return true;
        }
    }
}