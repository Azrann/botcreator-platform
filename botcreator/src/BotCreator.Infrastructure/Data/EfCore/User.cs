using System;
using System.Collections.Generic;

namespace BotCreator.Infrastructure.Data.EfCore
{
    public partial class User
    {
        public User()
        {
            Bots = new HashSet<Bot>();
            ResetTokens = new HashSet<ResetToken>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Bot> Bots { get; set; }
        public ICollection<ResetToken> ResetTokens { get; set; }
    }
}
