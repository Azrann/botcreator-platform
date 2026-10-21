namespace Bot.telegram.funnel.Actions
{
    public class TgClientMessageActionAsync : FunnelAction
    {
        public string Message { get; set; }
        public int Delay { get; set; }

        public TgClientMessageActionAsync(string message, int delay = 0)
        {
            Message = message;
            Delay = delay;
        }
    }
}