using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoCore.Exchanges;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class BiEnrichParametersProcessor : ActionProcessorAsync<BiEnrichParametersAsync>
    {
        private readonly IExchangeApiStorageService _exchangeApiStorageService;

        public BiEnrichParametersProcessor(IExchangeApiStorageService exchangeApiStorageService)
        {
            _exchangeApiStorageService = exchangeApiStorageService;
        }

        public override async Task<bool> ExecuteAsync(BiEnrichParametersAsync action,
            Dictionary<string, object> parameters)
        {
            var userId = parameters.GetUserId();
            var pair = parameters.GetPair();
            var pairSplit = pair.Split("/");
            parameters.AddPairLeft(pairSplit[0]);
            parameters.AddPairRight(pairSplit[1]);

            var binanceKeys = _exchangeApiStorageService.GetExchangeApiDataById(userId.ToString(), ExchangeName.Binance);
            
            IExchange binance = new BinanceExchange(binanceKeys.ApiKey, binanceKeys.ApiSecret);
            var marketData = await binance.RefreshPriceMarketData();
            var biPair = pair.Replace("/", "").Replace("\\", "");
            var currentPrice = marketData.Data[biPair];

            var limitPrice = parameters.GetOrderType() == "Market" ? currentPrice : parameters.GetLimitPrice();

            parameters.AddCurrentPrice(marketData.Data[biPair]);
            parameters.AddBiTotal(parameters.GetOrderSide() == "Buy"
                ? $"You pay {(parameters.GetOrderAmount() * limitPrice).ToString("G29")}"
                : $"You get {(parameters.GetOrderAmount() * limitPrice).ToString("G29")}");

            return true;
        }
    }

    public class BiEnrichParametersAsync : FunnelAction
    {
    }
}
