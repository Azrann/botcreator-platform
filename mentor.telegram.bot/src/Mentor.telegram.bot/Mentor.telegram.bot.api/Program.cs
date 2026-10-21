using System.IO;
using Mentor.telegram.bot.api.services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Mentor.telegram.bot.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Directory.CreateDirectory("App_Data\\logs");
            var host = CreateWebHostBuilder(args).Build();
            BootstrapServices.Initialize(host.Services);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
