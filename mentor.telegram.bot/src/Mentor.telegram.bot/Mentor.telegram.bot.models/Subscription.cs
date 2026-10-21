using System;
using Core.Common.SqlDataAccess;

namespace Mentor.telegram.bot.models
{
    public class Subscription : Entity<Guid>
    {
        public DateTime ExpiryDate { get; set; }
        public string Name { get; set; }
        public decimal Fee { get; set; }
        public string Currency { get; set; }
        public bool Active { get; set; }

    }

    public class ExpiredSubscription : Subscription
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public long ChatId { get; set; }
        public string BotId { get; set; }
    }
}
