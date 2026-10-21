using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Common.Extensions;

namespace Bot.telegram.funnel.Actions
{
    public class CommandClientMessageActionProcessor : ActionProcessorAsync<CommandClientMessageActionAsync>
    {
        private readonly ITelegramChatService _telegramChatService;
        public CommandClientMessageActionProcessor(ITelegramChatService telegramChatService)
        {
            _telegramChatService = telegramChatService;
        }

        public override async Task<bool> ExecuteAsync(CommandClientMessageActionAsync action, Dictionary<string, object> parameters)
        {
            // When we arrive here we assume parameters.UsernameCommand has been set
            Log.LogDeb("CommandClientMessageActionProcessor executing ", action.CorrelationId);
            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }

            await _telegramChatService.SendMessageByUsername(parameters.GetUsernameCommand(), action.Message.ReplaceParameters(parameters), action.CorrelationId);
            return true;
        }
    }

}