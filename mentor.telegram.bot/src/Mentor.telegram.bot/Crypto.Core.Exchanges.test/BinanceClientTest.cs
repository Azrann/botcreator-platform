using CryptoCore.Exchanges;
using Xunit;

namespace Crypto.Core.Exchanges.test
{
    public class BinanceClientTest
    {
        private readonly string ApiKey = "REPLACE_WITH_YOUR_VALUE";
        private readonly string SecretKey  = "REPLACE_WITH_YOUR_VALUE";

        [Fact]
        public async void WhenIRequestUserInformationICanRetrieveIt()
        {
            IExchange binance = new BinanceExchange(ApiKey,SecretKey);
            var result = await binance.CreateBuyLimitOrder("XRP/BNB", (decimal)80, (decimal)0.023);
            Assert.True(result.Success);
        }

        [Fact]
        public async void WhenIRequestMyBalanceICanRetrieveIt()
        {
            IExchange binance = new BinanceExchange(ApiKey, SecretKey);
            var result = await binance.GetBalances();
            Assert.True(result.Keys.Count>1);
        }

        [Fact]
        public async void WhenIRequestMyOpenOrdersICanRetrieveIt()
        {
            IExchange binance = new BinanceExchange(ApiKey, SecretKey);
            var result = await binance.GetOpenTrades();
            Assert.True(result.Success);
        }
    }
}
