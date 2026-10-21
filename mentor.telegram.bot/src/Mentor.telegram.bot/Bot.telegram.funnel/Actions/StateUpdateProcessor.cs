using System.Collections.Generic;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class StateUpdateProcessor : ActionProcessor<StateUpdateAction>
    {
        private readonly IStateStorageService _stateStorageService;

        public StateUpdateProcessor(IStateStorageService stateStorageService)
        {
            _stateStorageService = stateStorageService;
        }

        public override bool Execute(StateUpdateAction action, Dictionary<string, object> parameters)
        {
            var botId = parameters.GetBotId();
            var userId = parameters.GetUserId();

            _stateStorageService.AddOrUpdateState(botId,userId.ToString(),action.State,action.Value);
            if (parameters.ContainsKey(action.State))
            {
                parameters[action.State] = action.Value;
            }
            else
            {
                parameters.Add(action.State, action.Value);
            }
            return true;
        }
    }
}