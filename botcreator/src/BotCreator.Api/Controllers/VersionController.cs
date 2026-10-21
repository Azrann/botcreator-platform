using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using BotCreator.Api.Helpers.Extensions;

namespace BotCreator.Api.Controllers
{
    [ApiController]
    [Route("version")]
    public class VersionController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(VersionController));

        [AllowAnonymous]
        [HttpGet()]
        public IActionResult Get()
        {
            Log.LogInf("Version called.", Guid.NewGuid());
            return Ok("1.0");
        }
    }
}
