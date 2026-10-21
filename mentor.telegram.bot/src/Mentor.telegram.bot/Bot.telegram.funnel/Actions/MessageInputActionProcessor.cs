using System.Collections.Generic;

namespace Bot.telegram.funnel.Actions
{
    public class MessageInputActionProcessor : ActionProcessor<MessageInputAction>
    {
        private readonly IUserInterfaceService _userInterfaceService;

        public MessageInputActionProcessor(IUserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public override bool Execute(MessageInputAction action, Dictionary<string, object> parameters)
        {
            if (action.RemovePreviousMessage)
            {
                _userInterfaceService.RemovePreviousMessage(parameters);
            }

            _userInterfaceService.SendInputMessage(action.Message.ReplaceParameters(parameters), parameters);
            return true;
        }
    }
}