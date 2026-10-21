using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class IvoSendAboutToExpireNotificationProcessor : ActionProcessorAsync<IvoSendAboutToExpireNotificationAsync>
    {
        private readonly IChannelService _channelService;
        private readonly IUserInterfaceService _interfaceService;
        private readonly IIvoApi _ivoApi;
        private Guid CorrelationId;
        public IvoSendAboutToExpireNotificationProcessor(IChannelService channelService,
            IUserInterfaceService interfaceService, IIvoApi ivoApi)
        {
            _channelService = channelService;
            _interfaceService = interfaceService;
            _ivoApi = ivoApi;
        }

        public override async Task<bool> ExecuteAsync(IvoSendAboutToExpireNotificationAsync actionAsync, Dictionary<string, object> parameters)
        {
            CorrelationId = actionAsync.CorrelationId;
            Log.LogInf("IvoSendAboutToExpireNotificationProcessor executing", CorrelationId);

            var telegramUser = await _channelService.GetUserByUserName("REPLACE_WITH_YOUR_VALUE", CorrelationId);
            var adminUser = await _channelService.GetUserByUserName("verifiedcrypto", CorrelationId);

            var erroredUsers = new List<string>();
            foreach (var expirationData in actionAsync.ExpirationData)
            {
                var almostExpireUsers = await _ivoApi.GetUsersThatExpireInDays(expirationData.Days, expirationData.MembershipName, CorrelationId);
                if (almostExpireUsers.Count > 0)
                {
                    var users = await FilterUsersAlreadyRenewed(almostExpireUsers,expirationData.Days);
                    var userList = JsonConvertExt.SerializeObject(users);
                    await _interfaceService.SendMessageCommandByUserId(
                        $"The following {almostExpireUsers.Count} users expire in {expirationData.Days} \n\n" +
                        $"{userList}", telegramUser.Item1, parameters);
                    await _interfaceService.SendMessageCommandByUserId(
                        $"The following {almostExpireUsers.Count} users expire in {expirationData.Days} \n\n" +
                        $"{userList}", adminUser.Item1, parameters);

                    foreach (var user in users)
                    {
                        var telegramUserData = await _channelService.GetUserByUserName(user, CorrelationId);
                        if (!await _interfaceService.SendMessageCommandByUserId(expirationData.Message.Replace("[Username]", user), telegramUserData.Item1, parameters))
                        {
                            erroredUsers.Add(user);
                        }
                    }
                }
            }

            if (erroredUsers.Any())
            {
                var userListError = JsonConvertExt.SerializeObject(erroredUsers);
                Log.LogErr($"We could not send notification about to expired to the following users {userListError}", CorrelationId);
            }
            return true;
        }

        private async Task<List<string>> FilterUsersAlreadyRenewed(List<string> almostExpiredUsers,int days)
        {
            var result = new List<string>();

            foreach (var user in almostExpiredUsers)
            {
                var subscriptions = await _ivoApi.GetMembershipsForUser(user,CorrelationId);
                if (!subscriptions.Any(x => (x.ExpiryDate.Date - DateTime.Now.Date) > TimeSpan.FromDays(days)))
                {
                    result.Add(user);
                }
            }
            return result;
        }
    }
}