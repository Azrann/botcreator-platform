using System.Collections.Generic;

namespace Bot.telegram.funnel.Actions
{
    public class BiMessageResultProcessor : ActionProcessor<BiMessageResultAction>
    {
        private readonly IUserInterfaceService _userInterfaceService;

        public BiMessageResultProcessor(IUserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public override bool Execute(BiMessageResultAction action, Dictionary<string, object> parameters)
        {
            var message = parameters.GetBiMessageResult();
            _userInterfaceService.SendMessage(message.ReplaceParameters(parameters), parameters);
            return true;
        }
    }

    public class BiMessageResultAction : FunnelAction
    {
    }
}