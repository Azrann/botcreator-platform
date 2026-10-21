using System.Collections.Generic;
using System.Threading;

namespace Bot.telegram.funnel.Actions
{
    public class MessageButtonActionProcessor : ActionProcessor<MessageButtonAction>
    {
        private readonly IUserInterfaceService _userInterfaceService;

        public MessageButtonActionProcessor(IUserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public override bool Execute(MessageButtonAction action, Dictionary<string, object> parameters)
        {
            if (action.RemovePreviousMessage)
            {
                _userInterfaceService.RemovePreviousMessage(parameters);
            }

            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }

            _userInterfaceService.SendMessageWithButtons(action.Message.ReplaceParameters(parameters), action.Buttons, parameters);
            return true;
        }
    }
}