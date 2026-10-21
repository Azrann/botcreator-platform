using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Mentor.telegram.coinpayments.Helpers;
using Xunit;

namespace Mentor.telegram.coinpayments.test
{
    public class CoinpaymentTests
    {
        private const string PublicApiKey = "REPLACE_WITH_YOUR_VALUE";
        private const string PrivateApiKey = "REPLACE_WITH_YOUR_VALUE";
        public const string Url = "http://paymebot.getsandbox.com";

        [Fact]
        public async Task WhenISendACoinpaymentRequestIDontGetAnyErrors()
        {
            Random rnd = new Random();
            var amountLtc = rnd.Next(1999) / 100000;
            CoinApi api = new CoinApi(PublicApiKey, PrivateApiKey);
            /* TODO check the way we build payment info to know which parameters should we send
             EXAMPLE invoice = PaymentInfo.Id , */
            var transaction = new ReceiveTransaction()
            {
                Amount = amountLtc,
                Currency1 = "LTCT",
                Currency2 = "LTCT",
                BuyerEmail = "user@example.invalid",
                BuyerName = "Alberto",
                Cmd = "create_transaction",
                CallBackUrl = "https://mentortelegrambotapi20180703105533.azurewebsites.net/",
                //CallBackUrl = "http://paymebot.getsandbox.com",
                ItemName = "CRZ TEST COURSE"
            };

            var result = await api.ReceiveAsync(transaction, Guid.Empty);
            Console.Write("test");
        }

        [Fact]
        public async Task WhenISendACoinpaymentWithSendPaymentIDontGetAnyErrors()
        {
            Random rnd = new Random();
            var amountLtc = rnd.Next(1,1999) / 100000.0;
            CoinApi api = new CoinApi(PublicApiKey, PrivateApiKey);
            /* TODO check the way we build payment info to know which parameters should we send
             EXAMPLE invoice = PaymentInfo.Id , */
            var transaction = new ReceiveTransaction()
            {
                Amount = 0.003213,
                Currency1 = "LTCT",
                Currency2 = "LTCT",
                BuyerEmail = "user@example.invalid",
                BuyerName = "Alberto",
                Cmd = "create_transaction",
                CallBackUrl = "https://mentortelegrambotapi20180703105533.azurewebsites.net/",
                //CallBackUrl = "http://paymebot.getsandbox.com",
                ItemName = "CRZ TEST COURSE"
            };

            var result = await api.SendPayment(transaction, Guid.NewGuid());
            Console.Write("test");
        }

        [Fact]
        public void CalculatedHmacAreTheSame()
        {
            var query =
                "amount=0.003213&buyer_email=tupass%40gmail.com&buyer_name=Alberto&cmd=create_transaction&currency1=LTCT&currency2=LTCT&ipn_url=https%3A%2F%2Fmentortelegrambotapi20180703105533.azurewebsites.net%2F&item_name=CRZ+TEST+COURSE&key=REPLACE_WITH_YOUR_VALUE&version=1";
            var calculatedHmac =
                "REPLACE_WITH_YOUR_VALUE";

            var hmac = PaymentHelper.CreateSignature(PrivateApiKey, query);
           Assert.True(hmac == calculatedHmac);
        }

        [Fact]
        public async Task SendPaymentWithHttpHelper()
        {
            var query =
                "amount=0.003213&buyer_email=tupass%40gmail.com&buyer_name=Alberto&cmd=create_transaction&currency1=LTCT&currency2=LTCT&ipn_url=https%3A%2F%2Fmentortelegrambotapi20180703105533.azurewebsites.net%2F&item_name=CRZ+TEST+COURSE&key=REPLACE_WITH_YOUR_VALUE&version=1";
            var calculatedHmac =
                "REPLACE_WITH_YOUR_VALUE";

            var result = await PostAsyncHmac(query, Url, "", calculatedHmac);

            var deserializeContent = await result.Content.ReadAsStringAsync();

            Assert.True(result.StatusCode == HttpStatusCode.OK);
        }

        public static async Task<HttpResponseMessage> PostAsyncHmac(string queryString, string baseUrl, string requestUri, string hmac)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                if (!string.IsNullOrEmpty(hmac))
                {
                    client.DefaultRequestHeaders.Add("HMAC", hmac);
                }

                var request = new StringContent(queryString);

                request.Headers.Clear();

                request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                return await client.PostAsync(requestUri, request);
            }
        }
    }
}
