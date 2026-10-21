namespace Bot.telegram.funnel.Triggers
{
    public class HasSubscriptionTagTrigger : TagBiggerThan0Trigger
    {
        public HasSubscriptionTagTrigger(): base(TagName.SubscriptionCount)
        {
        }
    }

    public class HasNotSubscriptionTagTrigger : TagNotBiggerThan0Trigger
    {
        public HasNotSubscriptionTagTrigger() : base(TagName.SubscriptionCount)
        {
        }
    }
}