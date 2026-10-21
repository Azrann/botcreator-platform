using System;
using Core.Common.NoSqlDataAccess.DataAccess;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.api.Services.UserInterface;
using Mentor.telegram.bot.boundedContext;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Mentor.telegram.shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Mentor.telegram.bot.api.test
{
    public class DependencyInjectionTest
    {
        [Fact]
        public void WhenISetUpDIICanRetriveAServiceInstance()
        {
            IServiceCollection services = new ServiceCollection();
            var connectionString = @"REPLACE_WITH_YOUR_VALUE";

            services.AddDbContext<MentorDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IUpdateService, UpdateService>();
            services.AddTransient<IUserInterfaceService, TelegramUserInterfaceService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddTransient<IBlockchainService, BlockchainService>();
            services.AddTransient<IChannelService, TelegramChannelService>();
            services.AddTransient<ITelegramChatService, TelegramChatService>();
            services.AddTransient<IClientService, ClientService>(x => new ClientService(connectionString));
            services.AddTransient<IInitializacionService, InitializationService>();
            services.AddTransient<INoSqlDb<Funnel>, DapperNoSql<Funnel>>(x => new DapperNoSql<Funnel>(connectionString));
            services.AddTransient<IFunnelStorageService, FunnelStorageService>();
            services.AddTransient<INoSqlDb<UserTags>, DapperNoSql<UserTags>>(x => new DapperNoSql<UserTags>(connectionString));
            services.AddTransient<ITagStorageService, TagStorageService>();

            services.AddTransient<ActionProcessor<MessageAction>, MessageActionProcessor>();
            services.AddTransient<ActionProcessor<MessageButtonAction>, MessageButtonActionProcessor>();
            services.AddTransient<ActionProcessor<MessageInputAction>, MessageInputActionProcessor>();
            services.AddTransient<ActionProcessor<EmailUpdateAction>, EmailUpdateProcessor>();
            services.AddTransient<ActionProcessor<ClientCreateAction>, ClientCreateProcessor>();


            IServiceProvider collection = services.BuildServiceProvider();
            var clientProcessor = collection.GetService<ActionProcessor<ClientCreateAction>>();

            Assert.True(clientProcessor != null);
        }
    }
}
