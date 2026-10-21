using System;
using System.Collections.Generic;
using System.Text;
using Bot.Configuration;
using Core.Common;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.api.Services.UserInterface;
using Mentor.telegram.bot.boundedContext.Services;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Xunit;

namespace Mentor.telegram.bot.api.test
{
    public class TelegramClientTest
    {
        [Fact]
        public async void WhenICallGetUsersGoldIGetResponse()
        {
           var response = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/members/gold",
                "REPLACE_WITH_YOUR_VALUE");
            
           var stringResponse = await response.Content.ReadAsStringAsync();
            GetUsernamesResponse response1 =
                JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void WhenICallGetUsersSilverIGetResponse()
        {
            var response = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/members/silver",
                "REPLACE_WITH_YOUR_VALUE");

            var stringResponse = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void WhenICallGetUsersGoldTrialsIGetResponse()
        {
            var response = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/members/gold-subscription-trial-7-days",
                "REPLACE_WITH_YOUR_VALUE");

            var stringResponse = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void WhenICallGetUsersPlatinumIGetResponse()
        {
            var response = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/member-area/members/platinum/",string.Empty ,
                "REPLACE_WITH_YOUR_VALUE");

            var stringResponse = await response.Content.ReadAsStringAsync();
            GetUsernamesResponse response1 =
                JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(response1.Payload.Count >3);
        }

        [Fact]
        public async void WhenICallGetUsersICanGetACountForBilling()
        {
            StringBuilder sb = new StringBuilder();
            var response = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/members/gold",
               "REPLACE_WITH_YOUR_VALUE");

            var stringResponse = await response.Content.ReadAsStringAsync();
            GetUsernamesResponse response1 =
                JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);

            sb.AppendLine($"Users GOLD: {response1.Payload.Count}");

            response = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/members/silver",
                "REPLACE_WITH_YOUR_VALUE");

            stringResponse = await response.Content.ReadAsStringAsync();
            response1 =
                JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);

            sb.AppendLine($"Users SILVER: {response1.Payload.Count}");
            /* response = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/members/margin",
                 "REPLACE_WITH_YOUR_VALUE");

             stringResponse = await response.Content.ReadAsStringAsync();
             response1 =
                 JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);
             sb.AppendLine($"Users MARGIN: {response1.Payload.Count}");
             */
           /* response = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/members/gold-subscription-trial-7-days",
                 "REPLACE_WITH_YOUR_VALUE");

             stringResponse = await response.Content.ReadAsStringAsync();
             response1 =
                 JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);
             sb.AppendLine($"Gold Trial: {response1.Payload.Count}");*/

            response = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/members/platinum",
                "REPLACE_WITH_YOUR_VALUE");

            stringResponse = await response.Content.ReadAsStringAsync();
            response1 =
                JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);
            sb.AppendLine($"Users Platinum: {response1.Payload.Count}");

            /*TODO I use this for billing. Invoice. Charge*/
            var result = sb.ToString();
            Assert.True(true);
        }

        [Fact]
        public void WhenIGetATransactionIRetrieveAResponseVoid()
        {
            IChannelService service = new TelegramChannelService();
            int userId = 584308906;
            
            var result = service.AddUserToChannel(userId, "REPLACE_WITH_YOUR_VALUE", -4221940998005475662, Guid.NewGuid());
            Assert.True(true);
        }


        [Fact]
        public async void WhenISendAMessageIReceiveIt()
        {
            IUserInterfaceService userInterface = new TelegramUserInterfaceService(
                new BotService(new OptionsWrapper<BotConfiguration>(new BotConfiguration()
                {
                    Socks5Host = "",
                    Socks5Port = 1080
                })));
            int johnInvestId = 531710436;
            Dictionary<string,object> parameters = new Dictionary<string, object>();
            parameters.AddUserId(johnInvestId.ToString());
            parameters.AddBotId("REPLACE_WITH_YOUR_VALUE");
            parameters.AddChatId(johnInvestId);
            await userInterface.SendMessage("[Notifications Activated]", parameters);
        }

        [Fact]
        public void ICanSendAMessageToLarsUser()
        {
            IUserInterfaceService userInterface = new TelegramUserInterfaceService(
                new BotService(new OptionsWrapper<BotConfiguration>(new BotConfiguration()
                {
                    Socks5Host = "",
                    Socks5Port = 1080
                })));

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddUserId(531710436.ToString());
            parameters.AddBotId("REPLACE_WITH_YOUR_VALUE");
            parameters.AddChatId(531710436);
            userInterface.SendMessage("This Is a test Message", parameters);
        }

        [Fact]
        public async void ICanRetrieveSubscriptionsByUser()
        {
            var result = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/member/larsvelthuis",
                "REPLACE_WITH_YOUR_VALUE");

            if (!result.IsSuccessStatusCode)
            {
                return;
            }

            var stringResponse = await result.Content.ReadAsStringAsync();
            GetMembershipsResponse response =
                JsonConvert.DeserializeObject<GetMembershipsResponse>(stringResponse);
            if (response.Success == true && response.Payload != null && response.Payload.Count > 0)
            {
                // TODO send message to the bot with subscription info.
            }
        }


        [Fact]
        public async void ICanRetrieveExpiredUsersPerSubscription()
        {
            var result = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/", "member-area/users/gold",
                "REPLACE_WITH_YOUR_VALUE");

            if (!result.IsSuccessStatusCode)
            {
                return;
            }

            var stringResponse = await result.Content.ReadAsStringAsync();
            GetExpiredUsersResponse response =
                JsonConvert.DeserializeObject<GetExpiredUsersResponse>(stringResponse);
            if (response.Success == true && response.Payload != null && response.Payload.Count > 0)
            {
                // TODO send message to the bot with subscription info.
            }
        }
    }
}
