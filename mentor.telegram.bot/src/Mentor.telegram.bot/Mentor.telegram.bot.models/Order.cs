using System;
using Core.Common.SqlDataAccess;

namespace Mentor.telegram.bot.models
{
    public class Order : Entity<Guid>
    {
        public string Name { get; set; }
        public decimal Fee { get; set; }
        public TimeSpan SubscriptionDuration { get; set; }
        public Guid ClientId { get; set; }
        public int Status { get; set; }
        public string TransactionId { get; set; }
    }
}