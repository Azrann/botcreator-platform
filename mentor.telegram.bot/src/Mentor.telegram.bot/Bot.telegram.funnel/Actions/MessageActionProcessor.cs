using System.Collections.Generic;
using System.Threading;

namespace Bot.telegram.funnel.Actions
{
    public class MessageActionProcessor : ActionProcessor<MessageAction>
    {
        private readonly IUserInterfaceService _userInterfaceService;

        public MessageActionProcessor(IUserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public override bool Execute(MessageAction action, Dictionary<string, object> parameters)
        {
            if (action.RemovePreviousMessage)
            {
                _userInterfaceService.RemovePreviousMessage(parameters);
            }

            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }

            _userInterfaceService.SendMessage(action.Message.ReplaceParameters(parameters), parameters);
            return true;
        }
    }
}