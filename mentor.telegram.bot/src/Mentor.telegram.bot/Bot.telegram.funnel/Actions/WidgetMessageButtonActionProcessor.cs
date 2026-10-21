using System.Collections.Generic;
using System.Threading;
using Core.Common.Extensions;

namespace Bot.telegram.funnel.Actions
{
    public class WidgetMessageButtonActionProcessor : ActionProcessor<WidgetMessageButtonAction>
    {
        private readonly IUserInterfaceService _userInterfaceService;

        public WidgetMessageButtonActionProcessor(IUserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public override bool Execute(WidgetMessageButtonAction action, Dictionary<string, object> parameters)
        {
            if (action.RemovePreviousMessage)
            {
                _userInterfaceService.RemovePreviousMessage(parameters);
            }

            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }

            _userInterfaceService.SendMessageWithMenuButtonsAdvance(action.Message.ReplaceParameters(parameters), action.Buttons, parameters);
            return true;
        }
    }
}