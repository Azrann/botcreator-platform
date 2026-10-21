using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Functions.Services
{
    public class BotService : IBotService
    {
        private readonly string _connectionString;

        public BotService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool IsMyBot(string userId, string botId)
        {
            return GetMyBots(userId).Contains(botId);
        }

        public IEnumerable<string> GetMyBots(string userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<string>(@"SELECT ExternalId FROM [dbo].[Bots]
                    WHERE OwnerId = @UserId", new
                {
                    UserId = userId
                });
            }
        }
    }

    public interface IBotService
    {
        bool IsMyBot(string userId, string botId);
    }
}
