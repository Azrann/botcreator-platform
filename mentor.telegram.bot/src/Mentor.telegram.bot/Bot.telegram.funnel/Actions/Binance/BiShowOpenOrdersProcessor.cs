using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Binance.Net.Objects;
using CryptoCore.Model;

namespace Bot.telegram.funnel.Actions
{
    public class BiShowOpenOrdersProcessor : ActionProcessorAsync<BiShowOpenOrdersAsync>
    {
        private readonly IUserInterfaceService _userInterfaceService;

        public BiShowOpenOrdersProcessor(IUserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public override async Task<bool> ExecuteAsync(BiShowOpenOrdersAsync action,
            Dictionary<string, object> parameters)
        {
            var openTrades = parameters.GetBiOpenTrades();
            var marketData = parameters.GetBiMarketData();

            await _userInterfaceService.SendMessage(
                $"Here is a list of your open trades:\n\n {OpenTradesToString(openTrades, marketData)}", parameters);

            return true;
        }

        public string OpenTradesToString(List<BinanceOrder> orders, PriceData marketData)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var order in orders)
            {
                sb.Append(OpenTradeToString(order,marketData.Data[order.Symbol]));
            }
            return sb.ToString();
        }

        public string OpenTradeToString(BinanceOrder order, decimal currentPrice)
        {
            var orderSide = BiFormatter.GetSide(order.Side);
            string orderType = BiFormatter.GetOrderType(order.Type);
            return
                $"🏷{order.OrderId} ⏰{order.Time}\n{order.Symbol} {orderType} {orderSide}\n"+
                $"EntryPrice:<b>{order.Price.ToString("G29")}</b> CurrentPrice:<b> {currentPrice.ToString("G29")}\n"+
                $"</b> Amount:<b>{order.OriginalQuantity.ToString("G29")}</b> Filled:{order.ExecutedQuantity.ToString("G29")}%\n\n";
        }
    }

    public class BiShowOpenOrdersAsync : FunnelAction
    {
    }

    public class BiFormatter
    {
        public static string GetSide(OrderSide side)
        {
            return side == OrderSide.Buy ? "📈Buy" : "📉Sell";
        }

        public static string GetOrderType(OrderType type)
        {
            string orderType;
            switch (type)
            {
                case OrderType.Limit:
                    orderType = "📌Limit";
                    break;
                case OrderType.Market:
                    orderType = "📍Market";
                    break;
                case OrderType.StopLoss:
                    orderType = "🛑StopLoss";
                    break;
                case OrderType.StopLossLimit:
                    orderType = "🛑📌StopLossLimit";
                    break;
                case OrderType.TakeProfit:
                    orderType = "💰TakeProfit";
                    break;
                case OrderType.TakeProfitLimit:
                    orderType = "💰📌TakeProfit";
                    break;
                default:
                    orderType = type.ToString();
                    break;
            }

            return orderType;
        }
    }
}
