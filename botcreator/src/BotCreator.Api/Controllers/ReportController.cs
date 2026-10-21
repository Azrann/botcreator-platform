using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using BotCreator.Api.Helpers;
using BotCreator.Api.Helpers.Extensions;
using BotCreator.Core.ServiceInterfaces;
using BotCreator.Service.Bot;
using BotCreator.Core.Common.Exceptions;

namespace BotCreator.Api.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportController : BaseBotController
    {
        private readonly IEventService _eventService;
        private readonly IBotService _botService;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(VersionController));

        public ReportController(IEventService eventService, IBotService botService)
        {
            _eventService = eventService;
            _botService = botService;
        }

        [Authorize]
        [HttpGet("{botId}")]
        public async Task<IActionResult> GetData([FromRoute]int botId)
        {
            Log.LogInf($"ReportController GetData called with botId={botId}.", Guid.NewGuid());
            var bot = await _botService.GetBotById(botId);
            if (bot == null)
            {
                throw new BotBaseException($"Invalid botId");
            }

            if (await IsMyBot(bot.ExternalId))
            {
                return Ok(_eventService.GetEventsByBotId(bot.ExternalId));
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var botId = "5ae3941c-51d2-4489-a7ce-c3339ee221ad";
            Log.LogInf($"ReportController GetData called with botId={botId}.", Guid.NewGuid());

            return Ok(_eventService.GetEventsByBotId(botId));
        }

        private async Task<bool> IsMyBot(string botId)
        {
            var myBotIds = await _botService.GetBotsIdByUserId(LoggedInUser.Id);
            return myBotIds.Contains(botId);
        }
    }
}
