using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Objects;
using Core.Common.Extensions;
using CryptoCore.Exchanges;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class BiGetOpenOrdersProcessor : ActionProcessorAsync<BiGetOpenOrdersAsync>
    {
        private readonly IExchangeApiStorageService _exchangeApiStorageService;

        public BiGetOpenOrdersProcessor(IExchangeApiStorageService exchangeApiStorageService)
        {
            _exchangeApiStorageService = exchangeApiStorageService;
        }

        public override async Task<bool> ExecuteAsync(BiGetOpenOrdersAsync action,
            Dictionary<string, object> parameters)
        {
            var userId = parameters.GetUserId();
            var binanceKeys = _exchangeApiStorageService.GetExchangeApiDataById(userId.ToString(), ExchangeName.Binance);

            IExchange binance = new BinanceExchange(binanceKeys.ApiKey, binanceKeys.ApiSecret);
            var result = await binance.GetOpenTrades();
            await binance.RefreshPriceMarketData();
            var marketData = binance.GetPriceMarketData();

            if (result.Success)
            {
                var openTrades = ((BinanceOrder[]) result.Data).ToList();
                parameters.AddBiOpenTrades(openTrades);
                parameters.AddBiMarketData(marketData);
            }
            else
            {
                Log.LogErr($"BiGetOpenTradesProcessor error retrieving OpenTrades {result.Error}", action.CorrelationId);
            }

            return result.Success;
        }
    }


    public class BiGetOpenOrdersAsync : FunnelAction
    {
    }
}
