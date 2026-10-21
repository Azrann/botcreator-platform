using System.Collections.Generic;
using System.Threading.Tasks;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class ChannelAddClientProcessor : ActionProcessorAsync<ChannelAddClientActionAsync>
    {
        private readonly IAdminStorageService _adminStorageService;
        private readonly IChannelService _channelService;

        public ChannelAddClientProcessor(IAdminStorageService adminStorageService, IChannelService channelService)
        {
            _adminStorageService = adminStorageService;
            _channelService = channelService;
        }

        public override async Task<bool> ExecuteAsync(ChannelAddClientActionAsync action, Dictionary<string, object> parameters)
        {
            var adminId = parameters.GetadminId();
            var username = parameters.GetUsername();

            var channel = _adminStorageService.GetChannel(adminId, action.ChannelId);

            if (channel != null)
            {
                if (channel.Type == TelegramChannelType.Channel)
                {
                   await _channelService.AddUserToChannelByUsername(username, channel.Id.ToString(), channel.AccessHash, action.CorrelationId);
                }
                else
                {
                   await _channelService.AddUserToChatByUsername(username, channel.Id.ToString(), action.CorrelationId);
                }
            }

            return true;
        }
    }
}