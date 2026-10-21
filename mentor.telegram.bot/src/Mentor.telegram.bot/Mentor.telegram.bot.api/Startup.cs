using log4net;
using Mentor.telegram.bot.api.services;
using Mentor.telegram.bot.api.services.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace Mentor.telegram.bot.api
{
    public class Startup
    {
        private BootstrapServices _startupCore;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _startupCore = new BootstrapServices(Configuration);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            _startupCore.ConfigureCoreServices(services);

            // TODO only add swagger to Dev environment not to production.
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "AdminBot API", Version = "v1"}); });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
           app.UseSwagger();
           app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdminBot API V1");
                    c.RoutePrefix = string.Empty;
                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            app.UseHttpsRedirection();
            loggerFactory.AddLog4Net();

            app.UseCorsMiddleware();

            app.UseMvc();

            GlobalContext.Properties["feature"] = "TelegramApi";
            GlobalContext.Properties["Environment"] = Configuration["Environment"];

            ServiceProviderStatic.Instance = app.ApplicationServices;
        }
    }
}
