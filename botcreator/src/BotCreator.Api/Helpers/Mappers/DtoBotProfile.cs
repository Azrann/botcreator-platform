using AutoMapper;
using BotCreator.Api.ViewModels.Bot;
using BotCreator.Core.DomainModels.Bot;
using BotCreator.Infrastructure.Data.EfCore;

namespace BotCreator.Api.Helpers.Mappers
{
    public class DtoBotProfile : Profile
    {
        public DtoBotProfile()
        {
            CreateMap<BotCreator.Infrastructure.Data.EfCore.Bot, BotModel>();
            CreateMap<BotTemplate, BotTemplateModel>();
            CreateMap<BotTemplateModel, BotTemplateDto>();
            CreateMap<CustomerServiceBotDto, CustomerServiceBotModel>();
            CreateMap<UserBotModel, UserBotDto>();

            CreateMap<UserBotModel, UserBotDto>();
            CreateMap<CustomerServiceBotDto, CustomerServiceBotModel>(MemberList.Source);

        }
    }
}
