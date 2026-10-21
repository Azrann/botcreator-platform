using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BotCreator.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Directory.CreateDirectory("App_Data\\logs'"); 
            var host = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                        {
                            // Set properties and call methods on options
                        })
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                })
                .Build();
            Startup.InitializeServices(host.Services);
            host.Run();
        }
    }
}
