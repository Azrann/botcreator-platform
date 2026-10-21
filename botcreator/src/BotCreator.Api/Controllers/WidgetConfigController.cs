using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using BotCreator.Api.Helpers;
using BotCreator.Api.Helpers.Extensions;
using BotCreator.Core.Common.Models;
using BotCreator.Core.ServiceInterfaces;
using BotCreator.Service.Bot;
using BotCreator.Api.ViewModels.Bot;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace BotCreator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WidgetConfigController : BaseBotController
    {
        private readonly IWidgetConfigStorageService _configStorageService;
        private readonly IBotService _botService;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(VersionController));
        private IMapper _mapper;
        private readonly IConfigurationRoot _configuration;

        public WidgetConfigController(IWidgetConfigStorageService configStorageService, IBotService botService,
            IMapper mapper, IConfigurationRoot configuration)
        {
            _configStorageService = configStorageService;
            _botService = botService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [Route("{botId}")]
        [HttpGet()]
        public IActionResult GetConfig([FromRoute] string botId)
        {
            Log.LogInf($"WidgetConfigController GetConfig called with botId={botId}.", Guid.NewGuid());

            var isDevEnv = _configuration.GetValue<string>("Environment").ToLower() != "prod";
            var config = _configStorageService.GetWidgetHtmlConfigById(botId, isDevEnv);

            return Ok(config ?? WidgetConfig.NewWidgetEs(botId, "black"));
        }

        [Authorize]
        [HttpPost("save")]
        public async Task<IActionResult> SaveConfig(WidgetConfigDataDto widgetConfigDataView)
        {
            Log.LogInf($"WidgetConfigController SaveConfig called with botId={widgetConfigDataView.BotId}.", Guid.NewGuid());
            var bot = await _botService.GetBotById(widgetConfigDataView.BotId);
            var widgetConfigData = new WidgetConfigData
            {
                BotId = bot.ExternalId,                
                BubbleMessage = widgetConfigDataView.BubbleMessage,
                ChatNow = widgetConfigDataView.ChatNow,
                Description = widgetConfigDataView.Description,
                PrimaryColor = widgetConfigDataView.PrimaryColor,
                SecondaryColor = widgetConfigDataView.SecondaryColor,
                Language = widgetConfigDataView.Language,
                Active = widgetConfigDataView.Active,
                BotName = bot.Name
            };

            if (await IsMyBot(widgetConfigData.BotId))
            {
                _configStorageService.InsertOrUpdate(widgetConfigData.BotId, widgetConfigData);
                return StatusCode(202);
            }

            return NotFound(widgetConfigData.BotId);
        }

        [Authorize]
        [HttpPost("setActive")]
        public async Task<IActionResult> SetActive(WidgetConfigActiveDto widgetConfigActiveDto)
        {
            Log.LogInf($"WidgetConfigController SetActive called with botId={widgetConfigActiveDto.BotId}.", Guid.NewGuid());
            var bot = await _botService.GetBotById(widgetConfigActiveDto.BotId);
            if (await IsMyBot(bot.ExternalId))
            {
                _configStorageService.SetBotActive(bot.ExternalId, widgetConfigActiveDto.Active);
                return StatusCode(200);
            }

            return NotFound(widgetConfigActiveDto.BotId);
        }

        [AllowAnonymous]
        [Route("data/{botId}")]
        [HttpGet()]
        public async Task<IActionResult> GetConfigData([FromRoute] int botId)
        {
            Log.LogInf($"WidgetConfigController GetConfigData called with botId={botId}.", Guid.NewGuid());

            var bot = await _botService.GetBotById(botId);

            var config = _configStorageService.GetWidgetConfigById(bot.ExternalId);

            return Ok(config);
        }

        private async Task<bool> IsMyBot(string botId)
        {
            var myBotIds = await _botService.GetBotsIdByUserId(LoggedInUser.Id);
            return myBotIds.Contains(botId);
        }
    }
}
