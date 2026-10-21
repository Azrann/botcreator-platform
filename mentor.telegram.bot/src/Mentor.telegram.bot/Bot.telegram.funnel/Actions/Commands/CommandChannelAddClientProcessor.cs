using System.Collections.Generic;
using System.Threading.Tasks;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class CommandChannelAddClientProcessor : ActionProcessorAsync<CommandChannelAddClientActionAsync>
    {
        private readonly IAdminStorageService _adminStorageService;
        private readonly IChannelService _channelService;

        public CommandChannelAddClientProcessor(IAdminStorageService adminStorageService, IChannelService channelService)
        {
            _adminStorageService = adminStorageService;
            _channelService = channelService;
        }

        public override async Task<bool> ExecuteAsync(CommandChannelAddClientActionAsync action, Dictionary<string, object> parameters)
        {
            var adminId = parameters.GetadminId();
            var userId = parameters.GetUserId();
            var username = parameters.GetMessageText();

            if (string.IsNullOrEmpty(username))
            {
                username = parameters.GetUsername();
            }

            var channel = _adminStorageService.GetChannel(adminId.ToString(), action.ChannelId);

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