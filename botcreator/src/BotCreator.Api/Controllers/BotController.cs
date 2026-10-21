using AutoMapper;
using BotCreator.Api.Helpers;
using BotCreator.Api.ViewModels.Bot;
using BotCreator.Api.ViewModels.Bot.CustomerService;
using BotCreator.Core.Configs;
using BotCreator.Core.DomainModels.Bot;
using BotCreator.Core.DomainModels.Bot.CustomerService;
using BotCreator.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.telegram.shared.BotTemplates;
using BotCreator.Service.Bot;
using Microsoft.Extensions.Configuration;

namespace BotCreator.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("bot")]
    public class BotController : BaseBotController
    {
        private IBotService _botService;
        private IConfigurationRoot _configuration;
        private IMapper _mapper;
        private readonly IWidgetConfigStorageService _configStorageService;

        public BotController(IBotService botService, IMapper mapper, IConfigurationRoot configuration,
            IWidgetConfigStorageService configStorageService)
        {
            _botService = botService;
            _mapper = mapper;
            _configuration = configuration;
            _configStorageService = configStorageService;
        }

        [HttpGet("templates")]
        public async Task<List<BotTemplateDto>> GetBotTemplates()
        {
            var templates = await _botService.GetTemplatesAsync();
            return templates.Select(s => _mapper.Map<BotTemplateModel, BotTemplateDto>(s)).ToList();
        }

        [HttpPost("service-bot/save")]
        public async Task<int> SaveCustomerServiceBot(CustomerServiceBotDto customerServiceBot)
        {
            //TODO remove this once language comes from UI
            if (string.IsNullOrEmpty(customerServiceBot.Language))
            {
                customerServiceBot.Language = "ES";
            }

            if (customerServiceBot.Id != 0)
            {
                await _botService.UpdateCustomerServiceBot(_mapper.Map<CustomerServiceBotDto, CustomerServiceBotModel>(customerServiceBot), LoggedInUser.Id);
                return customerServiceBot.Id;
            }

            return await _botService.CreateCustomerServiceBot(_mapper.Map<CustomerServiceBotDto, CustomerServiceBotModel>(customerServiceBot), LoggedInUser.Id);
        }

        [HttpPost("service-bot/savedeploy")]
        public async Task<int> SaveDeployCustomerServiceBot(CustomerServiceBotDto customerServiceBot)
        {
            //TODO remove this once language comes from UI
            if (string.IsNullOrEmpty(customerServiceBot.Language))
            {
                customerServiceBot.Language = "ES";
            }    

            if (customerServiceBot.Id != 0)
            { 
                await _botService.UpdateCustomerServiceBot(_mapper.Map<CustomerServiceBotDto, CustomerServiceBotModel>(customerServiceBot), LoggedInUser.Id);
                await _botService.DeployBot(customerServiceBot.Id, LoggedInUser.Id);
                return customerServiceBot.Id;
            }

            var botId = await _botService.CreateCustomerServiceBot(_mapper.Map<CustomerServiceBotDto, CustomerServiceBotModel>(customerServiceBot), LoggedInUser.Id);
            await _botService.DeployBot(botId, LoggedInUser.Id);
            return botId;
        }

        [HttpGet("definition/{botId}")]
        public async Task<IBotDefinition> GetBotDefinition(int botId)
        {
            var bot = await _botService.GetBotById(botId);
            return (IBotDefinition)_botService.GetBotDefinitionById(bot.ExternalId, LoggedInUser.Id);
        }

        [HttpGet("service-bot/widget-html/{botId}")]
        public async Task<BotWidgetEmbeddedHtmlDto> GetBotWidgetEmbeddedHtml(int botId)
        {
            var isDev = _configuration.GetValue<string>("Environment").ToLower() != "prod";

            var htmlContent = await _botService.GetBotEmbeddedHtml(botId, isDev);
            BotWidgetEmbeddedHtmlDto htmCode = new BotWidgetEmbeddedHtmlDto
            {
                HtmlContent = htmlContent
            };

            return htmCode;
        }

        [HttpGet("customer-service/{botId}")]
        public async Task<CustomerServiceBotDto> GetBotById(int botId)
        {
            var bot = await _botService.GetBotById(botId);
            var botDefinition = (WidgetSupportBot)_botService.GetBotDefinitionById(bot.ExternalId, LoggedInUser.Id);

            CustomerServiceBotDto result = BuildCustomerServiceBotDto(botDefinition, bot);

            return result;
        }


        [HttpPost("service-bot/inbound-channel")]
        public async Task<CsBotInboundChannelDto> GetInboundChannelForServiceBot()
        {
            var inboundChannel = await _botService.GetInboundChannelForServiceBot(LoggedInUser.Id);
            return _mapper.Map<CsBotInboundChannelModel, CsBotInboundChannelDto>(inboundChannel);
        }

        [HttpPost("service-bot/find-peer")]
        public async Task<CsWaitingUserConnectionDto> ConnectToWaitingUser(CsConnectToWaitingUserDto connectionDetails)
        {
            var cd = _mapper.Map<CsConnectToWaitingUserDto, CsConnectToWaitingUserModel>(connectionDetails);
            var connectedUser = await _botService.ConnectToWaitingUser(cd, LoggedInUser.Id);
            return _mapper.Map<CsWaitingUserConnectionModel, CsWaitingUserConnectionDto>(connectedUser);
        }

        [HttpGet("user-bots")]
        public async Task<List<UserBotDto>> GetUserBotsAsync()
        {
            var result = new List<UserBotDto>();
            var bots = await _botService.GetUserBotsAsync(LoggedInUser.Id);
            foreach (var bot in bots)
            {
                var dbBot = await _botService.GetBotById(bot.Id);
                var config = _configStorageService.GetWidgetConfigById(dbBot.ExternalId);
                
                var resBot = _mapper.Map<UserBotDto>(bot);
                resBot.IsActive = config.Active;
                resBot.ExternalId = dbBot.ExternalId;

                result.Add(resBot);
            }

            return result;
        }

        [HttpPost("service-bot/remove-bot/{botId}")]
        public async Task<bool> RemoveBot(int botId)
        {
            await _botService.RemoveBot(botId, LoggedInUser.Id);
            return true;
        }

        [HttpPost("service-bot/deploy-bot/{botId}")]
        public async Task<bool> DeployBot(int botId)
        {
            await _botService.DeployBot(botId, LoggedInUser.Id);
            return true;
        }

        private async Task<bool> IsMyBot(string botId)
        {
            var myBotIds = await _botService.GetBotsIdByUserId(LoggedInUser.Id);
            return myBotIds.Contains(botId);
        }

        private CustomerServiceBotDto BuildCustomerServiceBotDto(WidgetSupportBot botDefinition, BotModel bot)
        {
            var result = new CustomerServiceBotDto
            {
                Id = bot.Id,
                Name = bot.Name,
                ChatOnWeb = botDefinition.ChatOnWeb,
                FbChecked = botDefinition.FbChecked,
                FbUserName = botDefinition.FbUserName,
//              TalkToMeChecked = botDefinition.TalkToMeChecked,
                TalkToAgent = botDefinition.TalkToAgent,
                WhatsappChecked = botDefinition.WhatsappChecked,
                WhatsappNumber = botDefinition.WhatsappNumber,
                CallMeChecked = botDefinition.CallMeChecked,
                CallMeEmail = botDefinition.CallMeEmail
            };

            var menu = botDefinition.MenuBot.ToArray();
            if (menu.Any())
            {
                result.ButtonText1 = menu[0].Key;
                result.ResultText1 = menu[0].Value;
            }

            if (menu.Count() >= 2)
            {
                result.ButtonText2 = menu[1].Key;
                result.ResultText2 = menu[1].Value;
            }

            if (menu.Count() >= 3)
            {
                result.ButtonText3 = menu[2].Key;
                result.ResultText3 = menu[2].Value;
            }

            return result;
        }
    }
}
