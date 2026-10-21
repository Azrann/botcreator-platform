using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Mentor.telegram.coinpayments.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Mentor.telegram.bot.api.Controllers
{
    // USED by cron services
    [Route("api/[controller]")]
    public class CronController : Controller
    {
        private readonly IUpdateService _updateService;
        private readonly IEventService _eventService;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CronController));
        private const string Secret = "REPLACE_WITH_YOUR_VALUE";
       
        public CronController(IUpdateService updateService, IEventService eventService)
        {
            _updateService = updateService;
            _eventService = eventService;
        }

        [HttpPost("{botId}")]
        public async Task<IActionResult> Post([FromRoute] string botId)
        {
            Guid correlationId = Guid.NewGuid();
            try
            {
                Log.LogInf($"Event update received for id {botId}", correlationId);
                var hmac = this.Request.Headers["HMAC"];
                if (string.IsNullOrEmpty(hmac))
                {
                    return BadRequest("signature not found");
                }

                var parametersData = GetParametersData();

                bool valid = PaymentHelper.Validate(Secret, parametersData, hmac);

                if (!valid)
                {
                    var parametersJson = JsonConvert.SerializeObject(parametersData);
                    Log.LogErr($"Event invalid signature [{hmac}] for parameters {parametersJson}", correlationId);
                    return BadRequest("Invalid signature");
                }

                StringValues command;

                if (!Request.Form.TryGetValue("command", out command))
                {
                    Log.LogErr($"Event command not found", correlationId);
                    return BadRequest("command not found");
                }

                var eventInfo = GetEventInfo(parametersData);

                _eventService.AddEvent(GetEventItem(botId, eventInfo));
                await _updateService.ProcessEvent(botId,eventInfo, correlationId);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogErr($"Event update called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
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
        private EventInfo GetEventInfo(Dictionary<string, string> parametersData)
        {
            var result = new EventInfo()
            {
                Command = parametersData.GetString("Command"),
                SubscriptionInfo = new SubscriptionInfo()
                {
                    SubscriptionId = string.IsNullOrEmpty(parametersData.GetString("SubscriptionId")) ?Guid.Empty: new Guid(parametersData.GetString("SubscriptionId")),
                    BotId = parametersData.GetString("BotId"),
                    ChatId = parametersData.GetLong("ChatId"),
                    UserId = parametersData.GetString("UserId"),
                    Id = Guid.NewGuid(),
                    ItemName = parametersData.GetString("SubscriptionName")
                }
            };
            return result;
        }

        private Dictionary<string, string> GetParametersData()
        {
            Dictionary<string, string> parametersData = new Dictionary<string, string>();
            foreach (var formKey in Request.Form.Keys)
            {
                var myKey = formKey.Replace("\"", "");
                var myValue = Request.Form[formKey].ToString().Replace("\"", "");
                Log.LogInf($"GetParametersData {formKey} {Request.Form[formKey]}",Guid.NewGuid());
                parametersData.Add(myKey, myValue);
            }

            return parametersData;
        }
    }
}
