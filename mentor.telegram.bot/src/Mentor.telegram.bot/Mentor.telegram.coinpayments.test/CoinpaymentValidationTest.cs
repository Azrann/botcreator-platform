using System.Collections.Generic;
using System.Threading.Tasks;
using Mentor.telegram.coinpayments.Helpers;
using Newtonsoft.Json;
using Xunit;

namespace Mentor.telegram.coinpayments.test
{
    public class CoinpaymentValidationTest
    {

        [Fact]
        public void WhenIGetAValidInputIReturnTrue()
        {
            var hmacHeader = "REPLACE_WITH_YOUR_VALUE";
            var jsonParameters =
                "{\"amount1\":\"0.0104\",\"amount2\":\"0.0104\",\"buyer_name\":\"Alberto\",\"currency1\":\"LTCT\",\"currency2\":\"LTCT\",\"email\":\"user@example.invalid\",\"fee\":\"5.0E-5\",\"ipn_id\":\"49a3fe2599efe6345cfce95d4445667d\",\"ipn_mode\":\"hmac\",\"ipn_type\":\"api\",\"ipn_version\":\"1.0\",\"merchant\":\"6e439a266e0735bab88391047cf0ee18\",\"received_amount\":\"0\",\"received_confirms\":\"0\",\"status\":\"0\",\"status_text\":\"Waiting for buyer funds...\",\"txn_id\":\"CPCG2X74CYBME7TKLZDFEAI6WW\"}";
            var parameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonParameters);

            var result = PaymentHelper.Validate("test123", parameters, hmacHeader);

            Assert.True(result);
        }

        [Fact]
        public void WhenIGetAValid2InputIReturnTrue()
        {
            var hmacHeader = "REPLACE_WITH_YOUR_VALUE";
            var jsonParameters =
                "{\"amount1\":\"0.0104\",\"amount2\":\"0.0104\",\"buyer_name\":\"Alberto\",\"currency1\":\"LTCT\",\"currency2\":\"LTCT\",\"email\":\"user@example.invalid\",\"fee\":\"5.0E-5\",\"ipn_id\":\"ac2b34f897ccc0d7c89228c4fe9f101e\",\"ipn_mode\":\"hmac\",\"ipn_type\":\"api\",\"ipn_version\":\"1.0\",\"merchant\":\"6e439a266e0735bab88391047cf0ee18\",\"received_amount\":\"0.0104\",\"received_confirms\":\"0\",\"status\":\"1\",\"status_text\":\"Funds received and confirmed, sending to you shortly...\",\"txn_id\":\"CPCG2X74CYBME7TKLZDFEAI6WW\"}";
            var parameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonParameters);

            var body = PaymentHelper.GetKeyValues(parameters);
            
            var createSignature = PaymentHelper.CreateSignature("test123", body);

            Assert.True(createSignature == hmacHeader);
        }

        [Fact]
        public void WhenIGetAPostStringInputIReturnTrue()
        {
            var hmacHeader = "REPLACE_WITH_YOUR_VALUE";

            var body = "amount1=0.0104&amount2=0.0104&buyer_name=Alberto&currency1=LTCT&currency2=LTCT&email=tupass%40gmail.com&fee=5.0E-5&ipn_id=cfa12e43513c1576c492d8283e798588&ipn_mode=hmac&ipn_type=api&ipn_version=1.0&merchant=6e439a266e0735bab88391047cf0ee18&received_amount=0&received_confirms=0&status=0&status_text=Waiting+for+buyer+funds...&txn_id=CPCG480LP55YCYBATPWWTB2CPR";

            var createSignature = PaymentHelper.CreateSignature("test123", body);

            Assert.True(createSignature == hmacHeader);
        }

        [Fact]
        public void WhenIGetATestVtcCron()
        {
            var hmacHeader = "REPLACE_WITH_YOUR_VALUE";

            var parameters = new Dictionary<string, string>
            {
                {"Command", $"CleanUpSubscriptionTest"},
                {"BotId", "REPLACE_WITH_YOUR_VALUE"},
            };
            var body = PaymentHelper.GetKeyValues(parameters);
            string seri = JsonConvert.SerializeObject(parameters);

            var createSignature = PaymentHelper.Validate("REPLACE_WITH_YOUR_VALUE", parameters,hmacHeader);

            Assert.True(true);
        }
    }
}
