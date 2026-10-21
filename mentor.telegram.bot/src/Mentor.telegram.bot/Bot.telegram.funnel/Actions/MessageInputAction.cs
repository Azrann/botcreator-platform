namespace Bot.telegram.funnel.Actions
{
    public class MessageInputAction : FunnelAction
    {
        public string Message { get; set; }
        public bool RemovePreviousMessage { get; set; }

        public MessageInputAction(string message, bool removePreviousMessage = true)
        {
            Message = message;
            RemovePreviousMessage = removePreviousMessage;
        }
    }
}