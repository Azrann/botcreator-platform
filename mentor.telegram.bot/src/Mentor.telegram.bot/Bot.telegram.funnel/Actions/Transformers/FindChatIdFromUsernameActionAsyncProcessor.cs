using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class FindChatIdFromUsernameActionAsyncProcessor : ActionProcessorAsync<FindChatIdFromUsernameActionAsync>
    {
        private readonly IChannelService _channelService;

        public FindChatIdFromUsernameActionAsyncProcessor(IChannelService channelService)
        {
            _channelService = channelService;
        }

        public override async Task<bool> ExecuteAsync(FindChatIdFromUsernameActionAsync action, Dictionary<string, object> parameters)
        {
            var username = parameters.GetUsername();

            var user = await _channelService.GetUserByUserName(username, Guid.NewGuid());
            parameters.AddChatIdCommand(user.Item1);
            parameters.AddUserHashCommand(user.Item2);

            return true;
        }
    }
}