using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.boundedContext.Services;
using System.Text;
using System.Linq;

namespace Bot.telegram.funnel.Actions
{
    public class IvoHttpGetSubscriptionActionAsyncProcessor : ActionProcessorAsync<IvoHttpGetSubscriptionActionAsync>
    {
        private readonly IUserInterfaceService _interfaceService;
        private readonly IIvoApi _ivoApi;

        public IvoHttpGetSubscriptionActionAsyncProcessor(IUserInterfaceService interfaceService, IIvoApi ivoApi)
        {
            _interfaceService = interfaceService;
            _ivoApi = ivoApi;
        }

        public override async Task<bool> ExecuteAsync(IvoHttpGetSubscriptionActionAsync action, Dictionary<string, object> parameters)
        {
            Log.LogInf($"IvoHttpGetSubscriptionActionAsync Processing", action.CorrelationId);

            var username = parameters.GetUsername();

            var memberships = await _ivoApi.GetMembershipsForUser(username, action.CorrelationId);

            if (memberships != null && memberships.Any(x => x.Active))
            {
                StringBuilder message = new StringBuilder("Memberships: \n\n");
                foreach (var membership in memberships.Where(x => x.Active))
                {
                    message.Append(membership);
                }
                await _interfaceService.RemovePreviousMessage(parameters);
                await _interfaceService.SendMessage(message.ToString(), parameters);
            }
            else
            {
                await _interfaceService.SendMessage($"No active memberships found for user {username}", parameters);
            }

            return true;
        }
    }

}