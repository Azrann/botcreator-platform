using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class FindChatIdFromMessageTextProcessor : ActionProcessorAsync<FindChatIdFromMessageTextActionAsync>
    {
        private readonly IChannelService _channelService;

        public FindChatIdFromMessageTextProcessor(IChannelService channelService)
        {
            _channelService = channelService;
        }

        public override async Task<bool> ExecuteAsync(FindChatIdFromMessageTextActionAsync action, Dictionary<string, object> parameters)
        {
            var username = parameters.GetMessageText();
            parameters.AddUsernameCommand(username);

            var user = await _channelService.GetUserByUserName(username, Guid.NewGuid());
            parameters.AddChatIdCommand(user.Item1);
            parameters.AddUserHashCommand(user.Item2);
            return true;
        }
    }

    public class FindChatIdFromMessageTextActionAsync : FunnelAction
    {
    }
}