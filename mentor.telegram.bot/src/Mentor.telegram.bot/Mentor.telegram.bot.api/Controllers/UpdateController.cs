using System;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot.Types.Enums;
using Update = Telegram.Bot.Types.Update;

namespace Mentor.telegram.bot.api.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly IUpdateService _updateService;
        private readonly IEventService _eventService;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(UpdateController));

        public UpdateController(IUpdateService updateService,IEventService eventService)
        {
            _updateService = updateService;
            _eventService = eventService;
        }

        [HttpPost("{botId}")]
        public async Task<IActionResult> Post([FromRoute]string botId, [FromBody]Update update)
        {
            Guid correlationId = Guid.NewGuid();
            try
            {
                Log.LogInf($"Update received for id {botId} with {JsonConvertExt.SerializeObject(update)}", correlationId);
                _eventService.AddEvent(GetEventItem(botId, update));

                await _updateService.ProcessAsync(botId,update, correlationId);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogErr($"Update called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return Ok();
            }
        }

        private EventItem GetEventItem(string botId, Update update)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                return new EventItem()
                {
                    BotId = botId,
                    ChannelId = update.CallbackQuery?.From?.Id.ToString(),
                    Username = update.CallbackQuery?.From?.Username,
                    Name = "Callback",
                    DataJson = JsonConvert.SerializeObject(update),
                    Id = Guid.NewGuid()
                };
            }

            if (update.Type == UpdateType.Message)
            {
                return new EventItem()
                {
                    BotId = botId,
                    ChannelId = update.Message?.From?.Id.ToString(),
                    Username = update.Message?.From?.Username,
                    Name = "Message",
                    DataJson = JsonConvert.SerializeObject(update),
                    Id = Guid.NewGuid()
                };
            }

            return new EventItem()
            {
                BotId = botId,
                ChannelId = update.Message?.From?.Id.ToString(),
                Username = update.Message?.From?.Username,
                Name = "Unknown",
                DataJson = JsonConvert.SerializeObject(update),
                Id = Guid.NewGuid()
            };
        }
    }
}
