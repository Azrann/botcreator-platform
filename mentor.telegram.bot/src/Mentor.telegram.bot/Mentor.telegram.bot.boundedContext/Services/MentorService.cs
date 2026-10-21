using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Mentor.telegram.bot.models;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public class MentorService : IMentorService
    {
        private readonly string _connectionString;

        public MentorService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<models.Mentor> GetMentorsByBotId(string botId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<models.Mentor>(@"SELECT * FROM Mentors as c
                    WHERE c.BotId = @BotId", new
                {
                    BotId = botId
                });
            }
        }

        public void UpdateMentorEmail(string userId, string botId, string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(@"UPDATE [dbo].[Mentors]
                   SET [LastUpdated] = getutcdate()
                     [Email] = @Email
                 WHERE [BotId] = @BotId AND [UserId] = @UserId", new
                {
                    Email = email,
                    BotId = botId,
                    UserId = userId
                });
            }
        }

        public IEnumerable<Subscription> GetSubscriptions(string botId, string adminId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Subscription>(@"SELECT * FROM Mentors as c
                    INNER JOIN Subscription ON (c.Id = adminId)
                    WHERE
                    c.UserId = @UserId AND c.BotId = @BotId", new {
                    UserId = adminId, BotId = botId});
            }
        }

        public IEnumerable<Subscription> GetActiveSubscriptions(string botId, string adminId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Subscription>(@"SELECT * FROM Mentors as c
                    INNER JOIN Subscription ON (c.Id = adminId)
                    WHERE
                    c.UserId = @UserId AND c.BotId = @BotId
                    AND ExpiryDate > getutcdate()", new
                {
                    UserId = adminId,
                    BotId = botId
                });
            }
        }

        private void UpdateSubscription(Subscription subscription)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(@"UPDATE [dbo].[Subscription]
                   SET [LastUpdated] = getutcdate()
                      ,[ExpiryDate] = @ExpiryDate
                      ,[Name] = @Name
                      ,[Fee] = @Fee
                      ,[Currency] = @Currency
                 WHERE [Id] = @Id", new
                {
                    subscription.ExpiryDate,
                    subscription.Name,
                    subscription.Fee,
                    subscription.Currency,
                    subscription.Id
                });
            }
        }

        public int RenewSubscription(string botId, string adminId, string subscriptionName, short subscriptionMonths,
            short subscriptionDays, string subscriptionCurrency, decimal subscriptionFee)
        {
            List<Subscription> subscriptions = GetSubscriptions(botId, adminId).ToList();

            var oldSubscription = subscriptions.FirstOrDefault(x => x.Name == subscriptionName);

            if ((oldSubscription != null) && (oldSubscription.ExpiryDate > DateTime.Now))
            {
                oldSubscription.ExpiryDate = oldSubscription.ExpiryDate.AddMonths(subscriptionMonths);
                oldSubscription.ExpiryDate = oldSubscription.ExpiryDate.AddDays(subscriptionDays);
                oldSubscription.Fee = subscriptionFee;
                oldSubscription.Currency = subscriptionCurrency;
                UpdateSubscription(oldSubscription);
            }
            else
            {
                AddNewSubscription(botId, adminId, new Subscription()
                {
                    Name = subscriptionName,
                    Currency = subscriptionCurrency,
                    Fee = subscriptionFee
                }, DateTime.Now.AddMonths(subscriptionMonths).AddDays(subscriptionDays));
            }

            return 1;
        }

        private void AddNewSubscription(string botId, string adminId, Subscription subscription, DateTime expiryDate)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(@"
                    DECLARE @adminId uniqueIdentifier
                    SET @adminId = (SELECT Id FROM Mentors as c
                    WHERE c.UserId = @UserId AND c.BotId = @BotId)

                    INSERT INTO [dbo].[Subscription]
                               ([Id]
                               ,[CreatedOn]
                               ,[LastUpdated]
                               ,[ExpiryDate]
                               ,[Name]
                               ,[Fee]
                               ,[ClientId]
                               ,[Currency])
                         VALUES
                               (NEWID()
                               ,getutcdate()
                               ,getutcdate()
                               ,@ExpiryDate
                               ,@Name
                               ,@Fee
                               ,@adminId
                               ,@Currency
                    GO
                    ", new
                {
                    ExpiryDate = expiryDate,
                    subscription.Name,
                    subscription.Fee,
                    subscription.Currency,
                    UserId = adminId,
                    BotId = botId
                });
            }
        }

        public int AddMentor(models.Mentor mentor)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Execute("Add_Mentor", new
                    {
                        UserId = mentor.User.Id,
                        Firstname = mentor.User.FirstName,
                        Lastname = mentor.User.LastName,
                        Username = mentor.User.UserName,
                        mentor.User.AccessHash,
                        mentor.ChatId,
                        mentor.BotId,
                        mentor.UserEmail
                },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }

    public interface IMentorService
    {
        int AddMentor(models.Mentor mentor);
        int RenewSubscription(string botId, string adminId, string subscriptionName, short subscriptionMonths,
            short subscriptionDays, string subscriptionCurrency, decimal subscriptionFee);
        void UpdateMentorEmail(string userId, string botId, string email);
        IEnumerable<Subscription> GetSubscriptions(string botId, string adminId);
        IEnumerable<Subscription> GetActiveSubscriptions(string botId, string adminId);
    }
}
