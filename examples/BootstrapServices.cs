using System;
using System.IO;
using channel.telegram.bot.boundedContext.Services;
using Core.Common.Extensions;
using Core.Common.NoSqlDataAccess.DataAccess;
using log4net;
using Mentor.bot.boundedContext.Services;
using Mentor.telegram.bot.api.services.Infrastructure;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.api.Services.UserInterface;
using Mentor.telegram.bot.api.test;
using Mentor.telegram.bot.boundedContext;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Mentor.telegram.bot.models.Facebook;
using Mentor.telegram.bot.models.Paypal;
using Mentor.telegram.funnel;
using Mentor.telegram.funnel.Actions;
using Mentor.telegram.shared;
using Mentor.telegram.shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace Mentor.telegram.bot.api.services
{
    public class BootstrapServices
    {
        public BootstrapServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureCoreServices(IServiceCollection services)
        {
            // services.AddCors();

            var coreConnectionString = Configuration.GetConnectionString("MentorDatabase");
            var botConnectionString = Configuration.GetConnectionString("BotDatabase");
            var queueConnectionString = Configuration.GetConnectionString("QueueStorage");

            services.AddDbContext<MentorDbContext>(options => options.UseSqlServer(coreConnectionString));
            services.AddDbContext<BotDbContext>(options => options.UseSqlServer(botConnectionString));

            services.AddTransient<IUpdateService, UpdateService>();
            services.AddTransient<IUserInterfaceService, TelegramUserInterfaceService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddTransient<IBlockchainService, BlockchainService>();
            services.AddTransient<IChannelService, TelegramChannelService>();
            services.AddTransient<ITelegramChatService, TelegramChatService>();
            services.AddTransient<IClientService, ClientService>(x => new ClientService(coreConnectionString));
            services.AddTransient<IInitializacionService, InitializationService>();
            services.AddTransient<INoSqlDb<Funnel>, DapperNoSql<Funnel>>(x =>
                new DapperNoSql<Funnel>(coreConnectionString));
            services.AddTransient<INoSqlDb<Bot>, DapperNoSql<Bot>>(x => new DapperNoSql<Bot>(coreConnectionString));
            services.AddTransient<INoSqlDb<BotConfig>, DapperNoSql<BotConfig>>(x =>
                new DapperNoSql<BotConfig>(coreConnectionString));
            services.AddTransient<INoSqlDb<AdminData>, DapperNoSql<AdminData>>(x =>
                new DapperNoSql<AdminData>(coreConnectionString));
            services.AddTransient<INoSqlDb<TelegramChannelData>, DapperNoSql<TelegramChannelData>>(x =>
                new DapperNoSql<TelegramChannelData>(coreConnectionString));
            services.AddTransient<IFunnelStorageService, FunnelStorageService>();
            services.AddTransient<IBotStorageService, BotStorageService>();
            services.AddTransient<IBotConfigStorageService, BotConfigStorageService>();
            services.AddTransient<IFunnelInitializationService, FunnelInitializationService>();
            services.AddTransient<INoSqlDb<UserTags>, DapperNoSql<UserTags>>(x =>
                new DapperNoSql<UserTags>(coreConnectionString));
            services.AddTransient<ITagStorageService, TagStorageService>();
            services.AddTransient<INoSqlDb<UserStates>, DapperNoSql<UserStates>>(x =>
                new DapperNoSql<UserStates>(coreConnectionString));
            services.AddTransient<IStateStorageService, StateStorageService>();
            services.AddTransient<IPaymentInfoService, PaymentInfoService>(x =>
                new PaymentInfoService(coreConnectionString));
            services.AddTransient<IChatLiveUserService, ChatLiveUserService>(x =>
                new ChatLiveUserService(botConnectionString));
            services.AddTransient<IEventService, EventService>(x => new EventService(botConnectionString));
            services.AddTransient<ILiveChatQueue, LiveChatQueue>(x => new LiveChatQueue(queueConnectionString));
            services.AddTransient<INoSqlDb<CoinApiData>, DapperNoSqlEncrypted<CoinApiData>>(x =>
                new DapperNoSqlEncrypted<CoinApiData>(coreConnectionString));
            services.AddTransient<ICoinApiStorageService, CoinApiStorageService>();
            services.AddTransient<INoSqlDb<ExchangeApiData>, DapperNoSqlEncrypted<ExchangeApiData>>(x =>
                new DapperNoSqlEncrypted<ExchangeApiData>(coreConnectionString));
            services.AddTransient<IExchangeApiStorageService, ExchangeApiStorageService>();
            services.AddTransient<IAdminStorageService, AdminStorageService>();
            services.AddTransient<IChannelStorageService, ChannelStorageService>();
            services.AddTransient<IConfigurationFunnel, ConfigurationFunnel>();
            services.AddTransient<IConfigurationCommon, ConfigurationCommon>();
            services.AddTransient<IConfigurationPubnub, ConfigurationPubnub>();
            services.AddTransient<IFbBotService, FbBotService>();
            services.AddTransient<IFacebookUserInterfaceService, FacebookUserInterfaceService>();
            services.AddTransient<IPubnubMessageBus, PubnubMessageBus>();
            services.AddTransient<ActionProcessor<MessageAction>, MessageActionProcessor>();
            services.AddTransient<ActionProcessor<MessageButtonAction>, MessageButtonActionProcessor>();
            services.AddTransient<ActionProcessor<MessageMenuButtonAction>, MessageMenuButtonProcessor>();
            services.AddTransient<ActionProcessor<MessageInputAction>, MessageInputActionProcessor>();
            services.AddTransient<ActionProcessor<EmailUpdateAction>, EmailUpdateProcessor>();
            services.AddTransient<ActionProcessor<ClientCreateAction>, ClientCreateProcessor>();
            services.AddTransient<ActionProcessor<ExchangeUpdateAction>, ExchangeUpdateActionProcesor>();
            services.AddTransient<ActionProcessor<MessageShowSubscriptionsAction>, MessageShowSubscriptionsProcessor>();
            services.AddTransient<ActionProcessor<SetUsernameCommandAction>, SetUsernameCommandProcessor>();
            services.AddTransient<ActionProcessorAsync<PaymentInfoCreateActionAsync>, PaymentInfoCreateProcessor>();
            services.AddTransient<ActionProcessorAsync<CgPaymentInfoCreateActionAsync>, CgPaymentInfoCreateProcessor>();
            services.AddTransient<ActionProcessor<StateUpdateAction>, StateUpdateProcessor>();
            services.AddTransient<ActionProcessor<StateUpdateActionWithData>, StateUpdateWithDataProcessor>();
            services.AddTransient<ActionProcessor<SubscriptionCreateAction>, SubscriptionCreateProcessor>();
            services.AddTransient<ActionProcessorAsync<ChannelAddMyselfActionAsync>, ChannelAddMyselfProcessor>();
            services
                .AddTransient<ActionProcessorAsync<CommandChannelAddClientActionAsync>, CommandChannelAddClientProcessor
                >();
            services.AddTransient<ActionProcessorAsync<ClientRequesTalkActionAsync>>(c =>
                new ClientRequesTalkProcessor(c.GetRequiredService<IChatLiveUserService>(),
                    new WidgetUserInterfaceService(c.GetRequiredService<IPubnubMessageBus>()),
                    c.GetRequiredService<IPubnubMessageBus>(),
                    c.GetRequiredService<ILiveChatQueue>()));
            services.AddTransient<ActionProcessorAsync<ChannelAddClientActionAsync>, ChannelAddClientProcessor>();
            services.AddTransient<ActionProcessorAsync<ImageActionAsync>, ImageActionProcessorAsync>();
            services.AddTransient<ActionProcessorAsync<FindChatIdActionAsync>, FindChatIdActionAsyncProcessor>();
            services
                .AddTransient<ActionProcessorAsync<IvoHttpNotificationActionAsync>,
                    IvoHttpNotificationActionAsyncProcessor>();
            services
                .AddTransient<ActionProcessorAsync<IvoHttpGetSubscriptionActionAsync>,
                    IvoHttpGetSubscriptionActionAsyncProcessor>();
            services
                .AddTransient<ActionProcessorAsync<CommandIvoGetSubscriptionAsync>,
                    CommandIvoGetSubscriptionAsyncProcessor>();
            services
                .AddTransient<ActionProcessorAsync<IvoSendAboutToExpireNotificationAsync>,
                    IvoSendAboutToExpireNotificationProcessor>();
            services
                .AddTransient<ActionProcessorAsync<CommandMessageActionAsync>, CommandMessageActionAsyncProcessor>();
            services.AddTransient<ActionProcessorAsync<TgClientMessageActionAsync>, TgClientMessageActionProcessor>();
            services
                .AddTransient<ActionProcessorAsync<TgClientSendImageActionAsync>, TgClientSendImageActionProcessor>();
            services
                .AddTransient<ActionProcessorAsync<CommandClientMessageActionAsync>, CommandClientMessageActionProcessor
                >();
            services
                .AddTransient<ActionProcessorAsync<FindChatIdFromMessageTextActionAsync>,
                    FindChatIdFromMessageTextProcessor>();
            services
                .AddTransient<ActionProcessorAsync<FindChatIdFromUsernameActionAsync>,
                    FindChatIdFromUsernameActionAsyncProcessor>();
            services
                .AddTransient<ActionProcessorAsync<FindChatIdCommandByUsernameActionAsync>,
                    FindChatIdCommandByUsernameActionAsyncProcessor>();
            services.AddTransient<ActionProcessorAsync<ChannelUnBanMyselfActionAsync>, ChannelUnBanMyselfProcessor>();
            services.AddTransient<ActionProcessorAsync<CommandChannelUnBanActionAsync>, CommandChannelUnBanProcessor>();
            services.AddTransient<ActionProcessorAsync<BiCreateLimitOrderAsync>, BiCreateLimitOrderProcessor>();
            services
                .AddTransient<ActionProcessorAsync<CleanUpChannelActionAsync>, CleanUpChannelActionAsyncProcessor>();
            services.AddTransient<ActionProcessorAsync<BiGetOpenOrdersAsync>, BiGetOpenOrdersProcessor>();
            services.AddTransient<ActionProcessorAsync<BiCreateOrderAsync>, BiCreateOrderProcessor>();
            services.AddTransient<ActionProcessorAsync<BiEnrichParametersAsync>, BiEnrichParametersProcessor>();
            services.AddTransient<ActionProcessorAsync<BiShowOpenOrdersAsync>, BiShowOpenOrdersProcessor>();
            services.AddTransient<ActionProcessorAsync<BiCancelOpenOrderAsync>, BiCancelOpenOrderProcessor>();
            services
                .AddTransient<ActionProcessorAsync<BiShowButtonsCancelOpenOrdersAsync>,
                    BiShowButtonsCancelOpenOrdersProcessor>();
            services.AddTransient<ActionProcessor<BiMessageResultAction>, BiMessageResultProcessor>();
            services.AddTransient<ActionProcessor<WidgetMessageAction>>(c =>
                new WidgetMessageActionProcessor(
                    new WidgetUserInterfaceService(c.GetRequiredService<IPubnubMessageBus>())));
            services.AddTransient<ActionProcessor<WidgetMessageButtonAction>>(c =>
                new WidgetMessageButtonActionProcessor(
                    new WidgetUserInterfaceService(c.GetRequiredService<IPubnubMessageBus>())));

            services.AddTransient<IPaypalApiStorageService, PaypalApiStorageService>();
            services.AddTransient<IFacebookBotStorageService, FacebookBotStorageService>();
            services.AddTransient<INoSqlDb<FacebookBotData>, DapperNoSqlEncrypted<FacebookBotData>>(x =>
                new DapperNoSqlEncrypted<FacebookBotData>(coreConnectionString));
            services.AddTransient<INoSqlDb<FacebookBotData>, DapperNoSqlEncrypted<FacebookBotData>>(x =>
                new DapperNoSqlEncrypted<FacebookBotData>(coreConnectionString));
            services.AddTransient<IIvoApi, IvoApiWrapper>();
            services.AddTransient<ActionProcessorAsync<PaypalPaymentCreateActionAsync>, PaypalPaymentCreateProcessor>();
            services.AddTransient<INoSqlDb<PaypalApiData>, DapperNoSqlEncrypted<PaypalApiData>>(x =>
                new DapperNoSqlEncrypted<PaypalApiData>(coreConnectionString));


            services.Configure<CommonConfiguration>(Configuration);
            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));
            services.Configure<PubnubConfiguration>(Configuration.GetSection("PubnubConfiguration"));
            services.Configure<FacebookBotConfiguration>(Configuration.GetSection("FBConfiguration"));
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var filePath = @"App_Data/logs/ini.txt";
                var file = new System.IO.FileInfo(filePath);
                file.Directory.Create(); // If the directory already exists, this method does nothing.
                File.WriteAllText(filePath, "Services. starting initialization");

                var services = scope.ServiceProvider;
                var service = services.GetRequiredService<IInitializacionService>();
                var funnelStorageService = services.GetRequiredService<IFunnelStorageService>();
                var funnelInitializationService = services.GetRequiredService<IFunnelInitializationService>();
                var botStorageService = services.GetRequiredService<IBotStorageService>();
                var botConfigStorageService = services.GetRequiredService<IBotConfigStorageService>();
                var tagsStorageService = services.GetRequiredService<ITagStorageService>();
                var coinApiStorageService = services.GetRequiredService<ICoinApiStorageService>();
                var exchangeApiStorageService = services.GetRequiredService<IExchangeApiStorageService>();
                var channelStorageService = services.GetRequiredService<IChannelStorageService>();
                var statesStorageService = services.GetRequiredService<IStateStorageService>();
                var mentorStorageService = services.GetRequiredService<IAdminStorageService>();
                var paypalApiStorageService = services.GetRequiredService<IPaypalApiStorageService>();
                var facebookBotStorageService = services.GetRequiredService<IFacebookBotStorageService>();
                try
                {
                    File.WriteAllText(@"App_Data/logs/ini.txt", "DB. starting Initialization");
                    service.DatabaseEnsureCreated();
                    service.InitializeStoreProcs();
                    mentorStorageService.Initialize();
                    funnelStorageService.Initialize();
                    funnelInitializationService.Initialize();
                    botStorageService.Initialize();
                    botConfigStorageService.Initialize();
                    tagsStorageService.Initialize();
                    coinApiStorageService.Initialize();
                    paypalApiStorageService.Initialize();
                    exchangeApiStorageService.Initialize();
                    statesStorageService.Initialize();
                    channelStorageService.Initialize();
                    facebookBotStorageService.Initialize();
                    File.WriteAllText(filePath, "DB initialized successfully DB.");
                }
                catch (Exception ex)
                {
                    File.WriteAllText(filePath, $"Error initializing DB {JsonConvertExt.SerializeObject(ex)}");
                }
            }
        }
    }
}
