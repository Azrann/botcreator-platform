using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net;
using Binance.Net.Objects;
using CryptoCore.Model;
using CryptoExchange.Net.Authentication;

namespace CryptoCore.Exchanges
{
    public class BinanceExchange :IExchange
    {
        private readonly BinanceClient _api;
        private Dictionary<string, Currency> _currencies;
        private DateTime _lastGetFundRefresh;
        private Dictionary<string,CryptoBalance> _funds;
        private PriceData _marketPriceData;
        private Dictionary<string, MarketData> _marketsData;
        private TickerRules _tickerRules;

        public BinanceExchange(string apiKey, string secretKey)
        {
            _api = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(apiKey,
                    secretKey)
            });
            _lastGetFundRefresh = DateTime.Now.AddDays(-1);
        }

        public Dictionary<string, MarketData> GetMarketData()
        {
            return _marketsData;
        }

        public List<string> GetMainCurrencies()
        {
            return new List<string>()
            {
                "BTC",
                "ETH",
                "BNB",
                "USDT"
            };
        }

        public async Task<Dictionary<string, MarketData>> RefreshMarketData(List<string> symbols)
        {
            var allMarketData = await _api.GetAllBookPricesAsync();

            Dictionary<string, MarketData> result = new Dictionary<string, MarketData>();
            foreach (var market in allMarketData.Data)
            {
                result.Add(market.Symbol,new MarketData()
                {
                    AskPrice = market.AskPrice,
                    AskQty = market.AskQuantity,
                    BidPrice = market.BidPrice,
                    BidQty = market.BidQuantity,
                    Ticker = market.Symbol
                });
            }

            _marketsData = result;
            return result;
        }

        public PriceData GetPriceMarketData()
        {
            return _marketPriceData;
        }

        public async Task<PriceData> RefreshPriceMarketData()
        {
            var markets = await _api.GetAllPricesAsync();

            PriceData result = new PriceData();
            foreach (var market in markets.Data)
            {
                result.Data.Add(market.Symbol, market.Price);
            }

            _marketPriceData = result;
            return result;
        }

        public async Task<List<string>> GetOpenTrades(List<string> symbols)
        {
            List <string> orderIds = new List<string>();
            foreach (var symbol in symbols)
            {
                var response = await _api.GetOpenOrdersAsync(symbol);
                if (response != null && response.Success && response.Data.Any())
                {
                    orderIds.AddRange(response.Data.Select(x => x.OrderId.ToString()).ToList());
                }
                
            }
            return orderIds;
        }

        public async Task<ApiResponse> GetOpenTrades()
        {
            var response = await _api.GetOpenOrdersAsync();
            
            return new ApiResponse()
            {
                Data = response.Data,
                Error = response.Error?.Message,
                Success = response.Success
            };
        }

        public async Task<List<string>> GetOpenTransfers()
        {
            List<string> orderIds = new List<string>();
            var response = await _api.GetWithdrawHistoryAsync(string.Empty);
            if (response.Success && response.Data.List.Any())
            {
                orderIds.AddRange(response.Data.List.Where(x => x.Status == WithdrawalStatus.Processing ||
                                                                x.Status == WithdrawalStatus.AwaitingApproval).Select(y => y.Id).ToList());
            }

            return orderIds;
        }

        public async Task<Dictionary<string,decimal>> GetBalances()
        {
            var accountInfo = await _api.GetAccountInfoAsync();

            Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            foreach (var balance in accountInfo.Data.Balances)
            {
                result.Add(balance.Asset, balance.Free);
            }
            return result;
        }

        public async Task<string> GetDepositAddress(string currency)
        {
            var result = await _api.GetDepositAddressAsync(currency);
            return result.Data.Address;
        }

        public async Task<ApiResponse> CreateBuyLimitOrder(string symbol, decimal quantity, decimal price)
        {
            try
            {
                symbol = symbol.Replace("/", "").Replace("\\", "");
                var tickerRules = await GetTickerRules();
                if (!tickerRules.Rules.ContainsKey(symbol))
                {
                    return new ApiResponse()
                    {
                        Success = false,
                        Error = $"Ticker {symbol} not found."
                    };
                }

                var safePrice = Math.Round(price, tickerRules.Rules[symbol].MinPriceDecimals);
                var safeQuantity = Math.Round(quantity, tickerRules.Rules[symbol].MinAmountDecimals);
                var response = await _api.PlaceOrderAsync(symbol, OrderSide.Buy, OrderType.Limit, safeQuantity, price: safePrice,timeInForce:TimeInForce.GoodTillCancel);
                return new ApiResponse()
                {
                    Success = response.Success,
                    Data = response.Data,
                    Error = response.Error?.Message
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse> CreateBuyMarketOrder(string symbol, decimal quantity, decimal lastKnownPrice = 0)
        {
            try
            {
                symbol = symbol.Replace("/", "").Replace("\\", "");
                var tickerRules = await GetTickerRules();
                if (!tickerRules.Rules.ContainsKey(symbol))
                {
                    return new ApiResponse()
                    {
                        Success = false,
                        Error = $"Ticker {symbol} not found."
                    };
                }

                var safePrice = Math.Round(lastKnownPrice, tickerRules.Rules[symbol].MinPriceDecimals);
                var safeQuantity = Math.Round(quantity, tickerRules.Rules[symbol].MinAmountDecimals);
                var response = await _api.PlaceOrderAsync(symbol, OrderSide.Buy, OrderType.Market, safeQuantity);
                return new ApiResponse()
                {
                    Success = response.Success,
                    Data = response.Data,
                    Error = response.Error?.Message
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse> CreateSellMarketOrder(string symbol, decimal quantity, decimal lastKnownPrice = 0)
        {
            try
            {
                symbol = symbol.Replace("/", "").Replace("\\", "");
                var tickerRules = await GetTickerRules();
                if (!tickerRules.Rules.ContainsKey(symbol))
                {
                    return new ApiResponse()
                    {
                        Success = false,
                        Error = $"Ticker {symbol} not found."
                    };
                }

                var safePrice = Math.Round(lastKnownPrice, tickerRules.Rules[symbol].MinPriceDecimals);
                var safeQuantity = Math.Round(quantity, tickerRules.Rules[symbol].MinAmountDecimals);
                var response = await _api.PlaceOrderAsync(symbol, OrderSide.Sell, OrderType.Market, safeQuantity);
                return new ApiResponse()
                {
                    Success = response.Success,
                    Data = response.Data,
                    Error = response.Error?.Message
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse> CreateSellLimitOrder(string symbol, decimal quantity, decimal price)
        {
            try
            {
                symbol = symbol.Replace("/", "").Replace("\\", "");
                var tickerRules = await GetTickerRules();
                if (!tickerRules.Rules.ContainsKey(symbol))
                {
                    return new ApiResponse()
                    {
                        Success = false,
                        Error = $"Ticker {symbol} not found."
                    };
                }

                var safePrice = Math.Round(price, tickerRules.Rules[symbol].MinPriceDecimals);
                var safeQuantity = Math.Round(quantity, tickerRules.Rules[symbol].MinAmountDecimals);
                var response = await _api.PlaceOrderAsync(symbol, OrderSide.Sell, OrderType.Limit, safeQuantity, price: safePrice, timeInForce: TimeInForce.GoodTillCancel);
                return new ApiResponse()
                {
                    Success = response.Success,
                    Data = response.Data,
                    Error = response.Error?.Message
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<bool> CreateWithdraw(string currency, decimal amount, string address)
        {
            try
            {
                var tickerRules = await GetTickerRules();
                var safeQuantity = Math.Round(amount, tickerRules.Rules[$"{currency}BTC"].MinAmountDecimals);
                var response = await _api.WithdrawAsync(currency, address, safeQuantity);
                return response.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> Transfer(string currency, decimal amount, string address)
        {
            try
            {
                var tickerRules = await GetTickerRules();
                var safeQuantity = Math.Round(amount, tickerRules.Rules[$"{currency}BTC"].MinAmountDecimals);
                var response = await _api.WithdrawAsync(currency, address, safeQuantity);
                return response.Data.Id;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<ApiResponse> CancelOpenOrder(string symbol, long orderId)
        {
            try
            {
                var response = await _api.CancelOrderAsync(symbol, orderId);
                return new ApiResponse()
                {
                    Success = response.Success,
                    Data = response.Data,
                    Error = response.Error?.Message
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse> CreateBuyStopLimit(string symbol, decimal quantity, decimal price, decimal stopLimitPrice)
        {
            try
            {
                symbol = symbol.Replace("/", "").Replace("\\", "");
                var tickerRules = await GetTickerRules();
                if (!tickerRules.Rules.ContainsKey(symbol))
                {
                    return new ApiResponse()
                    {
                        Success = false,
                        Error = $"Ticker {symbol} not found."
                    };
                }

                var safePrice = Math.Round(price, tickerRules.Rules[symbol].MinPriceDecimals);
                var safePriceStop = Math.Round(stopLimitPrice, tickerRules.Rules[symbol].MinPriceDecimals);
                var safeQuantity = Math.Round(quantity, tickerRules.Rules[symbol].MinAmountDecimals);
                var response = await _api.PlaceOrderAsync(symbol, OrderSide.Buy, OrderType.StopLossLimit,
                    safeQuantity, price: safePrice, timeInForce: TimeInForce.GoodTillCancel, stopPrice: safePriceStop);
                return new ApiResponse()
                {
                    Success = response.Success,
                    Data = response.Data,
                    Error = response.Error?.Message
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse> CreateSellStopLimit(string symbol, decimal quantity, decimal price, decimal stopLimitPrice)
        {
            try
            {
                symbol = symbol.Replace("/", "").Replace("\\", "");
                var tickerRules = await GetTickerRules();
                if (!tickerRules.Rules.ContainsKey(symbol))
                {
                    return new ApiResponse()
                    {
                        Success = false,
                        Error = $"Ticker {symbol} not found."
                    };
                }

                var safePrice = Math.Round(price, tickerRules.Rules[symbol].MinPriceDecimals);
                var safePriceStop = Math.Round(stopLimitPrice, tickerRules.Rules[symbol].MinPriceDecimals);
                var safeQuantity = Math.Round(quantity, tickerRules.Rules[symbol].MinAmountDecimals);
                var response = await _api.PlaceOrderAsync(symbol, OrderSide.Sell, OrderType.StopLossLimit, 
                    safeQuantity, price: safePrice, timeInForce: TimeInForce.GoodTillCancel, stopPrice: safePriceStop);
                return new ApiResponse()
                {
                    Success = response.Success,
                    Data = response.Data,
                    Error = response.Error?.Message
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<List<SymbolVolume>> GetMarketVolume()
        {
            List<SymbolVolume> result = new List<SymbolVolume>();
            var response = await _api.Get24HPricesListAsync();
            if (!response.Success)
            {
                return null;
            }

            foreach (var market in response.Data)
            {
                result.Add(new SymbolVolume()
                {
                    Symbol = market.Symbol,
                    Volume = market.Volume * market.WeightedAveragePrice
                });
            }
            return result;
        }

        #region tickerRules
        public async Task<TickerRules> GetTickerRules()
        {
            if (_tickerRules == null)
            {
                return await RefreshTickerRules();
            }

            return _tickerRules;
        }

        public async Task<TickerRules> RefreshTickerRules()
        {
            var result = new TickerRules { Rules = new Dictionary<string, Rule>() };

            var response = await _api.GetExchangeInfoAsync();
            foreach (var market in response.Data.Symbols)
            {
                    result.Rules.Add(
                    market.Name,
                    new Rule(GetNumDecimalPlaces(market.LotSizeFilter.MinQuantity.ToString()),
                        GetNumDecimalPlaces(market.PriceFilter.MinPrice.ToString())));
            }

            _tickerRules = result;
            return result;
        }

        #endregion

        public int GetMaxTasks()
        {
            return 5;
        }

        public int GetResponseTime()
        {
            return 450;
        }

        public string GetExchangeName()
        {
            return "Binance";
        }

        public MarketFee GetMarketFee()
        {
            return new MarketFee()
            {
                BuyingFee = 0.001m,
                SellingFee = 0.001m
            };
        }

        public Task<Dictionary<string, Currency>> GetCurrencies()
        {
            if (_currencies != null)
            {
                return Task.FromResult(_currencies);
            }

            BinanceFees serviceFees = new BinanceFees();
            var currencies = serviceFees.FetchWebSite();
            _currencies = new Dictionary<string, Currency>();

            foreach (var currency in currencies)
            {
                _currencies.Add(currency.assetCode, new Currency()
                {
                    MinWithdraw = decimal.Parse(currency.minProductWithdraw),
                    Symbol = currency.assetCode,
                    Name = currency.assetName,
                    WithdrawFee = System.Convert.ToDecimal(currency.transactionFee)
                });
            }

            return Task.FromResult(_currencies);
        }

        public async Task<Dictionary<string, CryptoBalance>> GetFunds(bool force = false)
        {
            if (force || _lastGetFundRefresh.AddMinutes(1) < DateTime.Now)
            {
                Console.WriteLine($"Get funds force = {force}");
                var result = new Dictionary<string, CryptoBalance>();

                var response = await _api.GetAccountInfoAsync();
                foreach (var balance in response.Data.Balances)
                {
                    result.Add(balance.Asset, new CryptoBalance()
                    {
                        Symbol = balance.Asset,
                        Available = balance.Free,
                        CanTradeOrDeposit = response.Data.CanDeposit && response.Data.CanTrade && response.Data.CanWithdraw
                    });
                }

                _lastGetFundRefresh = DateTime.Now;
                _funds = result;
            }

            return _funds;
        }

        private int GetNumDecimalPlaces(string argument)
        {
            decimal number = decimal.Parse(Remove0FromRight(argument));
            return BitConverter.GetBytes(decimal.GetBits(number)[3])[2];
        }

        private string Remove0FromRight(string argument)
        {
            if (argument.EndsWith("0"))
            {
                return Remove0FromRight(argument.Substring(0, argument.Length - 1));
            }
            return argument;
        }
    }
}
