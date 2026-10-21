using System;
using System.Collections.Generic;
using System.Text;

namespace BotCreator.Core.Configs
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; }

        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        public string Subject { get; set; }

    }
}
