using System;
using System.Collections.Generic;
using Core.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BotCreator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IConfigurationRoot _configuration;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(StatusController));

        public StatusController(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var environment = _configuration.GetValue<string>("Environment");
            Log.LogDeb($"BotApi {environment} OK", Guid.NewGuid());
            return Ok();
        }
    }
}
