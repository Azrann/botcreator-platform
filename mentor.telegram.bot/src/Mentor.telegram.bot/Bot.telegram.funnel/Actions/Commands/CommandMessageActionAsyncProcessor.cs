using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Common.Extensions;

namespace Bot.telegram.funnel.Actions
{
    public class CommandMessageActionAsyncProcessor : ActionProcessorAsync<CommandMessageActionAsync>
    {
        private readonly IUserInterfaceService _userInterfaceService;
        public CommandMessageActionAsyncProcessor(IUserInterfaceService userInterfaceService)
        {
            _userInterfaceService = userInterfaceService;
        }

        public override async Task<bool> ExecuteAsync(CommandMessageActionAsync action, Dictionary<string, object> parameters)
        {
            // When we arrive here we assume parameters.ChatId has been set
            Log.LogDeb("CommandMessageActionAsyncProcessor executing ", action.CorrelationId);
            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }

            await _userInterfaceService.SendMessageCommand(action.Message.ReplaceParameters(parameters), parameters);
            return true;
        }
    }
}