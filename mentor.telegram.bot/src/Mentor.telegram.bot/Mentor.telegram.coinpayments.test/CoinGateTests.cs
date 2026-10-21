using System;
using System.Threading.Tasks;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Mentor.telegram.coinpayments.test
{
    public class CoinGateTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CoinGateTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private const string PrivateApiKey = "REPLACE_WITH_YOUR_VALUE";
        public const string Url = "http://paymebot.getsandbox.com";

      
        [Fact]
        public async Task WhenISendACoinGatepaymentRequestIDontGetAnyErrors()
        {
            Random rnd = new Random();
            double amountLtc = rnd.Next(1999) / 100000.1;
            CoinGateApi api = new CoinGateApi(PrivateApiKey);
           
            var transaction = new CreateOrder()
            {
                order_id = Guid.NewGuid().ToString(),
                price_amount = amountLtc,
                price_currency = "LTC",
                receive_currency = "LTC",
                callback_url = "https://738dc524.ngrok.io/api/coingate/abcabc",
                //CallBackUrl = "http://paymebot.getsandbox.com",
                title = "CRZ TEST COURSE",
                description = "Test getting callback and Order",
                token = "REPLACE_WITH_YOUR_VALUE" //should match IPN secret 
            };

            var result = await api.SendPayment(transaction, Guid.Empty);
            Console.Write("test");
        }

        [Fact]
        public async Task WhenICreateAPaymentInfoAndSendACoinGatepaymentRequestIDontGetAnyErrors()
        {
            Random rnd = new Random();
            double amountLtc = rnd.Next(1999) / 100000.1;
            CoinGateApi api = new CoinGateApi(PrivateApiKey);

            var transaction = new CreateOrder()
            {
                order_id = Guid.NewGuid().ToString(),
                price_amount = amountLtc,
                price_currency = "LTC",
                receive_currency = "LTC",
                callback_url = "https://738dc524.ngrok.io/api/coingate/abcabc",
                //CallBackUrl = "http://paymebot.getsandbox.com",
                title = "CRZ TEST COURSE",
                description = "Test getting callback and Order",
                token = "REPLACE_WITH_YOUR_VALUE" //should match IPN secret 
            };

            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IPaymentInfoService service = new PaymentInfoService(connection);
            
            PaymentInfo paymentInfo = new PaymentInfo()
            {
                Id = new Guid(transaction.order_id),
                BotId = "abcabc",
                UserId = "123123",
                PaymentStatus = 0,
                TransactionId = "",
                WalletAddress = "",
                OriginalAmount = (decimal)transaction.price_amount,
                OriginalCurrency = "LTC",
                ItemName = "CRZ TEST COURSE",
                ChatId = 321321,
                Provider = PaymentProvider.CoingGate,
                SubscriptionMonths = (short)3,
                SubscriptionDays = (short)0,
            };

            service.AddPaymentInfo(paymentInfo);

            var result = await api.SendPayment(transaction, Guid.Empty);
            Console.Write("test");
        }

        [Fact]
        public void ICanDeserializeResponse()
        {
            var response =
                @"REPLACE_WITH_YOUR_VALUE";
            try
            {
                var result = JsonConvert.DeserializeObject<CreateOrderResponse>(response);
            }
            catch (Exception ex)
            {
                _testOutputHelper.WriteLine(ex.ToString());
                throw;
            }
        }

        [Fact]
        public async Task ICanRetrieveAnOrderById()
        {
            CoinGateApi api = new CoinGateApi(PrivateApiKey);
            var result = await api.GetOrderById("112644", Guid.NewGuid());
            Assert.False(result == null);
        }
    }
}
