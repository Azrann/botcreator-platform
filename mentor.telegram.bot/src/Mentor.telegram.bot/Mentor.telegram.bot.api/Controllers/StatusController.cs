using System;
using System.Threading.Tasks;
using Bot.Configuration;
using Core.Common.Extensions;
using Mentor.telegram.bot.boundedContext.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.telegram.bot.api.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private readonly IConfigurationCommon _configurationCommon;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(WidgetController));

        public StatusController(IConfigurationCommon configurationCommon)
        {
            _configurationCommon = configurationCommon;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var environment = _configurationCommon.Environment;
            Log.LogDeb($"BotEngine {environment} OK",Guid.NewGuid());
            return Ok();
        }
    }
}
