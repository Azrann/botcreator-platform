using Microsoft.AspNetCore.Mvc;

namespace Mentor.telegram.cron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        // GET: api/Ping
        [HttpGet]
        public string Get()
        {
            return "ok";
        }
    }
}
