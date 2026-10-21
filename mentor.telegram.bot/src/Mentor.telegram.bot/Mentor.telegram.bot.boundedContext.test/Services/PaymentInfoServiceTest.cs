using System;
using System.Linq;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Xunit;

namespace Mentor.telegram.bot.boundedContext.test.Services
{
    public class PaymentInfoServiceTest
    {
        [Fact]
        public void WhenIAddAPaymentInfoICanRetrieveItByBotId()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IPaymentInfoService service = new PaymentInfoService(connection);

            var paymentInfo = GetPaymentInfo();
            
            service.AddPaymentInfo(paymentInfo);

            var result = service.GetPaymentInfosByBotId(paymentInfo.BotId);
            Assert.True(result.Count() == 1);
        }

        [Fact]
        public void WhenIAddAPaymentInfoICanRetrieveIt()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IPaymentInfoService service = new PaymentInfoService(connection);

            var paymentInfo = GetPaymentInfo(PaymentProvider.Stripe);

            service.AddPaymentInfo(paymentInfo);

            var result = service.GetPaymentInfos(paymentInfo.BotId, paymentInfo.UserId);
            Assert.True(result.Count() == 1);
            Assert.True(result.First().Provider == PaymentProvider.Stripe);
        }

        [Fact]
        public void WhenIAddAPaymentInfoICanRetrieveItByTransactionId()
        {
           // var connection = @"REPLACE_WITH_YOUR_VALUE";

            var connection = @"REPLACE_WITH_YOUR_VALUE";
            IPaymentInfoService service = new PaymentInfoService(connection);

            var paymentInfo = GetPaymentInfo();

            service.AddPaymentInfo(paymentInfo);

            var result = service.GetPaymentInfoByTransactionId(paymentInfo.TransactionId);
            Assert.True(result.Id == paymentInfo.Id);
            Assert.True(result.OriginalAmount == paymentInfo.OriginalAmount);
            Assert.True(result.PaymentStatus == paymentInfo.PaymentStatus);
            Assert.True(result.Tag == paymentInfo.Tag);
            Assert.True(result.Username == paymentInfo.Username);
        }
        
        [Fact]
        public void WhenIUpdateStatusICanRetrieveIt()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IPaymentInfoService service = new PaymentInfoService(connection);
            var paymentInfo = GetPaymentInfo();

            service.AddPaymentInfo(paymentInfo);
            service.UpdatePaymentInfoStatus(paymentInfo.TransactionId,100);
            
            var result = service.GetPaymentInfoByTransactionId(paymentInfo.TransactionId);
            Assert.True(result.PaymentStatus == 100);
        }

        [Fact]
        public void WhenIUpdatePaymentInfoICanRetrieveIt()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IPaymentInfoService service = new PaymentInfoService(connection);
            var paymentInfo = GetPaymentInfo();

            service.AddPaymentInfo(paymentInfo);
            paymentInfo.PaymentStatus = 98;
            paymentInfo.ReceivedAmount = 3.2m;
            paymentInfo.ReceivedConfirms = 1;
            paymentInfo.CryptoTxId = "XCS1";
            paymentInfo.Fee = 0.2m;

            service.UpdatePaymentInfo(paymentInfo);

            var result = service.GetPaymentInfoByTransactionId(paymentInfo.TransactionId);
            Assert.True(result.PaymentStatus == paymentInfo.PaymentStatus);
            Assert.True(result.ReceivedAmount == paymentInfo.ReceivedAmount);
            Assert.True(result.ReceivedConfirms == paymentInfo.ReceivedConfirms);
            Assert.True(result.CryptoTxId == paymentInfo.CryptoTxId);
            Assert.True(result.Fee == paymentInfo.Fee);
        }

        [Fact]
        public void WhenIGetActivePaymentsIGet1Result()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IPaymentInfoService service = new PaymentInfoService(connection);
            var paymentInfo = GetPaymentInfo();

            service.AddPaymentInfo(paymentInfo);
            var result = service.GetActivePaymentInfos(paymentInfo.BotId, paymentInfo.UserId);
            Assert.True(result.Any());
        }

        [Fact]
        public void WhenIGetActivePaymentsIGet0Result()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IPaymentInfoService service = new PaymentInfoService(connection);
            var paymentInfo = GetPaymentInfo();

            service.AddPaymentInfo(paymentInfo);
            service.UpdatePaymentInfoStatus(paymentInfo.TransactionId, 100);

            var result = service.GetActivePaymentInfos(paymentInfo.BotId, paymentInfo.UserId);
            Assert.True(!result.Any());
        }

        public PaymentInfo GetPaymentInfo(PaymentProvider provider = PaymentProvider.Coinpayments)
        {
            Random rnd = new Random();
            return new PaymentInfo()
            {
                BotId = $"{Guid.NewGuid().ToString().Replace("-","")}{Guid.NewGuid().ToString().Replace("-", "")}",
                Id = Guid.NewGuid(),
                ChatId = rnd.Next(1000000),
                PaymentStatus = 0,
                TransactionId = Guid.NewGuid().ToString().Replace("-", ""),
                Provider = provider,
                OriginalAmount = rnd.Next(999) * 0.001m,
                OriginalCurrency = "LTC",
                Tag = "TradersPremium",
                UserId = $"UserId{rnd.Next(99999999)}",
                BuyerAmount = rnd.Next(99) * 0.001m,
                BuyerCurrency =  "XRP",
                Fee = 0.001m,
                ReceivedAmount = rnd.Next(40) * 0.001m,
                CryptoTxId = Guid.NewGuid().ToString().Replace("-", ""),
                SubscriptionMonths = 1,
                SubscriptionDays = 2,
                Username ="REPLACE_WITH_YOUR_VALUE"
            };
        }
    }
}
