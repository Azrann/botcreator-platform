using AutoMapper;
using BotCreator.Api.ViewModels;
using BotCreator.Core.DomainModels;

namespace BotCreator.Api.Helpers.Mappers
{
    public class DtoBaseProfile : Profile
    {
        public DtoBaseProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, UserDto>();
            CreateMap<UserWithoutPasswordDto, UserModel>();
            CreateMap<UserModel, UserWithoutPasswordDto>();
        }
    }
}
