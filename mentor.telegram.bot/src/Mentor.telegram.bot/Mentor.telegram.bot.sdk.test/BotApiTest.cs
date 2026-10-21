using System;
using System.Collections.Generic;
using Mentor.telegram.bot.sdk;
using Mentor.telegram.bot.sdk.Dtos;
using Xunit;

namespace Mentor.telegram.bot.sdk.test
{
    public class BotApiTest
    {
        [Fact]
        public void CanCreateBotAPI()
        {
            BotApi api = new BotApi();

            var botId = new Guid("10000000-0000-0000-0000-000000000001");
            IBotDefinitionDto aBotDefinition = new WidgetSupportBotDto()
            {
                ConnectingToAgentMessage = "Connecting to Agent... NewBot",
                ChatOnWeb = false,
                FbChecked = false,
                WhatsappChecked = false,
                CallMeChecked = true,
                CallMeEmail = "user@example.invalid",
                CallMeButtonText = "llamame",
                PreMenuText = "Options:",
                MenuBot = new Dictionary<string, string>()
                {
                    {"Contact info:","You can find us at /n 14 House CT43GF Phone number:07544588556"},
                    {"Delivery", "Deliveries will take between 3 and 5 days if you don't get it contact us at user@example.invalid" },
                    {"Discount codes", "At the moment we are offering 10% with code 'Smart10' we change discounts every week keep in touch for the best deals." }
                }
            }; 

            api.CreateBot(botId.ToString(), aBotDefinition);
            api.DeployBot(botId.ToString());
        }
    }
}