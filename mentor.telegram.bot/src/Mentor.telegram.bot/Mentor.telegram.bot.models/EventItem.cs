using System;
using Core.Common.SqlDataAccess;

namespace Mentor.telegram.bot.models
{
    public class EventItem : Entity<Guid>
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string BotId { get; set; }
        public string DataJson { get; set; }
        public string ChannelId { get; set; }
        public Guid ClientId { get; set; }
    }
}

// I Want to store conversations with bots.
// How do I keep a thread?
// -> Searching by BotId + ChannelId
//
// (CorrelationID doesn't work) (ChannelId?)
// Tipos de eventos.

