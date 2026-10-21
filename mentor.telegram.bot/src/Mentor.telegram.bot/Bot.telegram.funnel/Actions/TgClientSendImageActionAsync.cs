namespace Bot.telegram.funnel.Actions
{
    public class TgClientSendImageActionAsync : FunnelAction
    {
        public string Message { get; set; }
        public string ImageName { get; set; }
        public int Delay { get; set; }

        public TgClientSendImageActionAsync(string imageName, string message, int delay = 0)
        {
            Message = message;
            Delay = delay;
            ImageName = imageName;
        }
    }
}