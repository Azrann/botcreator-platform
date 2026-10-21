using System;
using System.Collections.Generic;
using Core.Common.SqlDataAccess;

namespace Mentor.telegram.bot.models
{
    public class EventInfo
    {
        public PaymentInfo PaymentInfo { get; set; }
        public SubscriptionInfo SubscriptionInfo { get; set; }
        public string Command { get; set; }
        public string Username { get; set; }
        public List<long> Channels { get; set; }
    }

    public class PaymentInfo : Entity<Guid>
    {
        public long ChatId { get; set; }
        public string BotId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public PaymentProvider Provider { get; set; }
        public int PaymentStatus { get; set; }
        public decimal OriginalAmount { get; set; }
        public string OriginalCurrency { get; set; }
        public string Tag { get; set; }
        public short SubscriptionMonths { get; set; }
        public short SubscriptionDays { get; set; }
        public string TransactionId { get; set; }
        public string BuyerCurrency { get; set; }
        public decimal BuyerAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public int ReceivedConfirms { get; set; }
        public string CryptoTxId { get; set; }
        public string ItemName { get; set; }
        public decimal Fee { get; set; }
        public string WalletAddress { get; set; }
    }
    
    public enum PaymentProvider
    {
        Coinpayments,
        Stripe,
        Paypal,
        CoingGate
    }

    public class SubscriptionInfo : Entity<Guid>
    {
        public long ChatId { get; set; }
        public string BotId { get; set; }
        public string UserId { get; set; }
        public string Tag { get; set; }
        public Guid SubscriptionId { get; set; }
        public string ItemName { get; set; }
    }
}
