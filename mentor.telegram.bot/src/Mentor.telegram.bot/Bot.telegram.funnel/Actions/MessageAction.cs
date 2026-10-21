namespace Bot.telegram.funnel.Actions
{
    public class MessageAction : FunnelAction
    {
        public string Message { get; set; }
        public bool RemovePreviousMessage { get; set; }
        public int Delay { get; set; }

        public MessageAction(string message, bool removePreviousMessage = true, int delay = 0)
        {
            Message = message;
            RemovePreviousMessage = removePreviousMessage;
            Delay = delay;
        }
    }

    public class WidgetMessageAction : FunnelAction
    {
        public string Message { get; set; }
        public bool RemovePreviousMessage { get; set; }
        public int Delay { get; set; }

        public WidgetMessageAction(string message, bool removePreviousMessage = true, int delay = 0)
        {
            Message = message;
            RemovePreviousMessage = removePreviousMessage;
            Delay = delay;
        }
    }

    public class WidgetMessageForKeyAction : FunnelAction
    {
        public string Key { get; set; }
        public bool RemovePreviousMessage { get; set; }
        public int Delay { get; set; }

        public WidgetMessageForKeyAction(string key, bool removePreviousMessage = true, int delay = 0)
        {
            Key = key;
            RemovePreviousMessage = removePreviousMessage;
            Delay = delay;
        }
    }
}