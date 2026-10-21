namespace Bot.telegram.funnel.Actions
{
    public class SubscriptionCreateAction : FunnelAction
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal Fee { get; set; }
        public short MonthsDuration { get; set; }
        public short DaysDuration { get; set; }
    }


}