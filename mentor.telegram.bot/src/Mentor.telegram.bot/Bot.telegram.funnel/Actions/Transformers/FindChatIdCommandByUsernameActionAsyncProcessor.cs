using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class FindChatIdCommandByUsernameActionAsyncProcessor : ActionProcessorAsync<FindChatIdCommandByUsernameActionAsync>
    {
        private readonly IChannelService _channelService;

        public FindChatIdCommandByUsernameActionAsyncProcessor(IChannelService channelService)
        {
            _channelService = channelService;
        }

        public override async Task<bool> ExecuteAsync(FindChatIdCommandByUsernameActionAsync action, Dictionary<string, object> parameters)
        {
            var username = action.Username;
            parameters.AddUsernameCommand(username);

            var user = await _channelService.GetUserByUserName(username, Guid.NewGuid());
            parameters.AddChatIdCommand(user.Item1);
            parameters.AddUserHashCommand(user.Item2);
            return true;
        }
    }
}