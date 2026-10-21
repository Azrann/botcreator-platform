using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BotCreator.Infrastructure.Data.EfCore;

namespace BotCreator.Infrastructure.Data
{
    public static class EfCoreConfiguration
    {
        public static void ConfigureEfCore(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BotManagerContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
