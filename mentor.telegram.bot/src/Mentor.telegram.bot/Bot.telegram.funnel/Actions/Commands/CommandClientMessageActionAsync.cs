namespace Bot.telegram.funnel.Actions
{
    public class CommandClientMessageActionAsync : FunnelAction
    {
        public string Message { get; set; }
        public int Delay { get; set; }

        public CommandClientMessageActionAsync(string message, int delay = 0)
        {
            Message = message;
            Delay = delay;
        }
    }
}