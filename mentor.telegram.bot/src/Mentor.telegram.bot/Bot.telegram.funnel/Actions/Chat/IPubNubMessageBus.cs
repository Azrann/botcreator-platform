using System;

namespace Mentor.telegram.bot.api
{
    public interface IPubnubMessageBus
    {
        void Publish(object message, string ChannelId);

        void Grantd(string authKey, string channel, bool read, bool write, int ttl = 240);

        IPubnubMessageBus Start(Guid correlationId);
    }
}