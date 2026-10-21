using BotCreator.Core.Configs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BotCreator.Core.Service;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace BotCreator.Infrastructure.Auth.Jwt
{
    public static class JwtConfiguration
    {
        public static void ConfigureJwtAuth(this IServiceCollection services, AppSettings settings)
        {
            var key = Encoding.ASCII.GetBytes(settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(ConfigureJwtBearer(key));

            services.AddTransient(typeof(ITokenHandler), typeof(TokenHandler));
        }

        static private Action<JwtBearerOptions> ConfigureJwtBearer(byte[] key)
        {
            return (options) =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = OnTokenValidatedCallback
                };
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            };
        }

        static private async Task OnTokenValidatedCallback(TokenValidatedContext context)
        {
            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            var userId = int.Parse(context.Principal.Identity.Name);
            var user = await userService.GetByIdAsync(userId);
            if (user == null)
            {
                // return unauthorized if user no longer exists
                context.Fail("Unauthorized");
            }
        }
    }
}
