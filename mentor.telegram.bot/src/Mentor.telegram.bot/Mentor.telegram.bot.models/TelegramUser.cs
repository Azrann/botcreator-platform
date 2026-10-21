using Core.Common.SqlDataAccess;

namespace Mentor.telegram.bot.models
{
    public class TelegramUser : Entity<string>
    {
        public long AccessHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}