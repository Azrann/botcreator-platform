using System;
using Functions.Services;
using Functions.Token;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(Functions.Startup))]
namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var executionContextOptions = builder.Services.BuildServiceProvider().GetService<IOptions<ExecutionContextOptions>>().Value;
            var appDirectory = executionContextOptions.AppDirectory;

            var config = new ConfigurationBuilder()
                .SetBasePath(appDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var botConnectionString = config.GetValue<string>("BotDbConnection");
            var authConnectionString = config.GetValue<string>("AuthConnection");

            var issuerToken = config.GetValue<string>("IssuerToken");

            builder.Services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>(s => new AccessTokenProvider(issuerToken));
            builder.Services.AddTransient<IBotService, BotService>(s => new BotService(authConnectionString));
            builder.Services.AddTransient<IFeedbackService, FeedbackService>(s => new FeedbackService(botConnectionString));
            builder.Services.AddTransient<IDashboardService, DashboardService>(s => new DashboardService(botConnectionString));
            builder.Services.AddTransient<PubnubService, PubnubService>();
            builder.Services.AddTransient<IConfigurationRoot>(s => config);
        }
    }
}
