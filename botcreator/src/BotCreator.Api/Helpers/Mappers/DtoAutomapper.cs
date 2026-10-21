using AutoMapper;
using BotCreator.Api.Helpers.Mappers;
using System.Collections.Generic;

namespace BotCreator.Api.Helpers
{
    public static class DtoAutomapper
    {
        public static List<Profile> GetDtoProfiles()
        {
            var profiles = new List<Profile>
            {
                new DtoBaseProfile(),
                new DtoBotProfile()
            };

            return profiles;
        }
    }
}
