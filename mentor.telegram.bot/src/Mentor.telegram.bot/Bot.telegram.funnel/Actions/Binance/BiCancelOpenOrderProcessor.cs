using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using CryptoCore.Exchanges;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class BiCancelOpenOrderProcessor : ActionProcessorAsync<BiCancelOpenOrderAsync>
    {
        private readonly IExchangeApiStorageService _exchangeApiStorageService;

        public BiCancelOpenOrderProcessor(IExchangeApiStorageService exchangeApiStorageService)
        {
            _exchangeApiStorageService = exchangeApiStorageService;
        }

        public override async Task<bool> ExecuteAsync(BiCancelOpenOrderAsync action,
            Dictionary<string, object> parameters)
        {
            var userId = parameters.GetUserId();
            var binanceKeys = _exchangeApiStorageService.GetExchangeApiDataById(userId.ToString(), ExchangeName.Binance);

            IExchange binance = new BinanceExchange(binanceKeys.ApiKey, binanceKeys.ApiSecret);
            var order = parameters.GetCallbackData().Split(" ");
            var result = await binance.CancelOpenOrder(order[1],long.Parse(order[0].Replace("🏷", "")));

            if (result.Success)
            {
                parameters.AddBiMessageResult($"{parameters.GetCallbackData()} <b>Cancelled</b>");
            }
            else
            {
                Log.LogErr($"BiCancelOpenOrderProcessor error canceling OpenOrder {result.Error}", action.CorrelationId);
            }

            return result.Success;
        }
    }

    public class BiCancelOpenOrderAsync : FunnelAction
    {
    }
}
