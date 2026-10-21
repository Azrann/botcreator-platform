using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Common;
using Core.Common.Extensions;
using Flurl.Http;
using Flurl.Http.Content;
using Mentor.telegram.coinpayments.Helpers;
using Newtonsoft.Json;

namespace Mentor.telegram.coinpayments
{
    public class CoinApi
    {
        public const string Url = "https://www.coinpayments.net/api.php";
        public readonly string publicApiKey;
        public readonly string privateApiKey;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CoinApi));

        public CoinApi(string publicApiKey,string privateApiKey)
        {
            this.publicApiKey = publicApiKey;
            this.privateApiKey = privateApiKey;
        }

        public async Task<ReceiveTransactionResponse> ReceiveAsync(ReceiveTransaction transaction, Guid correlationId)
        {
            try
            {
           
            var result = await SendRequestAsync(transaction,correlationId);
                Log.LogInf($"CoinApi : ReceiveAsync result received {JsonConvertExt.SerializeObject(result.StatusCode)}", correlationId);
                if (result.IsSuccessStatusCode)
                {
                    var str = await result.Content.ReadAsStringAsync();
                    try
                    {
                        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ReceiveTransactionResponse>(str);
                        return data;
                    }
                    catch
                    {
                        var data = JsonConvert.DeserializeObject<TransactionException>(str);
                        Log.LogErr($"CoinApi : Error retrieving request {JsonConvertExt.SerializeObject(data.Error)}", correlationId);
                    }

                }
                return new ReceiveTransactionResponse()
                {
                    Error = $"Error retrieving request received {result.StatusCode}"
                };
            }
            catch (Exception e)
            {
                return new ReceiveTransactionResponse()
                {
                    Error = $"Error retrieving request received {JsonConvertExt.SerializeObject(e)}"
                };
            }
        }

        public async Task<ReceiveTransactionResponse> SendPayment(ReceiveTransaction transaction, Guid correlationId)
        {
            try
            {

                var result = await SendRequestAsync2(transaction, correlationId);
                Log.LogInf($"CoinApi : ReceiveAsync result received {JsonConvertExt.SerializeObject(result.StatusCode)}", correlationId);
                if (result.IsSuccessStatusCode)
                {
                    var str = await result.Content.ReadAsStringAsync();
                    try
                    {
                        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ReceiveTransactionResponse>(str,new JsonSerializerSettings() {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                        return data;
                    }
                    catch
                    {
                        var data = JsonConvert.DeserializeObject<TransactionException>(str);
                        Log.LogErr($"CoinApi : Error retrieving request {JsonConvertExt.SerializeObject(data.Error)}", correlationId);
                    }

                }
                return new ReceiveTransactionResponse()
                {
                    Error = $"Error retrieving request received {result.StatusCode}"
                };
            }
            catch (Exception e)
            {
                return new ReceiveTransactionResponse()
                {
                    Error = $"Error retrieving request received {JsonConvertExt.SerializeObject(e)}"
                };
            }
        }

        private  async Task<HttpResponseMessage> SendRequestAsync(BaseTransaction transaction, Guid correlationId)
        {
            Log.LogInf($"CoinApi : Encoding request.", correlationId);
            transaction.Key = publicApiKey;
            var query = string.Empty.GetUrl(transaction.ToKeyValuePair());

            byte[] keyBytes = Encoding.UTF8.GetBytes(privateApiKey);
            byte[] postBytes = Encoding.UTF8.GetBytes(query);
            var hmacsha512 = new System.Security.Cryptography.HMACSHA512(keyBytes);
            string hmac = BitConverter.ToString(hmacsha512.ComputeHash(postBytes)).Replace("-", string.Empty);
            Log.LogInf($"CoinApi : HMAC = {hmac}", correlationId);
            Log.LogInf($"CoinApi : QUERY = {query}", correlationId);
            return await Url.WithHeaders(new Dictionary<string, string> { ["HMAC"] = hmac, ["Content-Type"] = "application/x-www-form-urlencoded" }).PostAsync(new CapturedUrlEncodedContent(query));
        }

        private async Task<HttpResponseMessage> SendRequestAsync2(BaseTransaction transaction, Guid correlationId)
        {
            Log.LogInf($"CoinApi2 : Encoding request.", correlationId);
            transaction.Key = publicApiKey;
            var query = GetKeyValues(transaction.ToKeyValuePairDictionary());
            
            string hmac = PaymentHelper.CreateSignature(privateApiKey, query);

            Log.LogInf($"CoinApi : HMACF = {hmac}", correlationId);
            Log.LogInf($"CoinApi : QUERYF = {query}", correlationId);
            return await HttpClientHelper.PostAsyncHmac(query, Url, string.Empty, hmac);
            //return await Url.WithHeaders(new Dictionary<string, string> { ["HMAC"] = hmac, ["Content-Type"] = "application/x-www-form-urlencoded" }).PostAsync(new CapturedUrlEncodedContent(query));
        }

        public  async Task<SendTransactionResponse> TransferAsync(SendTransaction transaction, Guid correlationId)
        {
            var result = await SendRequestAsync(transaction, correlationId);

            if (result.IsSuccessStatusCode)
            {
                var str = await result.Content.ReadAsStringAsync();
                try
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<SendTransactionResponse>(str);
                    return data;
                }
                catch
                {
                    var data = JsonConvert.DeserializeObject<TransactionException>(str);
                    throw data;
                }

            }
            else throw new TransactionException { Error = result.StatusCode.ToString() };
        }


        public  async Task<ReceiveInfoTransaction> GetInfoAsync(ReceiveInfoTransaction transaction, Guid correlationId)
        {
            var result = await SendRequestAsync(transaction, correlationId);
            if (result.IsSuccessStatusCode)
            {
                var str = await result.Content.ReadAsStringAsync();
                try
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ReceiveInfoTransaction>(str);
                    return data;
                }
                catch
                {
                    var data = JsonConvert.DeserializeObject<TransactionException>(str);
                    throw data;
                }

            }
            else throw new TransactionException { Error = result.StatusCode.ToString() };
        }

        public static string GetKeyValues(Dictionary<string, string> keyValues)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (var collectionKey in keyValues.Keys.OrderBy(x => x))
            {
                if (first)
                {
                    // TODO WebUtility.UrlEncode can also work?
                    sb.Append($"{Uri.EscapeDataString(collectionKey)}={Uri.EscapeDataString(keyValues[collectionKey])}");
                    first = false;
                }
                else
                {
                    sb.Append($"&{Uri.EscapeDataString(collectionKey)}={Uri.EscapeDataString(keyValues[collectionKey])}");
                }
            }

            return sb.ToString().Replace("%20", "+");
        }
    }
}
