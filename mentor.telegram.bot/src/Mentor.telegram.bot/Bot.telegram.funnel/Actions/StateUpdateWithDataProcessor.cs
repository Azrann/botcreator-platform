using System.Collections.Generic;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class StateUpdateWithDataProcessor : ActionProcessor<StateUpdateActionWithData>
    {
        private readonly IStateStorageService _stateStorageService;

        public StateUpdateWithDataProcessor(IStateStorageService stateStorageService)
        {
            _stateStorageService = stateStorageService;
        }

        public override bool Execute(StateUpdateActionWithData action, Dictionary<string, object> parameters)
        {
            var botId = parameters.GetBotId();
            var userId = parameters.GetUserId();
            var message = parameters.GetCallbackData();

            if (string.IsNullOrEmpty(message))
            {
                message = parameters.GetMessageText();
            }

            _stateStorageService.AddOrUpdateState(botId,userId.ToString(),action.State, message);
            if (parameters.ContainsKey(action.State))
            {
                parameters[action.State] = message;
            }
            else
            {
                parameters.Add(action.State, message);
            }
            return true;
        }
    }
}