using Hangfire;
using Hangfire.RecurringJobExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mentor.telegram.cron
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("VctDatabase");
             services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(connectionString);
                x.UseRecurringJob(typeof(VctRecurringJobService));
                x.UseRecurringJob(typeof(RecurringJobService));
                x.UseDefaultActivator();
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                Queues = new[] { "default", "apis", "jobs" }
            });

            app.UseMvc();
            app.UseHangfireDashboard("");
        }
    }
}
