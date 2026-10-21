using System;
using Core.Common.SqlDataAccess;

namespace Mentor.telegram.bot.models
{
    public class ChatLiveUser : Entity<Guid>
    { 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Auth { get; set; }
        public string BotId { get; set; }
        public string ChannelId { get; set; }
        public Guid ClientId { get; set; }
    }
}