namespace Bot.telegram.funnel.Triggers
{
    public class HasEmailTagTrigger : TagAddedTrigger
    {
        public HasEmailTagTrigger(): base(TagName.EmailUpdated)
        {
        }
    }

    public class HasNotEmailTagTrigger : TagNotAddedTrigger
    {
        public HasNotEmailTagTrigger() : base(TagName.EmailUpdated)
        {
        }
    }
}