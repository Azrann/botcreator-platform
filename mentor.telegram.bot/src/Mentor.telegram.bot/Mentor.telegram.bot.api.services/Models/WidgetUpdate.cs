using System;

namespace Mentor.telegram.bot.api.Models
{
    public class WidgetUpdate
    {
        public string Type { get; set; }
        public string ChatId { get; set; }
        public string FromId { get; set; }
        public string Text { get; set; }
        public string ReplyToMessage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid WebUserId { get; set; }
    }
}
