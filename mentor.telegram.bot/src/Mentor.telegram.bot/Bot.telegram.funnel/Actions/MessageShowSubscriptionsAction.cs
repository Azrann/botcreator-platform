namespace Bot.telegram.funnel.Actions
{
    public class MessageShowSubscriptionsAction : FunnelAction
    {
        public MessageShowSubscriptionsAction(bool removePreviousMessage = true)
        {
            RemovePreviousMessage = removePreviousMessage;
        }

        public bool RemovePreviousMessage { get; set; }
    }
}