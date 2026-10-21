using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.bot.boundedContext.Services;
using Mentor.telegram.bot.api;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;

namespace Bot.telegram.funnel.Actions
{
    public class ClientRequesTalkProcessor : ActionProcessorAsync<ClientRequesTalkActionAsync>
    {
        private readonly IChatLiveUserService _chatLiveUserService;
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IPubnubMessageBus _pubnubMessageBus;
        private readonly ILiveChatQueue _liveChatQueue;

        public ClientRequesTalkProcessor(IChatLiveUserService chatLiveUserService, 
            IUserInterfaceService userInterfaceService, IPubnubMessageBus pubnubMessageBus,
            ILiveChatQueue liveChatQueue)
        {
            _chatLiveUserService = chatLiveUserService;
            _userInterfaceService = userInterfaceService;
            _pubnubMessageBus = pubnubMessageBus;
            _liveChatQueue = liveChatQueue;
        }

        public override async Task<bool> ExecuteAsync(ClientRequesTalkActionAsync action, Dictionary<string, object> parameters)
        {
            Guid correlationId = action.CorrelationId;
            var botId = parameters.GetBotId();
            var channelId = parameters.GetChatId().ToString();

           
                Log.LogInf($"Retrieving user for botId={botId} and channelId={channelId} ", correlationId);
                var user = _chatLiveUserService.GetChatLiveUserByChannelId(botId, channelId);

                var channel = GetRandomChannelId(); ;
               
                _pubnubMessageBus.Start(correlationId);
                _pubnubMessageBus.Grantd(user.Auth, channel, true, true);
                await _liveChatQueue.AddClientToQueue(new QueueItem()
                {
                    BotId = user.BotId,
                    ChannelId = channel,
                    AuthKey = user.Auth
                });

                _pubnubMessageBus.Publish(new
                    {
                        action = "join",
                        user = new
                        {
                            userId = user.Id,
                            userName = user.Name,
                            AvatarUrl = ""
                        }
                    },
                    user.BotId
                );

                await _userInterfaceService.SendMessageCommand($"ChangeChannel {channel}",parameters);
                return true;
        }

        private static string GetRandomChannelId()
        {
            Random rnd = new Random();
            string channel = rnd.Next(1, 99999999).ToString();
            return channel;
        }
    }

    public class ClientRequesTalkActionAsync : FunnelAction
    {
    }
}