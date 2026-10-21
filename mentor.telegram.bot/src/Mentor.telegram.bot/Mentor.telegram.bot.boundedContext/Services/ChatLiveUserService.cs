using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Mentor.telegram.bot.models;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public class ChatLiveUserService : IChatLiveUserService
    {
        private readonly string _connectionString;

        public ChatLiveUserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddChatLiveUser(ChatLiveUser chatLiveUser)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(@"INSERT INTO [dbo].[ChatLiveUsers]
                                       ([Id]
                                       ,[ClientId]
                                       ,[CreatedOn]
                                       ,[LastUpdated]
                                       ,[ChannelId]
                                       ,[BotId]
                                       ,[Auth]
                                       ,[Name]
                                       ,[Email])
                                 VALUES
                                       (@Id
                                        ,@ClientId
                                        ,getutcdate()
                                        ,getutcdate()
                                        ,@ChannelId
                                        ,@BotId
                                        ,@Auth
                                        ,@Name
                                        ,@Email)", new
                    {
                        chatLiveUser.Id,
                        chatLiveUser.ClientId,
                        chatLiveUser.ChannelId,
                        chatLiveUser.BotId,
                        chatLiveUser.Auth,
                        chatLiveUser.Name,
                        chatLiveUser.Email
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public IEnumerable<ChatLiveUser> GetChatLiveUsersByBotId(string botId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<ChatLiveUser>(@"SELECT * FROM [dbo].[ChatLiveUsers]
                    WHERE BotId = @BotId", new
                    {
                        BotId = botId
                    });
            }
        }

        public ChatLiveUser GetChatLiveUserByToken(string botId, string auth)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<ChatLiveUser>(@"SELECT * FROM [dbo].[ChatLiveUsers]
                    WHERE
                    Auth = @Auth AND BotId = @BotId", new {
                    Auth = auth, BotId = botId
                });
            }
        }

        public ChatLiveUser GetChatLiveUserByChannelId(string botId, string channelId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<ChatLiveUser>(@"SELECT * FROM [dbo].[ChatLiveUsers]
                    WHERE
                    ChannelId = @ChannelId AND BotId = @BotId", new
                {
                    ChannelId = channelId,
                    BotId = botId
                });
            }
        }

        public ChatLiveUser GetChatLiveUserById(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<ChatLiveUser>(@"SELECT * FROM [dbo].[ChatLiveUsers]
                    WHERE
                    Id = @Id", new
                {
                    Id = id
                });
            }
        }
    }

    public interface IChatLiveUserService
    {
        int AddChatLiveUser(ChatLiveUser chatLiveUser);
        IEnumerable<ChatLiveUser> GetChatLiveUsersByBotId(string botId);
        ChatLiveUser GetChatLiveUserByToken(string botId, string auth);
        ChatLiveUser GetChatLiveUserByChannelId(string botId, string channelId);
        ChatLiveUser GetChatLiveUserById(string id);
    }
}
