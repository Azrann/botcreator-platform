using System;
using BotCreator.Api.Helpers;
using BotCreator.Api.Helpers.Infrastructure;
using BotCreator.Api.Helpers.Middleware;
using BotCreator.Api.Helpers.Middleware.Filters;
using BotCreator.Core.Common.Interfaces;
using BotCreator.Core.Common.Models;
using BotCreator.Core.Configs;
using BotCreator.Core.Service;
using BotCreator.Core.ServiceInterfaces;
using BotCreator.Infrastructure.Auth.Jwt;
using BotCreator.Infrastructure.Data;
using BotCreator.Infrastructure.Mapper;
using BotCreator.Service.Bot;
using BotCreator.Service.EmailProviders.Mailgun;
using BotCreator.Service.EmailProviders.SendGrid;
using BotCreator.Service.User;
using Core.Common.NoSqlDataAccess.DataAccess;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BotCreator.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostEnvironment env)
        {
            var dom = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .Build();

            Configuration = dom;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var coreConnectionString = Configuration.GetValue<string>("BotEngineDbConnection");
            var botConnectionString = Configuration.GetValue<string>("BotDbConnection");
            var authConnectionString = Configuration.GetValue<string>("AuthConnection");

            services.AddCors();
            services.ConfigureEfCore(authConnectionString);
            services.AddMvc(config =>
            {
                config.Filters.Add(new ResultFilter());
            }).AddNewtonsoftJson().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.ConfigureAutoMapper(DtoAutomapper.GetDtoProfiles());

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            services.ConfigureJwtAuth(appSettings);

            ConfigureBusinessServices(services, coreConnectionString, botConnectionString);
        }

        private void ConfigureBusinessServices(IServiceCollection services, string connectionString,
            string botConnectionString)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailProvider, MailgunEmailProvider>();
            services.AddTransient<IBotService, BotService>();
            services.AddTransient<IWidgetConfigStorageService, WidgetConfigStorageService>();
            services.AddTransient<INoSqlDb<WidgetConfigData>, DapperNoSql<WidgetConfigData>>(x => new DapperNoSql<WidgetConfigData>(connectionString));
            services.AddTransient<IEventService, EventService>(x => new EventService(botConnectionString));
            services.AddTransient<IEmailProvider, SendGridProvider>();
            services.AddTransient<IPathProvider, PathProvider>();
            services.AddTransient<IConfigurationRoot>(x => Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            loggerFactory.AddLog4Net();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            GlobalContext.Properties["feature"] = "BotCreatorApi";
            GlobalContext.Properties["Environment"] = Configuration["Environment"];
        }

        public static void InitializeServices(IServiceProvider hostServices)
        {
            var widgetConfig = hostServices.GetService<IWidgetConfigStorageService>();
            widgetConfig.Initialize();
        }
    }
}
