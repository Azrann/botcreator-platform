using Mentor.telegram.bot.models;

namespace Bot.telegram.funnel.Actions
{
    public class PaymentInfoCreateActionAsync : FunnelAction
    {
        public double Amount { get; set; }
        public string OriginalCurrency { get; set; }
        public string BuyerCurrency { get; set; }
        public PaymentProvider PaymentProvider { get; set; }
        public string ItemName { get; set; }

        public short MonthsDuration { get; set; }
        public short DaysDuration { get; set; }

        public PaymentInfoCreateActionAsync()
        {          
        }
    }
}