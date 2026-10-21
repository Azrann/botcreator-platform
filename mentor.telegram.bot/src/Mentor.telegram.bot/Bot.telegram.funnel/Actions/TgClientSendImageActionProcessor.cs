using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Common.Extensions;

namespace Bot.telegram.funnel.Actions
{
    public class TgClientSendImageActionProcessor : ActionProcessorAsync<TgClientSendImageActionAsync>
    {
        private readonly ITelegramChatService _telegramChatService;
        public TgClientSendImageActionProcessor(ITelegramChatService telegramChatService)
        {
            _telegramChatService = telegramChatService;
        }

        public override async Task<bool> ExecuteAsync(TgClientSendImageActionAsync action, Dictionary<string, object> parameters)
        {
            // When we arrive here we assume parameters.Username has been set
            Log.LogDeb("TgClientSendImageActionAsync executing ", action.CorrelationId);
            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }

            await _telegramChatService.SendImageByUsername(parameters.GetUsername(), action.ImageName, action.CorrelationId,action.Message.ReplaceParameters(parameters));
            return true;
        }
    }
}