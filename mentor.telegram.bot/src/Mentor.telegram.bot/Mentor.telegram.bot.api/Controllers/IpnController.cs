using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mentor.telegram.bot.api.Controllers
{
    // USED by cron services
    [Route("api/[controller]")]
    public class IpnController : Controller
    {
        private readonly IUpdateService _updateService;
        private readonly ICoinApiStorageService _coinApiStorageService;
        private readonly IEventService _eventService;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(IpnController));

        public IpnController(IUpdateService updateService, ICoinApiStorageService coinApiStorageService,
            IEventService eventService)
        {
            _updateService = updateService;
            _coinApiStorageService = coinApiStorageService;
            _eventService = eventService;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] IpnUpdate update)
        {
            var botId = update.BotId;
            Guid correlationId = Guid.NewGuid();
            try
            {
                Log.LogInf($"IPN update received for id {botId} with data {JsonConvertExt.SerializeObject(update)}", correlationId);

                var eventInfo = GetEventInfo(update.Payload, botId);

                var data = _coinApiStorageService.GetCoinApiDataById(botId);

                if (data == null)
                {
                    Log.LogErr($"IPN security IPN does not exists for bot {botId}", correlationId);
                    return Unauthorized();
                }

                if (update.IpnSecret != data.IpnSecret)
                {
                    Log.LogErr($"IPN security IPN does not match", correlationId);
                    return Unauthorized();
                }

                _eventService.AddEvent(GetEventItem(botId, eventInfo));
                await _updateService.ProcessEvent(botId, eventInfo, correlationId);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogErr($"IPN update called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return Ok();
            }
        }

        private EventItem GetEventItem(string botId, EventInfo eventInfo)
        {
            return new EventItem()
            {
                BotId = botId,
                ChannelId = eventInfo.SubscriptionInfo?.ChatId.ToString(),
                Username = eventInfo.Username,
                Name = eventInfo.Command,
                DataJson = JsonConvert.SerializeObject(eventInfo),
                Id = Guid.NewGuid()
            };
        }

        private EventInfo GetEventInfo(Dictionary<string, string> parametersData, string botId)
        {
            var subscriptionId = parametersData.GetString("SubscriptionId");
            var result = new EventInfo()
            {
                Command = parametersData.GetString("Command"),
                Username = parametersData.GetString("Username").Replace("@",""),
                SubscriptionInfo = new SubscriptionInfo
                {
                    BotId = botId,
                    SubscriptionId = string.IsNullOrEmpty(subscriptionId) ? Guid.Empty : new Guid(subscriptionId),
                    ChatId = parametersData.GetLong("ChatId"),
                    UserId = parametersData.GetString("UserId"),
                    Id = Guid.NewGuid(),
                    ItemName = parametersData.GetString("SubscriptionName")
                },
                Channels = parametersData.GetListOfLongs("Channels").ToList(),
            };
            return result;
        }


    }

    public class IpnUpdate
    {
        public string BotId { get; set; }
        public string IpnSecret { get; set; }
        public Dictionary<string, string> Payload { get; set; }
    }
}
