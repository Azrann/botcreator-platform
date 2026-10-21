using System;
using System.Collections.Generic;
using System.Text;

namespace BotCreator.Core.DomainModels.Bot
{
    public class UserBotModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TemplateName { get; set; }

        public bool IsDeployed { get; set; }
    }
}
