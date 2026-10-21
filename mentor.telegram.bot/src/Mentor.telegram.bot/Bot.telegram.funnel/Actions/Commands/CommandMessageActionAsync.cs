namespace Bot.telegram.funnel.Actions
{
    public class CommandMessageActionAsync : FunnelAction
    {
        public string Message { get; set; }
        public bool RemovePreviousMessage { get; set; }
        public int Delay { get; set; }

        public CommandMessageActionAsync(string message, bool removePreviousMessage = true, int delay = 0)
        {
            Message = message;
            RemovePreviousMessage = removePreviousMessage;
            Delay = delay;
        }
    }
}