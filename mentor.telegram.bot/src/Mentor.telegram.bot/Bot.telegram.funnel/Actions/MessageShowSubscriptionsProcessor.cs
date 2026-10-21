using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class MessageShowSubscriptionsProcessor : ActionProcessor<MessageShowSubscriptionsAction>
    {
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IClientService _clientService;

        public MessageShowSubscriptionsProcessor(IUserInterfaceService userInterfaceService, IClientService clientService)
        {
            _userInterfaceService = userInterfaceService;
            _clientService = clientService;
        }

        public override bool Execute(MessageShowSubscriptionsAction action, Dictionary<string, object> parameters)
        {
            var userId = parameters.GetUserId();
            var botId = parameters.GetBotId();

            var subscriptions = _clientService.GetSubscriptions(botId,userId.ToString());
            if (action.RemovePreviousMessage)
            {
                _userInterfaceService.RemovePreviousMessage(parameters);
            }

            var message = new StringBuilder($"You have {subscriptions.Count()} subscriptions \n");
            foreach (var subscription in subscriptions)
            {
                message.AppendLine(
                    $"🎫 {subscription.Name} : Expiry 📅 {subscription.ExpiryDate.ToShortDateString()} with Fee {subscription.Fee} {subscription.Currency}");
            }
            _userInterfaceService.SendMessage(message.ToString(), parameters);

            return true;
        }
    }
}