using Mentor.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Xunit;

namespace Mentor.telegram.bot.boundedContext.test.Services
{
    public class LiveChatQueueTest
    {
        string connection = "REPLACE_WITH_YOUR_VALUE";

        [Fact]
        public void WhenIEnqueAChatLiveUserICanRetrieveIt()
        {
            string botId = "03312e47-7b57-450e-89a0-37dc5d681866";
            string channelId = "myChannelId1";
            string auth = "auth123";

            QueueItem item = new QueueItem()
            {
                ChannelId = channelId,
                AuthKey = auth,
                BotId = botId
            };

            ILiveChatQueue service = new LiveChatQueue(connection, "DEV");

            var result = service.AddClientToQueue(item).Result;

            var itemResult = service.GetClientFromQueue(item.BotId).Result;
            Assert.True(itemResult.ChannelId == channelId);
            Assert.True(itemResult.BotId == botId);
            Assert.True(itemResult.AuthKey == auth);
        }

        [Fact]
        public async void WhenIDequeAChatLiveUserMessageIsRemoved()
        {
            string botId = "03312e47-7b57-450e-89a0-37dc5d681866";
            string channelId = "myChannelId2";
            string auth = "auth123";

            QueueItem item = new QueueItem()
            {
                ChannelId = channelId,
                AuthKey = auth,
                BotId = botId
            };

            ILiveChatQueue service = new LiveChatQueue(connection,"DEV");

            var result = await service.AddClientToQueue(item);

            var itemResult = await service.GetClientFromQueue(botId);
            var itemResult2 = await service.GetClientFromQueue(botId);
            Assert.True(itemResult.ChannelId == channelId);
            Assert.True(itemResult.BotId == botId);
            Assert.True(itemResult.AuthKey == auth);
            Assert.True(itemResult2 == null);
        }
    }
}
