namespace Bot.telegram.funnel.Actions
{
    public class CgPaymentInfoCreateActionAsync : FunnelAction
    {
        public double Amount { get; set; }
        public string OriginalCurrency { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public short MonthsDuration { get; set; }
        public short DaysDuration { get; set; }
    }
}