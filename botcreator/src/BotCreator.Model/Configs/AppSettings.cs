using System;
using System.Collections.Generic;
using System.Text;

namespace BotCreator.Core.Configs
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public int BearerTokenExpireMinutes { get; set; }

        public int RefreshTokenExpireMinutes { get; set; }

        public int ResetTokenExpireHours { get; set; }

        public string ResetTokenBaseUrl { get; set; }

        public string ResetTokenQueryField { get; set; }

        public EmailSettings EmailSettings { get; set; }
    }
}
