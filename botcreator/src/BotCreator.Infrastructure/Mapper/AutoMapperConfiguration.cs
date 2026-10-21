using AutoMapper;
using BotCreator.Infrastructure.Mapper.Profiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotCreator.Infrastructure.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void ConfigureAutoMapper(this IServiceCollection services, List<Profile> additionalProfiles = null)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserDbToModelMappingProfile());

                AddAdditionalProfiles(additionalProfiles, mc);
                
            });


            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void AddAdditionalProfiles(List<Profile> additionalProfiles, IMapperConfigurationExpression mc)
        {
            if (additionalProfiles == null || additionalProfiles.Count == 0)
                return;

            foreach (var profile in additionalProfiles)
            {
                mc.AddProfile(profile);
            }
        }
    }
}
