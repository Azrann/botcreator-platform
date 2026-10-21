using Mentor.telegram.bot.api;
using Mentor.telegram.bot.api.services;
using Mentor.telegram.shared.BotTemplates;
using Mentor.telegram.shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.bot.boundedContext.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Bot.telegram.funnel;
using Mentor.telegram.bot.sdk.Dtos;

namespace Mentor.telegram.bot.sdk
{
    public class BotApi
    {
        private readonly IPubnubMessageBus _pubnubMessageBus;
        private readonly ServiceProvider _serviceProvider;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(BotApi));

        public BotApi()
        {
            var sc = new ServiceCollection();

            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("appsettings.json");

            var config = configBuilder.Build();
            var startup = new BootstrapServicesSdk(config);
            startup.ConfigureCoreServices(sc);

            _serviceProvider = sc.BuildServiceProvider();
            BootstrapServicesSdk.Initialize(_serviceProvider);

            _pubnubMessageBus = _serviceProvider.GetRequiredService<IPubnubMessageBus>();
        }

        public bool CreateBot(string botId, IBotDefinitionDto botDto)
        {
            Guid correlationId = Guid.NewGuid();
            Log.LogInf($"CreateBot received botId={botId}", correlationId);
            IBotDefinitionStorageService botDefinitionStorageService = _serviceProvider.GetRequiredService<IBotDefinitionStorageService>();
            IBotDefinition bot = BotDefinitionMapper.Map(botDto);
            return botDefinitionStorageService.InsertOrUpdate(botId, bot);
        }

        public bool DeployBot(string botId)
        {
            Guid correlationId = Guid.NewGuid();
            Log.LogInf($"DeployBot received botId={botId}", correlationId);
            BotCreator creator = _serviceProvider.GetRequiredService<BotCreator>();
            return creator.DeployBot(botId);
        }

        public IBotDefinition GetBotDefinition(string botId)
        {
            Guid correlationId = Guid.NewGuid();
            Log.LogInf($"GetBotDefinition received botId={botId}", correlationId);
            BotCreator creator = _serviceProvider.GetRequiredService<BotCreator>();
            return creator.GetBotDefinition(botId);
        }

        public string AgentAuth(string botId)
        {
            Guid correlationId = Guid.NewGuid();
            string authKey = Guid.NewGuid().ToString().Replace("-", "");
            var channel = botId;

            try
            {
                Log.LogInf($"AgentAuth channel received", correlationId);

                _pubnubMessageBus.Start(correlationId);
                _pubnubMessageBus.Grantd(authKey, channel, true, false);


                return authKey;
            }
            catch (Exception ex)
            {
                Log.LogErr($"AgentAuth called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return string.Empty;
            }
        }

        public async Task<ConnectResponse> Connect(Agent customerServiceAgent)
        {
            Guid correlationId = Guid.NewGuid();

            try
            {
                var liveChatQueue =_serviceProvider.GetRequiredService<ILiveChatQueue>();
                var chatLiveUserService = _serviceProvider.GetRequiredService<IChatLiveUserService>();

                Log.LogInf($"ConnectToChatClient user received request {JsonConvertExt.SerializeObject(customerServiceAgent)}", correlationId);

                var liveChatUser = await liveChatQueue.GetClientFromQueue(customerServiceAgent.BotId);
                Log.LogInf($"ConnectToChatClient GetClientFromQueue object {JsonConvertExt.SerializeObject(liveChatUser)}", correlationId);

                if (liveChatUser == null)
                {
                    return null;
                }

                _pubnubMessageBus.Start(correlationId);
                _pubnubMessageBus.Grantd(customerServiceAgent.AuthKey, liveChatUser.ChannelId, true, true);

                var userInfo = chatLiveUserService.GetChatLiveUserByToken(liveChatUser.BotId, liveChatUser.AuthKey);

                _pubnubMessageBus.Publish(new
                {
                    action = "leave",
                    user = new
                    {
                        userId = userInfo.Id,
                    }
                }, customerServiceAgent.BotId);

                return
                    new ConnectResponse
                    {
                        ChannelId = liveChatUser.ChannelId,
                        Name = userInfo.Name,
                        UserId = userInfo.Id
                    };
            }
            catch (Exception ex)
            {
                Log.LogErr($"ConnectToChatClient channel called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return null;
            }
        }
    }

    public class ConnectResponse
    {
        public string ChannelId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
