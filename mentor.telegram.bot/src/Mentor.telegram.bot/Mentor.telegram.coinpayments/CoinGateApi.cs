using System;
using System.Threading.Tasks;
using Core.Common;
using Core.Common.Extensions;
using Newtonsoft.Json;

namespace Mentor.telegram.coinpayments
{
    public class CoinGateApi
    {
        private readonly string _privateKey;

        //public const string Url = "https://api-sandbox.coingate.com/v2/";
        public const string Url = "https://api.coingate.com/v2/";
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CoinApi));

        public CoinGateApi(string privateKey)
        {
            //_privateKey = "REPLACE_WITH_YOUR_VALUE";
            _privateKey = privateKey;
        }

        public async Task<CreateOrderResponse> SendPayment(CreateOrder order, Guid correlationId)
        {
            try
            {
                var response = await HttpClientHelper.PostAsyncToken(order, Url, "orders",_privateKey);
                CreateOrderResponse result;
                Log.LogInf($"CoinGate : ReceiveAsync result received {JsonConvertExt.SerializeObject(response.StatusCode)}", correlationId);

                try
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<CreateOrderResponse>(stringResponse);
                    return result;
                }
                catch (Exception ex)
                {
                    Log.LogErr($"CoinGate : Error retrieving request {JsonConvertExt.SerializeObject(ex)}", correlationId);
                }

                return new CreateOrderResponse()
                {
                    reason = $"Error retrieving request received {response.StatusCode}"
                };
            }
            catch (Exception e)
            {
                return new CreateOrderResponse()
                {
                    reason = $"Error retrieving request received {JsonConvertExt.SerializeObject(e)}"
                };
            }
        }

        public async Task<CoinGateOrder> GetOrderById(string orderId, Guid correlationId)
        {
            try
            {
                var response = await HttpClientHelper.GetAsyncToken(Url, $"orders/{orderId}", _privateKey);
                CoinGateOrder result;
                Log.LogInf($"CoinPay : GetOrderById result received {JsonConvertExt.SerializeObject(response.StatusCode)}", correlationId);

                try
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<CoinGateOrder>(stringResponse);
                    return result;
                }
                catch (Exception ex)
                {
                    Log.LogErr($"CoinGate : Error deserializing request {JsonConvertExt.SerializeObject(ex)}", correlationId);
                }

                return null;
            }
            catch (Exception ex)
            {
                Log.LogErr($"CoinGate : Error retrieving request {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return null;
            }
        }
    }
}
