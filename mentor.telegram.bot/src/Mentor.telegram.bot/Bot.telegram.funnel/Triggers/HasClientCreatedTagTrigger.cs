namespace Bot.telegram.funnel.Triggers
{
    public class HasClientCreatedTagTrigger : TagAddedTrigger
    {
        public HasClientCreatedTagTrigger(): base(TagName.ClientCreated)
        {
        }
    }

    public class HasNotClientCreatedTagTrigger : TagNotAddedTrigger
    {
        public HasNotClientCreatedTagTrigger() : base(TagName.ClientCreated)
        {
        }
    }
}