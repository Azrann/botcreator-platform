using System.Collections.Generic;
using System.Threading.Tasks;
using channel.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class ChannelUnBanMyselfProcessor : ActionProcessorAsync<ChannelUnBanMyselfActionAsync>
    {
        private readonly IChannelService _channelService;
        private readonly IChannelStorageService _channelStorageService;

        public ChannelUnBanMyselfProcessor(IChannelService channelService, IChannelStorageService channelStorageService)
        {
            _channelService = channelService;
            _channelStorageService = channelStorageService;
        }

        public override async Task<bool> ExecuteAsync(ChannelUnBanMyselfActionAsync action, Dictionary<string, object> parameters)
        {
            var username = parameters.GetUsername();
            var userId = await _channelService.GetUserByUserName(username, action.CorrelationId);

            foreach (var channelId in action.ChannelIds)
            {
                var channel = _channelStorageService.GetById(channelId);
                await _channelService.UnBanUserFromChannel(userId.Item1, channel.Id, userId.Item2, channel.AccessHash, action.CorrelationId);
            }

            return true;
        }
    }

    public class ChannelUnBanMyselfActionAsync : FunnelAction
    {
        public List<int> ChannelIds { get; set; }
    }


}