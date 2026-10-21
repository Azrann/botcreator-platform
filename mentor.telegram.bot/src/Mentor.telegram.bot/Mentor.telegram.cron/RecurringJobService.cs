using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Common;
using Hangfire;
using Hangfire.Console;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using Mentor.telegram.bot.api.services.Services.Email;
using Newtonsoft.Json;
using PubnubAdmin;

namespace Mentor.telegram.cron
{
    public class RecurringJobService
    {
        [RecurringJob("5,35 * * * *", RecurringJobId = "RestartPubNub")]
        [Queue("jobs")]
        public void RestartPubNub(PerformContext context)
        {
            try
            {
                PubnubWrapper client = new PubnubWrapper();
                var token = client.Login("user@example.invalid", "%Qw5od%Ew%8h");
                var result1 = client.Stop("540819", "44763", token);
                var result2 = client.Start("540819", "44763", token);
                var result3 = client.Stop("720538", "52926", token);
                var result4 = client.Start("720538", "52926", token);
            }
            catch (Exception ex)
            {
                context.WriteLine($"SendRestartPubNub Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} SendRestartPubNub finished ...");
        }

        [RecurringJob("1 8,20 * * *", RecurringJobId = "TestIvoEndpoints")]
        [Queue("jobs")]
        public async Task TestIvoEndpoints(PerformContext context)
        {
            try
            {
                string content = await GetErrorServices();

                if (!string.IsNullOrEmpty(content))
                {
                    var url =
                        "\n\nYou can test the availability of the API clicking on this url <a href='https://botcreatorfunc.azurewebsites.net/api/IvoAvailability?code=avfXu/Og8ke3pMnj0b8wO3KVvRjktch/aGwJ8zjPAV4BijM5ejpEWQ=='>CLICK HERE</a>";
                    EmailService emailService = new EmailService();
                    var response = await emailService.Send(new SendEmailEvent()
                    {
                        BodyHtml = url + content,
                        CorrelationId = Guid.NewGuid(),
                        EmailTo = "user@example.invalid",
                        EmailCc = "user@example.invalid,user@example.invalid",
                        PlainTextContent = "",
                        Subject = "API services not working :("
                    });
                }
            }
            catch (Exception ex)
            {
                context.WriteLine($"TestIvoEndpoints Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} TestIvoEndpoints finished ...");
        }

        [RecurringJob("1/5 * * * *", RecurringJobId = "WakeUpBotApis")]
        [Queue("jobs")]
        public async Task WakeUpBotApis(PerformContext context)
        {
            try
            {
                string content = await GetBotServices();

                if (!string.IsNullOrEmpty(content))
                {
                    EmailService emailService = new EmailService();
                    var response = await emailService.Send(new SendEmailEvent()
                    {
                        BodyHtml = content,
                        CorrelationId = Guid.NewGuid(),
                        EmailTo = "user@example.invalid",
                        EmailCc = "user@example.invalid",
                        PlainTextContent = "",
                        Subject = "API services not working :("
                    });
                }
            }
            catch (Exception ex)
            {
                context.WriteLine($"WakeUpBotApis Error {JsonConvert.SerializeObject(ex)}");
            }

            context.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} WakeUpBotApis finished ...");
        }

        public async Task<string> GetErrorServices()
        {
            Dictionary<string, string> services = new Dictionary<string, string>();
            services.Add("member-area/members/gold", "Gold");
            services.Add("member-area/members/silver", "Silver");
            services.Add("member-area/members/gold-subscription-trial-7-days", "Gold Trial");
            services.Add("member-area/members/platinum", "Platinum");
            services.Add("member-area/members/platinum-monthly", "Platinum monthly");
            services.Add("member-area/members/silver-monthly", "Silver monthly");
            services.Add("member-area/members/gold-monthly", "Gold monthly");
            StringBuilder sb = new StringBuilder();

            foreach (var service in services)
            {
                var response = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/members/platinum",
                    "REPLACE_WITH_YOUR_VALUE");

                var stringResponse = await response.Content.ReadAsStringAsync();
                var usernames =
                    JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);

                if (usernames.Payload == null)
                {
                    sb.AppendLine($"\n\n<br><br>Service https://verifiedcryptotraders.com{"/"+service.Key} could not retrieve data. With response <br><br>\n\n{stringResponse}");
                }
            }

            return sb.ToString();
        }

        public async Task<string> GetBotServices()
        {
            Dictionary<string, string> services = new Dictionary<string, string>();
            services.Add("https://botcreatorapp.azurewebsites.net", "BotApi Prod");
            services.Add("https://botcreatorapptest.azurewebsites.net", "BotApi Dev");
            services.Add("https://botengineapi-deve.azurewebsites.net", "BotEngine Dev");
            services.Add("https://botengineapi-prod.azurewebsites.net", "BotEngine Prod");
            StringBuilder sb = new StringBuilder();

            foreach (var service in services)
            {
                var response = await HttpClientHelper.GetAsync(service.Key, "api/status");

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    sb.AppendLine($"\n\n<br><br>Service {service.Key} could not retrieve status. with response {response.StatusCode} <br><br>\n\n");
                }
            }

            return sb.ToString();
        }
    }

    public class GetUsernamesResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public List<string> Payload { get; set; }
    }
}
