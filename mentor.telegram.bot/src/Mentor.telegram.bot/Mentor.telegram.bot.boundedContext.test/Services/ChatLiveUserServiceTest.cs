using System;
using System.Linq;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Xunit;

namespace Mentor.telegram.bot.boundedContext.test.Services
{
    public class ChatLiveUserServiceTest
    {
        string connection = @"REPLACE_WITH_YOUR_VALUE";

        [Fact]
        public void WhenIAddAChatLiveUserICanRetrieveItByBotId()
        {
            IChatLiveUserService service = new ChatLiveUserService(connection);

            var ChatLiveUser = GetChatLiveUser();
            
            service.AddChatLiveUser(ChatLiveUser);

            var result = service.GetChatLiveUsersByBotId(ChatLiveUser.BotId);
            Assert.True(result.Count() == 1);
        }

        [Fact]
        public void WhenIAddAChatLiveUserICanRetrieveIt()
        {
            IChatLiveUserService service = new ChatLiveUserService(connection);

            var ChatLiveUser = GetChatLiveUser();

            service.AddChatLiveUser(ChatLiveUser);

            var result = service.GetChatLiveUserByToken(ChatLiveUser.BotId, ChatLiveUser.Auth);
            Assert.NotNull(result);
        }

        public ChatLiveUser GetChatLiveUser()
        {
            Random rnd = new Random();
            return new ChatLiveUser()
            {
                BotId = $"{Guid.NewGuid().ToString().Replace("-","")}{Guid.NewGuid().ToString().Replace("-", "")}",
                Id = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                ChannelId = rnd.Next(1000000).ToString(),
                Email = $"UserId{rnd.Next(99999999)}@gmail.com",
                Name ="aplber",
                Auth = Guid.NewGuid().ToString().Replace("-",""),
            };
        }
    }
}
