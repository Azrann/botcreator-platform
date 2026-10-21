using System;
using System.Collections.Generic;

namespace BotCreator.Infrastructure.Data.EfCore
{
    public partial class ResetToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expires { get; set; }

        public User User { get; set; }
    }
}
