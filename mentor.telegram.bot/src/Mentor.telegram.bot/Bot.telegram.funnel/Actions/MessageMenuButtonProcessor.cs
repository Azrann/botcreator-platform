using System.Collections.Generic;
using System.Threading;

namespace Bot.telegram.funnel.Actions
{
    public class MessageMenuButtonProcessor : ActionProcessor<MessageMenuButtonAction>
    {
        private readonly IUserInterfaceService _userInterfaceService;

        public MessageMenuButtonProcessor(IUserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public override bool Execute(MessageMenuButtonAction action, Dictionary<string, object> parameters)
        {
            if (action.RemovePreviousMessage)
            {
                _userInterfaceService.RemovePreviousMessage(parameters);
            }

            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }
            _userInterfaceService.SendMessageWithMenuButtons(action.Message, action.Buttons, parameters);
            return true;
        }
    }
}