using System.Collections.Generic;
using AutoMapper;
using Mentor.telegram.shared.BotTemplates;

namespace Mentor.telegram.bot.sdk.Dtos
{
    public class WidgetSupportBotDto : IBotDefinitionDto
    {
        public string ConnectingToAgentMessage { get; set; }
        public string PreMenuText { get; set; }

        public bool WhatsappChecked { get; set; }
        public string WhatsappNumber { get; set; }

        public bool ChatOnWeb { get; set; }

        public bool FbChecked { get; set; }
        public string FbUserName { get; set; }

        public bool CallMeChecked { get; set; }
        public string CallMeEmail { get; set; }
        public string CallMeButtonText { get; set; }
        public string TalkToAgent { get; set; }
        public Dictionary<string, string> MenuBot { get; set; }
    }

    public interface IBotDefinitionDto
    {
    }

    public static class BotDefinitionMapper
    {
        public static IBotDefinition Map(IBotDefinitionDto dto)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<WidgetSupportBotDto, WidgetSupportBot>();
            });

            if (dto.GetType() == typeof(WidgetSupportBotDto))
            {
                IMapper iMapper = config.CreateMapper();
                var result = iMapper.Map<WidgetSupportBotDto, WidgetSupportBot>((WidgetSupportBotDto)dto);
                return result;
            }

            return null;
        }
    }
}