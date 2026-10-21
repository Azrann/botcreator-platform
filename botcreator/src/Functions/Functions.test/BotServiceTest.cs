using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Functions.Services;
using Xunit;

namespace Functions.test
{
    public class BotServiceTest
    {
        private string Connection =
            "REPLACE_WITH_YOUR_VALUE";

        private string UserId = "4";

        [Fact]
        public void ICanFindBotsByUserId()
        {
            BotService service = new BotService(Connection);
            var result = service.GetMyBots(UserId);
            Assert.True(result.ToList().Count > 0);
        }

        [Fact]
        public void ICanCheckIfItIsMyBot()
        {
            var botId = "7f3ed87c-e2dd-41fc-bfe7-5085ac2c17f5";
            BotService service = new BotService(Connection);
            var result = service.IsMyBot(UserId, botId);
            Assert.True(result);
        }
    }
}
