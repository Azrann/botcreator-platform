using AutoMapper;
using BotCreator.Core.DomainModels;
using BotCreator.Infrastructure.Data.EfCore;
using BotCreator.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotCreator.Infrastructure.Mapper.Profiles
{
    public class UserDbToModelMappingProfile : Profile
    {
        public UserDbToModelMappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
