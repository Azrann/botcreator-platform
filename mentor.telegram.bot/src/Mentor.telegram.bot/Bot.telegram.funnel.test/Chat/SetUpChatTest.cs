using System;
using Bot.Configuration;
using Bot.telegram.funnel.Actions;
using Core.Common.Extensions;
using Mentor.telegram.bot.api;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Bot.telegram.funnel.test.Chat
{
    public class SetUpChatTest
    {
        private string subscriberKey = "REPLACE_WITH_YOUR_VALUE";
        private string secretKey = "REPLACE_WITH_YOUR_VALUE";
        private string publisherKey = "REPLACE_WITH_YOUR_VALUE";
        private const string WebhookChannel = "bc-message";

        [Fact]
        public void ICanCreateAChatWithoutExceptions()
        {
            Mock<IConfigurationPubnub> config = new Mock<IConfigurationPubnub>();
            config.Setup(x => x.GetSubscriberKey()).Returns(subscriberKey);
            config.Setup(x => x.GetPublisherKey()).Returns(publisherKey);
            config.Setup(x => x.GetSecretKey()).Returns(secretKey);
            Mock<IConfigurationCommon> configCommon = new Mock<IConfigurationCommon>();

            IPubnubMessageBus pubnubMessageBus = new PubnubMessageBus(config.Object);

            Guid correlationId = Guid.NewGuid();
            string authKey = Guid.NewGuid().ToString().Replace("-", "");
            var channel = GetRandomChannelId();
            string webhookChannelId = WebhookChannel + "1";

            pubnubMessageBus.Start(correlationId);
            pubnubMessageBus.Grantd(authKey, channel, true, false);
            pubnubMessageBus.Grantd(authKey, webhookChannelId, false, true);
            pubnubMessageBus.Grantd(authKey + 1, channel, true, true);
        }

        private static string GetRandomChannelId()
        {
            Random rnd = new Random();
            string channel = rnd.Next(1, 99999999).ToString();
            return channel;
        }
    }
}
