using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoCore.Exchanges;

namespace Bot.telegram.funnel.Actions
{
    public class BiCreateLimitOrderProcessor : ActionProcessorAsync<BiCreateLimitOrderAsync>
    {

        public override async Task<bool> ExecuteAsync(BiCreateLimitOrderAsync action,
            Dictionary<string, object> parameters)
        {
            IExchange binance = new BinanceExchange("","");
            await binance.CreateBuyLimitOrder("BTC/USDT", (decimal) 0.0035,(decimal) 1000.0);
            return false;
        }
    }
}
