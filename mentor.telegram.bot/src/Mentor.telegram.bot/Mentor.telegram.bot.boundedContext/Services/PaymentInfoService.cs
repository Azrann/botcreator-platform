using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Mentor.telegram.bot.models;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public class PaymentInfoService : IPaymentInfoService
    {
        private readonly string _connectionString;

        public PaymentInfoService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddPaymentInfo(PaymentInfo paymentInfo)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(@"INSERT INTO [dbo].[PaymentInfo]
                                       ([Id]
                                       ,[CreatedOn]
                                       ,[LastUpdated]
                                       ,[ChatId]
                                       ,[BotId]
                                       ,[UserId]
                                       ,[Provider]
                                       ,[PaymentStatus]
                                       ,[OriginalAmount]
                                       ,[OriginalCurrency]
                                       ,[Tag]
                                       ,[SubscriptionMonths]
                                       ,[SubscriptionDays]
                                       ,[TransactionId]
                                       ,[BuyerCurrency]
                                       ,[BuyerAmount]
                                       ,[ReceivedAmount]
                                       ,[ReceivedConfirms]
                                       ,[CryptoTxId]
                                       ,[ItemName]
                                       ,[Fee]
                                       ,[WalletAddress]
                                       ,[Username])
                                 VALUES
                                       (@Id
                                        ,getutcdate()
                                        ,getutcdate()
                                        ,@ChatId
                                        ,@BotId
                                        ,@UserId
                                        ,@Provider
                                        ,@PaymentStatus
                                        ,@OriginalAmount
                                        ,@OriginalCurrency
                                        ,@Tag
                                        ,@SubscriptionMonths
                                        ,@SubscriptionDays
                                        ,@TransactionId
                                        ,@BuyerCurrency
                                        ,@BuyerAmount
                                        ,@ReceivedAmount
                                        ,@ReceivedConfirms
                                        ,@CryptoTxId
                                        ,@ItemName
                                        ,@Fee
                                        ,@WalletAddress
                                        ,@Username)", new
                    {
                        paymentInfo.Id,
                        paymentInfo.ChatId,
                        paymentInfo.BotId,
                        paymentInfo.UserId,
                        paymentInfo.Provider,
                        paymentInfo.PaymentStatus,
                        paymentInfo.OriginalAmount,
                        paymentInfo.OriginalCurrency,
                        paymentInfo.Tag,
                        paymentInfo.SubscriptionMonths,
                        paymentInfo.SubscriptionDays,
                        paymentInfo.TransactionId,
                        paymentInfo.BuyerCurrency,
                        paymentInfo.BuyerAmount,
                        paymentInfo.ReceivedAmount,
                        paymentInfo.ReceivedConfirms,
                        paymentInfo.CryptoTxId,
                        paymentInfo.ItemName,
                        paymentInfo.Fee,
                        paymentInfo.WalletAddress,
                        paymentInfo.Username
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public IEnumerable<PaymentInfo> GetPaymentInfosByBotId(string botId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<PaymentInfo>(@"SELECT * FROM [dbo].[PaymentInfo]
                    WHERE BotId = @BotId", new
                    {
                        BotId = botId
                    });
            }
        }

        public void UpdatePaymentInfoStatus(string transactionId, int paymentStatus)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(@"UPDATE [dbo].[PaymentInfo]
                   SET [LastUpdated] = getutcdate(),
                     [PaymentStatus] = @PaymentStatus
                 WHERE [TransactionId] = @TransactionId", new
                {
                    PaymentStatus = paymentStatus,
                    TransactionId = transactionId
                });
            }
        }

        public void UpdatePaymentInfo(PaymentInfo paymentInfo)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(@"UPDATE [dbo].[PaymentInfo]
                   SET [LastUpdated] = getutcdate(),
                    [PaymentStatus] = @PaymentStatus,
                    [ReceivedAmount] = @ReceivedAmount,
                    [ReceivedConfirms] = @ReceivedConfirms,
                    [CryptoTxId] = @CryptoTxId,
                    [Fee] = @Fee
                 WHERE [Id] = @Id", new
                {
                    paymentInfo.Id,
                    paymentInfo.ReceivedAmount,
                    paymentInfo.ReceivedConfirms,
                    paymentInfo.CryptoTxId,
                    paymentInfo.Fee,
                    paymentInfo.PaymentStatus
                });
            }
        }

        public void UpdatePaymentInfoWithBuyerInfo(PaymentInfo paymentInfo)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(@"UPDATE [dbo].[PaymentInfo]
                   SET [LastUpdated] = getutcdate(),
                    [PaymentStatus] = @PaymentStatus,
                    [ReceivedAmount] = @ReceivedAmount,
                    [ReceivedConfirms] = @ReceivedConfirms,
                    [CryptoTxId] = @CryptoTxId,
                    [Fee] = @Fee,
                    [BuyerCurrency] = @BuyerCurrency,
                    [BuyerAmount] = @BuyerAmount,
                    [TransactionId] = @TransactionId
                 WHERE [Id] = @Id", new
                {
                    paymentInfo.Id,
                    paymentInfo.ReceivedAmount,
                    paymentInfo.ReceivedConfirms,
                    paymentInfo.CryptoTxId,
                    paymentInfo.Fee,
                    paymentInfo.PaymentStatus,
                    paymentInfo.BuyerAmount,
                    paymentInfo.BuyerCurrency,
                    paymentInfo.TransactionId
                });
            }
        }

        public IEnumerable<PaymentInfo> GetPaymentInfos(string botId, string userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<PaymentInfo>(@"SELECT * FROM [dbo].[PaymentInfo]
                    WHERE
                    UserId = @UserId AND BotId = @BotId", new {
                    UserId = userId, BotId = botId
                });
            }
        }

        public PaymentInfo GetPaymentInfoByTransactionId(string transactionId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<PaymentInfo>(@"SELECT * FROM [dbo].[PaymentInfo]
                    WHERE
                    TransactionId = @TransactionId", new
                {
                    TransactionId = transactionId
                });
            }
        }

        public IEnumerable<PaymentInfo> GetActivePaymentInfos(string botId, string userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<PaymentInfo>(@"SELECT * FROM [dbo].[PaymentInfo]
                    WHERE
                    UserId = @UserId AND BotId = @BotId
                    AND PaymentStatus < 100 AND PaymentStatus > -1", new
                {
                    UserId = userId,
                    BotId = botId
                });
            }
        }

        public PaymentInfo GetPaymentInfoById(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<PaymentInfo>(@"SELECT * FROM [dbo].[PaymentInfo]
                    WHERE
                    Id = @Id", new
                {
                    Id = id
                });
            }
        }
    }

    public interface IPaymentInfoService
    {
        int AddPaymentInfo(PaymentInfo paymentInfo);
        IEnumerable<PaymentInfo> GetPaymentInfosByBotId(string botId);
        void UpdatePaymentInfoStatus(string transactionId, int paymentStatus);
        void UpdatePaymentInfo(PaymentInfo paymentInfo);
        void UpdatePaymentInfoWithBuyerInfo(PaymentInfo paymentInfo);
        IEnumerable<PaymentInfo> GetActivePaymentInfos(string botId, string userId);
        IEnumerable<PaymentInfo> GetPaymentInfos(string botId, string userId);
        PaymentInfo GetPaymentInfoByTransactionId(string transactionId);
        PaymentInfo GetPaymentInfoById(string id);
    }
}
