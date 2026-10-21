using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class FindChatIdActionAsyncProcessor : ActionProcessorAsync<FindChatIdActionAsync>
    {
        private readonly IChannelService _channelService;
        public FindChatIdActionAsyncProcessor(IChannelService channelService)
        {
            _channelService = channelService;
        }

        public override async Task<bool> ExecuteAsync(FindChatIdActionAsync action, Dictionary<string, object> parameters)
        {
            Log.LogDeb("FindChatIdActionAsyncProcessor executing ", action.CorrelationId);

            if (!parameters.ContainsKey(ParametersExtensions.ChatId) || parameters.GetChatId() == 0)
            {
                var username = parameters.GetUsername();
                var user = await _channelService.GetUserByUserName(username, Guid.NewGuid());
                parameters.AddChatId(user.Item1);
                parameters.AddUserId(user.Item1.ToString());
                parameters.AddUserHash(user.Item2);
                parameters.AddChatIdCommand(user.Item1);
                Log.LogDeb($"FindChatIdActionAsyncProcessor Added chat Id {user.Item1} ", action.CorrelationId);
            }

            return true;
        }
    }
}