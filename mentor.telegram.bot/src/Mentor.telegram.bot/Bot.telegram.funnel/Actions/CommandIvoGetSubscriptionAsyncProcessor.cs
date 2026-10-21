using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.boundedContext.Services;
using System.Text;

namespace Bot.telegram.funnel.Actions
{
    public class CommandIvoGetSubscriptionAsyncProcessor : ActionProcessorAsync<CommandIvoGetSubscriptionAsync>
    {
        private readonly IUserInterfaceService _interfaceService;
        private readonly IIvoApi _ivoApi;

        public CommandIvoGetSubscriptionAsyncProcessor(IUserInterfaceService interfaceService, IIvoApi ivoApi)
        {
            _interfaceService = interfaceService;
            _ivoApi = ivoApi;
        }

        public override async Task<bool> ExecuteAsync(CommandIvoGetSubscriptionAsync action, Dictionary<string, object> parameters)
        {
            Log.LogInf($"IvoHttpGetSubscriptionActionAsync Processing", action.CorrelationId);

            var username = parameters.GetMessageText();

            var memberships = await _ivoApi.GetMembershipsForUser(username, action.CorrelationId);

            if (memberships != null && memberships.Count > 0)
            {
                StringBuilder message = new StringBuilder("Memberships: \n\n");
                foreach (var membership in memberships)
                {
                    message.Append(membership);
                }
                await _interfaceService.RemovePreviousMessage(parameters);
                await _interfaceService.SendMessage(message.ToString(), parameters);
            }
            else
            {
                await _interfaceService.SendMessage($"No memberships found for user {username}", parameters);
            }

            return true;
        }
    }

}