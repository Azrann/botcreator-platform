using System;
using Core.Common.NoSqlDataAccess.DataAccess;
using Mentor.bot.boundedContext.Services;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.boundedContext;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models.Facebook;
using Bot.Configuration;
using Bot.telegram.funnel;
using Core.Common;
using Mentor.telegram.shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mentor.telegram.bot.api.services
{
    public class BootstrapServicesSdk
    {
        public BootstrapServicesSdk(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureCoreServices(IServiceCollection services) 
        {
            // services.AddCors();

            var coreConnectionString = Configuration.GetValue<string>("BotEngineDbConnection");
            var botConnectionString = Configuration.GetValue<string>("BotDbConnection");
            var queueConnectionString = Configuration.GetValue<string>("QueueConnection");

            services.AddDbContext<MentorDbContext>(options => options.UseSqlServer(coreConnectionString));
            services.AddDbContext<BotDbContext>(options => options.UseSqlServer(botConnectionString));

            services.AddSingleton<IBotService, BotService>();
            services.AddTransient<INoSqlDb<Funnel>, DapperNoSql<Funnel>>(x =>
                new DapperNoSql<Funnel>(coreConnectionString));
            services.AddTransient<INoSqlDb<Bot.telegram.funnel.Bot>, DapperNoSql<Bot.telegram.funnel.Bot>>(x => new DapperNoSql<Bot.telegram.funnel.Bot>(coreConnectionString));
            services.AddTransient<INoSqlDb<BotDefinition>, DapperNoSql<BotDefinition>>(x =>
                new DapperNoSql<BotDefinition>(coreConnectionString));
            services.AddTransient<INoSqlDb<AdminData>, DapperNoSql<AdminData>>(x =>
                new DapperNoSql<AdminData>(coreConnectionString));
            services.AddTransient<IFunnelStorageService, FunnelStorageService>();
            services.AddTransient<IBotStorageService, BotStorageService>();
            services.AddTransient<IBotDefinitionStorageService, BotDefinitionStorageService>();
            services.AddTransient<IChatLiveUserService, ChatLiveUserService>(x =>
                new ChatLiveUserService(botConnectionString));
            services.AddTransient<ILiveChatQueue, LiveChatQueue>(x => new LiveChatQueue(queueConnectionString, Configuration.GetValue<string>("Environment")));
            services.AddTransient<IConfigurationFunnel, ConfigurationFunnel>();
            services.AddTransient<IConfigurationCommon, ConfigurationCommon>();
            services.AddTransient<IConfigurationPubnub, ConfigurationPubnub>();
            services.AddTransient<IPubnubMessageBus, PubnubMessageBus>();
            services.AddTransient<IFacebookBotStorageService, FacebookBotStorageService>();
            services.AddTransient<INoSqlDb<FacebookBotData>, DapperNoSqlEncrypted<FacebookBotData>>(x =>
                new DapperNoSqlEncrypted<FacebookBotData>(coreConnectionString));
            services.AddTransient<INoSqlDb<FacebookBotData>, DapperNoSqlEncrypted<FacebookBotData>>(x =>
                new DapperNoSqlEncrypted<FacebookBotData>(coreConnectionString));

            services.Configure<CommonConfiguration>(setting =>
            {
                Configuration.Bind(setting);
            });

            services.Configure<BotConfiguration>(setting =>
            {
                Configuration.GetSection("BotConfiguration").Bind(setting);
            });

            services.Configure<PubnubConfiguration>(setting =>
            {
                Configuration.GetSection("PubnubConfiguration").Bind(setting);
            });

            services.Configure<FacebookBotConfiguration>(setting =>
            {
                Configuration.GetSection("FBConfiguration").Bind(setting);
            });

            services.AddTransient<BotCreator, BotCreator>();
        }
      
        public static void Initialize(IServiceProvider serviceProvider)
        {
        }
    }
}
