using System;

namespace Mentor.telegram.bot.models
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid WebUserId { get; set; }
        public string Platform { get; set; }
        public string UserAgent { get; set; }
        public string CurrentUrl { get; set; }
        public string BotId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public Guid ReplyToId { get; set; }
    }
}
