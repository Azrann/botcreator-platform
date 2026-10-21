using System.Collections.Generic;
using System.Threading.Tasks;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class ChannelAddMyselfProcessor : ActionProcessorAsync<ChannelAddMyselfActionAsync>
    {
        private readonly IAdminStorageService _adminStorageService;
        private readonly IChannelService _channelService;

        public ChannelAddMyselfProcessor(IAdminStorageService adminStorageService, IChannelService channelService)
        {
            _adminStorageService = adminStorageService;
            _channelService = channelService;
        }

        public override async Task<bool> ExecuteAsync(ChannelAddMyselfActionAsync action, Dictionary<string, object> parameters)
        {
            var adminId = parameters.GetadminId();
            var userId = parameters.GetUserId();

            var channel = _adminStorageService.GetChannel(adminId.ToString(), action.ChannelId);

            if (channel != null)
            {
                if (channel.Type == TelegramChannelType.Channel)
                {
                   await _channelService.AddUserToChannel(int.Parse(userId), channel.Id.ToString(), channel.AccessHash, action.CorrelationId);
                }
                else
                {
                   await _channelService.AddUserToChat(int.Parse(userId), channel.Id.ToString(), action.CorrelationId);
                }
            }

            return true;
        }
    }
}