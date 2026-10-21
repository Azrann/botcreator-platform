using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using CryptoCore.Exchanges;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class BiCreateOrderProcessor : ActionProcessorAsync<BiCreateOrderAsync>
    {
        private const string Limit = "Limit";
        private const string Market = "Market";
        private const string StopLimit = "StopLimit";

        private const string Buy = "Buy";
        private const string Sell = "Sell";

        private readonly IExchangeApiStorageService _exchangeApiStorageService;

        public BiCreateOrderProcessor(IExchangeApiStorageService exchangeApiStorageService)
        {
            _exchangeApiStorageService = exchangeApiStorageService;
        }

        public override async Task<bool> ExecuteAsync(BiCreateOrderAsync action,
            Dictionary<string, object> parameters)
        {
            var userId = parameters.GetUserId();
            var binanceKeys = _exchangeApiStorageService.GetExchangeApiDataById(userId.ToString(), ExchangeName.Binance);

            IExchange binance = new BinanceExchange(binanceKeys.ApiKey, binanceKeys.ApiSecret);

            var orderType = parameters.GetOrderType();
            switch (orderType)
            {
                case Limit: return await CreateLimitOrder(binance,parameters,action);
                case Market:
                    return await CreateMarketOrder(binance, parameters, action);
                case StopLimit:
                    return await CreateStopLimitOrder(binance, parameters, action);
            }

            return false;
        }

        private async Task<bool> CreateStopLimitOrder(IExchange binance, Dictionary<string, object> parameters, BiCreateOrderAsync action)
        {
            ApiResponse result;
            if (parameters.GetOrderSide() == Buy)
            {
                result = await binance.CreateBuyStopLimit(parameters.GetPair(), parameters.GetOrderAmount(), parameters.GetLimitPrice(), parameters.GetStopLimitPrice());
            }
            else
            {
                result = await binance.CreateSellStopLimit(parameters.GetPair(), parameters.GetOrderAmount(), parameters.GetLimitPrice(), parameters.GetStopLimitPrice());
            }

            if (result.Success)
            {
                parameters.AddBiMessageResult($"{parameters.GetOrderSide()} StopLimitOrder <b>Created</b>");
            }
            else
            {
                Log.LogErr($"BiCreateOrderProcessor error creating StopLimitOrder {result.Error}", action.CorrelationId);
            }
            return result.Success;
        }

        private async Task<bool> CreateMarketOrder(IExchange binance, Dictionary<string, object> parameters, BiCreateOrderAsync action)
        {
            ApiResponse result;
            if (parameters.GetOrderSide() == Buy)
            {
                result = await binance.CreateBuyMarketOrder(parameters.GetPair(), parameters.GetOrderAmount());
            }
            else
            {
                result = await binance.CreateSellMarketOrder(parameters.GetPair(), parameters.GetOrderAmount());
            }

            if (result.Success)
            {
                parameters.AddBiMessageResult($"{parameters.GetOrderSide()} MarketOrder <b>Created</b>");
            }
            else
            {
                Log.LogErr($"BiCreateOrderProcessor error creating Market Order {result.Error}", action.CorrelationId);
            }
            return result.Success;
        }

        private async Task<bool> CreateLimitOrder(IExchange binance, Dictionary<string, object> parameters, BiCreateOrderAsync action)
        {
            ApiResponse result;
            if (parameters.GetOrderSide() == Buy)
            {
                result = await binance.CreateBuyLimitOrder(parameters.GetPair(), parameters.GetOrderAmount(), parameters.GetLimitPrice());
            }
            else
            {
                result = await binance.CreateSellLimitOrder(parameters.GetPair(), parameters.GetOrderAmount(), parameters.GetLimitPrice());
            }

            if (result.Success)
            {
                parameters.AddBiMessageResult($"{parameters.GetOrderSide()} LimitOrder <b>Created</b>");
            }
            else
            {
                Log.LogErr($"BiCreateOrderProcessor error creating Limit Order {result.Error}", action.CorrelationId);
            }
            return result.Success;
        }
    }
}
