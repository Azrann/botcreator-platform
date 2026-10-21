using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace Functions.Services
{
    public class WebMetricsService : IWebMetricsService
    {
        private readonly string _connectionString;

        public WebMetricsService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddWebMetric(WebMetric webMetric)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(@"INSERT INTO [dbo].[WebMetrics]
                                       ([Id]
                                       ,[WebUserId]
                                       ,[CreatedDate]
                                       ,[BotId]
                                       ,[Cookies]
                                       ,[BrowserLanguage]
                                       ,[Language]
                                       ,[Platform]
                                       ,[Connection]
                                       ,[UserAgent]
                                       ,[Webdriver]
                                       ,[CurrentUrl]
                                       ,[Ip]
                                       ,[City]
                                       ,[Region]
                                       ,[Country]
                                       ,[Loc]
                                       ,[Postal]
                                        ,[Referrer])
                                 VALUES
                                       (@Id
                                        ,@WebUserId
                                        ,getutcdate()
                                        ,@BotId
                                        ,@Cookies
                                        ,@BrowserLanguage
                                        ,@Language
                                        ,@Platform
                                        ,@Connection
                                        ,@UserAgent
                                        ,@Webdriver
                                        ,@CurrentUrl
                                        ,@Ip
                                        ,@City
                                        ,@Region
                                        ,@Country
                                        ,@Loc
                                        ,@Postal
                                        ,@Referrer)", new
                    {
                        Id = webMetric.Id,
                        WebUserId = webMetric.WebUserId,
                        BotId = webMetric.BotId,
                        Cookies = webMetric.Cookies,
                        BrowserLanguage = webMetric.BrowserLanguage,
                        Language = webMetric.Language,
                        Platform = webMetric.Platform,
                        Connection = webMetric.Connection,
                        UserAgent = webMetric.UserAgent,
                        Webdriver = webMetric.Webdriver,
                        CurrentUrl = webMetric.CurrentUrl,
                        Ip = webMetric.Ip,
                        City = webMetric.City,
                        Region = webMetric.Region,
                        Country = webMetric.Country,
                        Loc = webMetric.Loc,
                        Postal = webMetric.Postal,
                        Referrer = webMetric.Referrer
                    });
                }        
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public IEnumerable<WebMetric> GetWebMetricsByBotId(string botId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<WebMetric>(@"SELECT * FROM [dbo].[WebMetrics]
                    WHERE BotId = @BotId", new
                {
                    BotId = botId
                });
            }
        }

        public WebMetric GetWebMetricByUrl(string botId, string url)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<WebMetric>(@"SELECT * FROM [dbo].[WebMetrics]
                    WHERE
                    CurrentUrl = @CurrentUrl AND BotId = @BotId", new
                {
                    CurrentUrl = url,
                    BotId = botId
                });
            }
        }

        public WebMetric GetWebMetricById(Guid id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<WebMetric>(@"SELECT * FROM [dbo].[WebMetrics]
                    WHERE
                    Id = @Id", new
                {
                    Id = id
                });
            }
        }

        public void RemoveWebMetricById(Guid id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(@"DELETE FROM [dbo].[WebMetrics]
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

    public interface IWebMetricsService
    {
        int AddWebMetric(WebMetric webMetric);
        IEnumerable<WebMetric> GetWebMetricsByBotId(string botId);
        WebMetric GetWebMetricByUrl(string botId, string url);
        WebMetric GetWebMetricById(Guid id);
        void RemoveWebMetricById(Guid id);
    }
}

