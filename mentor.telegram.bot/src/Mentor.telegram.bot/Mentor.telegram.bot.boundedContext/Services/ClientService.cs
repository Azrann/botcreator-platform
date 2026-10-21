using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Mentor.telegram.bot.models;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public class ClientService : IClientService
    {
        private readonly string _connectionString;

        public ClientService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Client> GetClientsByBotId(string botId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Client, TelegramUser, Client>(@"SELECT c.*, t.* FROM Clients as c
                    INNER JOIN TelegramUser as t
                    ON (c.UserId = t.Id)
                    WHERE c.BotId = @BotId"
                    , (c, t) =>
                    {
                        c.User = t;
                        return c;
                    }, new
                    {
                        BotId = botId
                    },
                    splitOn: "Id");
            }
        }

        public Client GetClient(string botId, string userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var result = conn.Query<Client, TelegramUser, Client>(@"SELECT c.*, t.* FROM Clients as c
                    INNER JOIN TelegramUser as t
                    ON (c.UserId = t.Id)
                    WHERE c.BotId = @BotId AND
                        c.UserId = @UserId"
                    , (c, t) =>
                    {
                        c.User = t;
                        return c;
                    }, new
                    {
                        BotId = botId,
                        UserId = userId
                    },
                    splitOn: "Id");
                return result.FirstOrDefault();
            }
        }

        public void UpdateClientEmail(string botId, string userId, string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(@"UPDATE [dbo].[Clients]
                   SET [LastUpdated] = getutcdate(),
                     [UserEmail] = @UserEmail
                 WHERE [BotId] = @BotId AND [UserId] = @UserId", new
                {
                    UserEmail = email,
                    BotId = botId,
                    UserId = userId
                });
            }
        }

        public IEnumerable<Subscription> GetSubscriptions(string botId, string userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Subscription>(@"SELECT s.* FROM Clients as c
                    INNER JOIN Subscription as s ON (c.Id = s.ClientId)
                    WHERE
                    c.UserId = @UserId AND c.BotId = @BotId", new
                {
                    UserId = userId,
                    BotId = botId
                });
            }
        }

        public IEnumerable<Subscription> GetActiveSubscriptions(string botId, string userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Subscription>(@"SELECT * FROM Clients as c
                    INNER JOIN Subscription ON (c.Id = ClientId)
                    WHERE
                    c.UserId = @UserId AND c.BotId = @BotId
                    AND Active = true", new
                {
                    UserId = userId,
                    BotId = botId
                });
            }
        }

        public IEnumerable<ExpiredSubscription> GetExpiredActiveSubscriptions()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<ExpiredSubscription>(@"SELECT * FROM Clients as c
                    INNER JOIN Subscription ON (c.Id = ClientId)
                    AND ExpiryDate < getutcdate()
                    AND Active = 1");
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
                      ,[Currency] = @Currency,
                       [Active] = @Active
                 WHERE [Id] = @Id", new
                {
                    subscription.ExpiryDate,
                    subscription.Name,
                    subscription.Fee,
                    subscription.Currency,
                    subscription.Id,
                    subscription.Active
                });
            }
        }

        public int RenewSubscription(string botId, string userId, string subscriptionName, short subscriptionMonths,
            short subscriptionDays, string subscriptionCurrency, decimal subscriptionFee)
        {
            List<Subscription> subscriptions = GetSubscriptions(botId, userId).ToList();

            var oldSubscription = subscriptions.FirstOrDefault(x => x.Name == subscriptionName);

            if ((oldSubscription != null) && (oldSubscription.ExpiryDate > DateTime.Now))
            {
                oldSubscription.ExpiryDate = oldSubscription.ExpiryDate.AddMonths(subscriptionMonths);
                oldSubscription.ExpiryDate = oldSubscription.ExpiryDate.AddDays(subscriptionDays);
                oldSubscription.Fee = subscriptionFee;
                oldSubscription.Currency = subscriptionCurrency;
                oldSubscription.Active = true;
                UpdateSubscription(oldSubscription);
            }
            else
            {
                AddNewSubscription(botId, userId, new Subscription()
                {
                    Name = subscriptionName,
                    Currency = subscriptionCurrency,
                    Fee = subscriptionFee,
                    Active = true
                }, DateTime.Now.AddMonths(subscriptionMonths).AddDays(subscriptionDays));
            }

            return 1;
        }

        private void AddNewSubscription(string botId, string userId, Subscription subscription, DateTime expiryDate)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(@"
                    DECLARE @ClientId uniqueIdentifier
                    SET @ClientId = (SELECT c.Id FROM Clients as c
                    WHERE c.UserId = @UserId AND c.BotId = @BotId)

                    INSERT INTO [dbo].[Subscription]
                               ([Id]
                               ,[CreatedOn]
                               ,[LastUpdated]
                               ,[ExpiryDate]
                               ,[Name]
                               ,[Fee]
                               ,[ClientId]
                               ,[Currency]
                               ,[Active])
                         VALUES
                               (NEWID()
                               ,getutcdate()
                               ,getutcdate()
                               ,@ExpiryDate
                               ,@Name
                               ,@Fee
                               ,@ClientId
                               ,@Currency,
                                @Active)
                    ", new
                {
                    ExpiryDate = expiryDate,
                    subscription.Name,
                    subscription.Fee,
                    subscription.Currency,
                    UserId = userId,
                    BotId = botId,
                    Active = true
                });
            }
        }

        public int AddClient(Client client)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Execute("Add_Client", new
                    {
                        UserId = client.User.Id,
                        Firstname = client.User.FirstName,
                        Lastname = client.User.LastName,
                        Username = client.User.UserName,
                        client.User.AccessHash,
                        client.ChatId,
                        client.BotId,
                        client.UserEmail
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void DeactivateSubscription(Guid subscriptionId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(@"UPDATE [dbo].[Subscription]
                   SET [LastUpdated] = getutcdate(),
                       [Active] = 0
                 WHERE [Id] = @Id", new
                {
                    Id = subscriptionId,
                });
            }
        }
    }

    public interface IClientService
    {
        int AddClient(Client client);
        IEnumerable<Client> GetClientsByBotId(string botId);
        Client GetClient(string botId, string userId);
        void UpdateClientEmail(string botId, string userId, string email);
        int RenewSubscription(string botId, string userId, string subscriptionName, short subscriptionMonths,
            short subscriptionDays, string subscriptionCurrency, decimal subscriptionFee);
        IEnumerable<Subscription> GetActiveSubscriptions(string botId, string userId);
        IEnumerable<Subscription> GetSubscriptions(string botId, string userId);
        IEnumerable<ExpiredSubscription> GetExpiredActiveSubscriptions();
        void DeactivateSubscription(Guid subscriptionId);
    }
}