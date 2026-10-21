using System;
using System.Collections.Generic;
using Core.Common.SqlDataAccess;

namespace Mentor.telegram.bot.models
{
    public class Mentor : Entity<Guid>
    {
        public TelegramUser User { get; set; }
        public string UserEmail { get; set; }
        public long ChatId { get; set; }
        public string BotId { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}