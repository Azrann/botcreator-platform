using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Mentor.telegram.bot.models;

namespace BotCreator.Service.Bot
{
    public class EventService : IEventService
    {
        private readonly string _connectionString;

        public EventService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddEvent(EventItem eventItem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(@"INSERT INTO [dbo].[EventItems]
                                       ([Id]
                                       ,[CreatedOn]
                                       ,[LastUpdated]
                                       ,[ChannelId]
                                       ,[BotId]
                                       ,[Username]
                                       ,[Name]
                                       ,[DataJson])
                                 VALUES
                                       (@Id
                                        ,getutcdate()
                                        ,getutcdate()
                                        ,@ChannelId
                                        ,@BotId
                                        ,@Username
                                        ,@Name
                                        ,@DataJson)", new
                    {
                        Id = eventItem.Id,
                        ChannelId = eventItem.ChannelId,
                        BotId = eventItem.BotId,
                        Username = eventItem.Username,
                        Name = eventItem.Name,
                        DataJson = eventItem.DataJson
                    });
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        public IEnumerable<EventItem> GetEventsByBotId(string botId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<EventItem>(@"SELECT * FROM [dbo].[EventItems]
                    WHERE BotId = @BotId", new
                {
                    BotId = botId
                });
            }
        }

        public EventItem GetEventsByUsername(string botId, string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<EventItem>(@"SELECT * FROM [dbo].[EventItems]
                    WHERE
                    Username = @Username AND BotId = @BotId", new
                {
                    Username = username,
                    BotId = botId
                });
            }
        }

        public EventItem GetEventsByChannelId(string botId, string channelId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<EventItem>(@"SELECT * FROM [dbo].[EventItems]
                    WHERE
                    ChannelId = @ChannelId AND BotId = @BotId", new
                {
                    ChannelId = channelId,
                    BotId = botId
                });
            }
        }

        public EventItem GetEventById(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<EventItem>(@"SELECT * FROM [dbo].[EventItems]
                    WHERE
                    Id = @Id", new
                {
                    Id = id
                });
            }
        }
    }

    public interface IEventService
    {
        int AddEvent(EventItem EventItem);
        IEnumerable<EventItem> GetEventsByBotId(string botId);
        EventItem GetEventsByUsername(string botId, string Username);
        EventItem GetEventsByChannelId(string botId, string channelId);
        EventItem GetEventById(string id);
    }
}