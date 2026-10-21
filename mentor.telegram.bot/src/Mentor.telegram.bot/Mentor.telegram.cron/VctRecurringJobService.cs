using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Common;
using Hangfire;
using Hangfire.Console;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using Mentor.telegram.coinpayments.Helpers;
using Newtonsoft.Json;

namespace Mentor.telegram.cron
{
    public class VctRecurringJobService
    {
        private const string Secret = "REPLACE_WITH_YOUR_VALUE";

        private const string BaseUrl =
            "https://verifycryptogroupbotify.azurewebsites.net/api/cron/";

        [RecurringJob("1 7,19 * * *",RecurringJobId = "SendCleanUpSubscriptionGold")]
        [Queue("jobs")]
        public void SendCleanUpSubscriptionGold(PerformContext context)
        {
            try
            {
                var response = SendCleanUpSubscriptionEvent("Gold").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendCleanUpSubscriptionGold Error {JsonConvert.SerializeObject(ex)}");
            }              

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendCleanUpSubscriptionGold finished ...");
        }

        [RecurringJob("1/5 * * * *", RecurringJobId = "Test")]
        [Queue("jobs")]
        public void Test(PerformContext context)
        {
            try
            {
                //var response = SendCleanUpSubscriptionEvent("Test").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"Test Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} Test finished ...");
        }

        [RecurringJob("3 7,19 * * *", RecurringJobId = "SendCleanUpSubscriptionPlatinumCryptoCoffe")]
        [Queue("jobs")]
        public void SendCleanUpSubscriptionPlatinumCryptoCoffe(PerformContext context)
        {
            try
            {
                //var response = SendCleanUpSubscriptionEvent("PlatinumCryptoCoffe").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendCleanUpSubscriptionPlatinumCryptoCoffe Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendCleanUpSubscriptionPlatinumCryptoCoffe finished ...");
        }

        [RecurringJob("4 7,19 * * *", RecurringJobId = "SendCleanUpSubscriptionSilver")]
        [Queue("jobs")]
        public void SendCleanUpSubscriptionSilver(PerformContext context)
        {
            try
            {
                var response = SendCleanUpSubscriptionEvent("Silver").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendCleanUpSubscriptionSilver Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendCleanUpSubscriptionSilver finished ...");
        }

        [RecurringJob("5 7,19 * * *", RecurringJobId = "SendCleanUpSubscriptionGoldChat")]
        [Queue("jobs")]
        public void SendCleanUpSubscriptionGoldChat(PerformContext context)
        {
            try
            {
                var response = SendCleanUpSubscriptionEvent("GoldChat").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendCleanUpSubscriptionGoldChat Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendCleanUpSubscriptionGold finished ...");
        }

        [RecurringJob("7 7,19 * * *", RecurringJobId = "SendCleanUpSubscriptionSilverChat")]
        [Queue("jobs")]
        public void SendCleanUpSubscriptionSilverChat(PerformContext context)
        {
            try
            {
                var response = SendCleanUpSubscriptionEvent("SilverChat").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendCleanUpSubscriptionSilverChat Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendCleanUpSubscriptionSilverChat finished ...");
        }

        [RecurringJob("8 7,19 * * *", RecurringJobId = "SendCleanUpSubscriptionPlatinum")]
        [Queue("jobs")]
        public void SendCleanUpSubscriptionPlatinum(PerformContext context)
        {
            try
            {
                //var response = SendCleanUpSubscriptionEvent("Platinum").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendCleanUpSubscriptionPlatinum Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendCleanUpSubscriptionPlatinum finished ...");
        }

        [RecurringJob("9 7,19 * * *", RecurringJobId = "SendCleanUpSubscriptionPlatinumChat")]
        [Queue("jobs")]
        public void SendCleanUpSubscriptionPlatinumChat(PerformContext context)
        {
            try
            {
                //var response = SendCleanUpSubscriptionEvent("PlatinumChat").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendCleanUpSubscriptionPlatinumChat Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendCleanUpSubscriptionPlatinumChat finished ...");
        }


        [RecurringJob("1 8 * * *", RecurringJobId = "SendExpiredReminderEvent1")]
        [Queue("jobs")]
        public void SendExpiredReminder1(PerformContext context)
        {
            try
            {
                var response = SendExpiredReminderEvent("1").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendExpiredReminderEvent1 Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendExpiredReminderEvent1 finished ...");
        }

        [RecurringJob("10 8 * * *", RecurringJobId = "SendExpiredReminderEvent2")]
        [Queue("jobs")]
        public void SendExpiredReminder2(PerformContext context)
        {
            try
            {
                var response = SendExpiredReminderEvent("2").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendExpiredReminderEvent2 Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendExpiredReminderEvent2 finished ...");
        }

        [RecurringJob("20 8 * * *", RecurringJobId = "SendExpiredReminderEvent3")]
        [Queue("jobs")]
        public void SendExpiredReminder3(PerformContext context)
        {
            try
            {
                var response = SendExpiredReminderEvent("3").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendExpiredReminderEvent3 Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendExpiredReminderEvent3 finished ...");
        }

        [RecurringJob("30 8 * * *", RecurringJobId = "SendExpiredReminderEvent4")]
        [Queue("jobs")]
        public void SendExpiredReminder4(PerformContext context)
        {
            try
            {
                var response = SendExpiredReminderEvent("4").Result;
            }
            catch (Exception ex)
            {
                // TODO: Add logs to this service.
                context.WriteLine($"SendExpiredReminderEvent4 Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendExpiredReminderEvent4 finished ...");
        }

        private async Task<HttpResponseMessage> SendCleanUpSubscriptionEvent(string subscription)
        {
            var botId = "REPLACE_WITH_YOUR_VALUE";
            var parameters = new Dictionary<string, string>
            {
                {"Command", $"CleanUpSubscription{subscription}"},
                {"BotId", botId},
            };

            var body = PaymentHelper.GetKeyValues(parameters);

            var createSignature = PaymentHelper.CreateSignature(Secret, body);

            return await HttpClientHelper.PostAsyncHmac(body, BaseUrl, botId, createSignature);
        }

        private async Task<HttpResponseMessage> SendExpiredReminderEvent(string number)
        {
            var botId = "REPLACE_WITH_YOUR_VALUE";
            var parameters = new Dictionary<string, string>
            {
                {"Command", $"SendSubscriptionAboutToExpire{number}"},
                {"BotId", botId},
            };

            var body = PaymentHelper.GetKeyValues(parameters);

            var createSignature = PaymentHelper.CreateSignature(Secret, body);

            return await HttpClientHelper.PostAsyncHmac(body, BaseUrl, botId, createSignature);
        }
    }
}
