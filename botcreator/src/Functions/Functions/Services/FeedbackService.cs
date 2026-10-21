using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace Functions.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly string _connectionString;

        public FeedbackService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddFeedback(Feedback Feedback)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(@"INSERT INTO [dbo].[Feedbacks]
                                       ([Id]
                                       ,[WebUserId]
                                       ,[CreatedDate]
                                       ,[BotId]
                                       ,[Platform]
                                       ,[UserAgent]
                                       ,[CurrentUrl]
                                        ,[Message]
                                        ,[Email]
                                        ,[ReplyToId]
                                      )
                                 VALUES
                                       (@Id
                                        ,@WebUserId
                                        ,getutcdate()
                                        ,@BotId
                                        ,@Platform
                                        ,@UserAgent
                                        ,@CurrentUrl
                                        ,@Message
                                        ,@Email
                                        ,@ReplyToId
                                       )", new
                    {
                        Id = Feedback.Id,
                        WebUserId = Feedback.WebUserId,
                        BotId = Feedback.BotId,
                        Platform = Feedback.Platform,
                        UserAgent = Feedback.UserAgent,
                        CurrentUrl = Feedback.CurrentUrl,
                        Message = Feedback.Message,
                        Email = Feedback.Email,
                        ReplyToId = Feedback.ReplyToId
                    });
                }        
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public IEnumerable<Feedback> GetFeedbackByBotId(string botId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Feedback>(@"SELECT * FROM [dbo].[Feedbacks]
                    WHERE BotId = @BotId", new
                {
                    BotId = botId
                });
            }
        }

        public Feedback GetFeedbackByUrl(string botId, string url)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<Feedback>(@"SELECT * FROM [dbo].[Feedbacks]
                    WHERE
                    CurrentUrl = @CurrentUrl AND BotId = @BotId", new
                {
                    CurrentUrl = url,
                    BotId = botId
                });
            }
        }

        public Feedback GetFeedbackById(Guid id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<Feedback>(@"SELECT * FROM [dbo].[Feedbacks]
                    WHERE
                    Id = @Id", new
                {
                    Id = id
                });
            }
        }

        public void RemoveFeedbackById(Guid id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(@"DELETE FROM [dbo].[Feedbacks]
                                       WHERE Id = @Id", new
                    {
                        Id = id
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }

    public interface IFeedbackService
    {
        int AddFeedback(Feedback Feedback);
        IEnumerable<Feedback> GetFeedbackByBotId(string botId);
        Feedback GetFeedbackByUrl(string botId, string url);
        Feedback GetFeedbackById(Guid id);
        void RemoveFeedbackById(Guid id);
    }
}

