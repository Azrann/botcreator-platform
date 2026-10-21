using System.Collections.Generic;

namespace Bot.telegram.funnel.Actions
{
    public class MessageMenuButtonAction : FunnelAction
    {
        public string Message { get; set; }
        public List<List<string>> Buttons { get; set; }
        public bool RemovePreviousMessage { get; set; }
        public int Delay { get; set; }

        public MessageMenuButtonAction()
        {
        }

        public MessageMenuButtonAction(string message, List<List<string>> buttons, bool removePreviousMessage = true,
            int delay = 0)
        {
            Message = message;
            Buttons = buttons;
            RemovePreviousMessage = removePreviousMessage;
            Delay = delay;
        }
    }
}