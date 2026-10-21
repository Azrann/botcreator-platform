using System.Collections.Generic;
using Mentor.telegram.bot.boundedContext.Services;
using Bot.telegram.funnel.Triggers;

namespace Bot.telegram.funnel.Actions
{
    public class SubscriptionCreateProcessor : ActionProcessor<SubscriptionCreateAction>
    {
        private readonly ITagStorageService _tagStorageService;
        private readonly IClientService _clientService;

        public SubscriptionCreateProcessor(ITagStorageService tagStorageService, IClientService clientService)
        {
            _tagStorageService = tagStorageService;
            _clientService = clientService;
        }

        public override bool Execute(SubscriptionCreateAction action, Dictionary<string, object> parameters)
        {
            var userId = parameters.GetUserId();
            var botId = parameters.GetBotId();
            var adminId = parameters.GetadminId();

            var result = _clientService.RenewSubscription(botId, userId.ToString(), action.Name, action.MonthsDuration, action.DaysDuration, action.Currency,
                action.Fee);
            _tagStorageService.IncrementTag(botId, userId.ToString(), TagName.SubscriptionCount);
            return result == 1;
        }
    }
}