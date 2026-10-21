namespace Mentor.telegram.bot.models
{
    public class QueueItem
    {
        public string AuthKey { get; set; }
        public string ChannelId { get; set; }
        public string BotId { get; set; }
    }

    public class Agent
    {
        public string AuthKey { get; set; }
        public string Name { get; set; }
        public string BotId { get; set; }
    }
}