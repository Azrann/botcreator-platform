using System.Collections.Generic;

namespace Bot.telegram.funnel.Actions
{
    public class WidgetMessageButtonAction : FunnelAction
    {
        public string Message { get; set; }
        public List<List<Button>> Buttons { get; set; }
        public bool RemovePreviousMessage { get; set; }
        public int Delay { get; set; }

        public WidgetMessageButtonAction()
        {
        }

        public WidgetMessageButtonAction(string message, List<List<Button>> buttons, bool removePreviousMessage = true,
            int delay = 0)
        {
            Message = message;
            Buttons = buttons;
            RemovePreviousMessage = removePreviousMessage;
            Delay = delay;
        }
    }
}