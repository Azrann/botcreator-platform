using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoCore.Model;

namespace CryptoCore.Exchanges
{
    public interface IExchange
    {
        Dictionary<string, MarketData> GetMarketData();
        List<string> GetMainCurrencies();
        Task<Dictionary<string, MarketData>> RefreshMarketData(List<string> symbols);
        PriceData GetPriceMarketData();
        Task<PriceData> RefreshPriceMarketData();
        Task<List<string>> GetOpenTrades(List<string> symbols);
        Task<ApiResponse> GetOpenTrades();
        Task<List<string>> GetOpenTransfers();
        Task<Dictionary<string, decimal>> GetBalances();
        Task<string> GetDepositAddress(string currency);
        Task<ApiResponse> CreateBuyLimitOrder(string symbol, decimal quantity, decimal price);
        Task<ApiResponse> CreateBuyMarketOrder(string symbol, decimal quantity, decimal lastKnownPrice = 0);
        Task<ApiResponse> CreateSellMarketOrder(string symbol, decimal quantity, decimal lastKnownPrice = 0);
        Task<ApiResponse> CreateSellLimitOrder(string symbol, decimal quantity, decimal price);
        Task<bool> CreateWithdraw(string currency, decimal amount, string address);
        Task<List<SymbolVolume>> GetMarketVolume();
        Task<TickerRules> GetTickerRules();
        Task<TickerRules> RefreshTickerRules();
        int GetMaxTasks();
        int GetResponseTime();
        string GetExchangeName();
        MarketFee GetMarketFee();
        Task<Dictionary<string, Currency>> GetCurrencies();
        Task<Dictionary<string, CryptoBalance>> GetFunds(bool force = false);
        Task<string> Transfer(string currency, decimal amount, string address);
        Task<ApiResponse> CancelOpenOrder(string symbol, long orderId);
        Task<ApiResponse> CreateBuyStopLimit(string symbol, decimal quantity, decimal price, decimal stopLimitPrice);
        Task<ApiResponse> CreateSellStopLimit(string symbol, decimal quantity, decimal price, decimal stopLimitPrice);
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
    }
}
