using System;
using System.Collections.Generic;
using System.Text;

namespace BotCreator.Core.DomainModels
{
    public class ResetTokenConfirmation
    {
        public string Token { get; set; }

        public string Password { get; set; }
    }
}
