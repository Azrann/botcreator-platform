using System;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.api.Models;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mentor.telegram.bot.api.Controllers
{
    [Route("api/[controller]")]
    public class WidgetController : Controller
    {
        private readonly IUpdateService _updateService;
        private readonly IEventService _eventService;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(WidgetController));

        public WidgetController(IUpdateService updateService, IEventService eventService)
        {
            _updateService = updateService;
            _eventService = eventService;
        }

        [HttpPost("{botId}")]
        public async Task<IActionResult> Post([FromRoute]string botId, [FromBody] Models.WidgetUpdate update)
        {
            Guid correlationId = Guid.NewGuid();
            try
            {
                Log.LogInf($"Update received for id {botId} with {JsonConvertExt.SerializeObject(update)}", correlationId);
                _eventService.AddEvent(GetEventItem(botId,update));

                await _updateService.ProcessWidgetAsync(botId, update, correlationId);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogErr($"Update called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return Ok();
            }
        }

        private EventItem GetEventItem(string botId, WidgetUpdate update)
        {
            var result = new EventItem()
            {
                BotId = botId,
                ClientId = update.WebUserId,
                ChannelId = update.ChatId,
                Username = $"{update.FirstName} {update.LastName}",
                Name = update.Type == "message" ? "WidgetMessage" : "WidgetCallback",
                DataJson = JsonConvert.SerializeObject(update),
                Id = Guid.NewGuid()
            };

            return result;
        }
    }
}
