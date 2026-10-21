using System;
using System.Collections.Generic;

namespace BotCreator.Infrastructure.Data.EfCore
{
    public partial class BotTemplate
    {
        public BotTemplate()
        {
            Bots = new HashSet<Bot>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }

        public ICollection<Bot> Bots { get; set; }
    }
}
