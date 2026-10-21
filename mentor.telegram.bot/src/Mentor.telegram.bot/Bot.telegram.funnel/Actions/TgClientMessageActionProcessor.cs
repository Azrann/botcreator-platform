using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Common.Extensions;

namespace Bot.telegram.funnel.Actions
{
    public class TgClientMessageActionProcessor : ActionProcessorAsync<TgClientMessageActionAsync>
    {
        private readonly ITelegramChatService _telegramChatService;
        public TgClientMessageActionProcessor(ITelegramChatService telegramChatService)
        {
            _telegramChatService = telegramChatService;
        }

        public override async Task<bool> ExecuteAsync(TgClientMessageActionAsync action, Dictionary<string, object> parameters)
        {
            // When we arrive here we assume parameters.Username has been set
            Log.LogDeb("TgClientMessageActionAsync executing ", action.CorrelationId);
            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }

            await _telegramChatService.SendMessageByUsername(parameters.GetUsername(), action.Message.ReplaceParameters(parameters), action.CorrelationId);
            return true;
        }
    }
}