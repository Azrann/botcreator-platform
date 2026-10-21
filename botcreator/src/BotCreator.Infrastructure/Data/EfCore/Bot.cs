using System;
using System.Collections.Generic;

namespace BotCreator.Infrastructure.Data.EfCore
{
    public partial class Bot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BotTemplateId { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }
        public bool IsDeployed { get; set; }
        public int? OwnerId { get; set; }

        public BotTemplate BotTemplate { get; set; }
        public User Owner { get; set; }
    }
}
