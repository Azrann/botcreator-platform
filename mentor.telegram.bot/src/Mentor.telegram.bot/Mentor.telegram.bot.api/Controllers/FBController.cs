using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Mentor.telegram.bot.api.Models;
using Mentor.telegram.bot.api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Mentor.telegram.bot.api.Controllers
{
    [Route("api/[controller]")]
    public class FbController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FbController));

        private const string PageUpdate = "page";

        private readonly IFbBotService _fbBotService;
        public FbController(IConfiguration configuration, IFbBotService fbBotService)
        {
            _fbBotService = fbBotService;
        }

        [HttpGet]
        public string Get([FromQuery(Name = "hub.mode")] string hub_mode,
            [FromQuery(Name = "hub.challenge")] string hub_challenge,
            [FromQuery(Name = "hub.verify_token")] string hub_verify_token)
        {
            return hub_challenge;
        }

        [HttpPost("{botId}")]
        public async Task<HttpResponseMessage> Post([FromHeader(Name = "X-Hub-Signature")]string signature, [FromRoute] string botId)
        {
            string body;
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                body = sr.ReadToEnd();
            }
            
            if (!_fbBotService.VerifySignature(signature, body, botId))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var value = JsonConvert.DeserializeObject<WebhookModel>(body);

            if (value._object != PageUpdate)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            foreach (var item in value.entry[0].messaging)
            {
                if (item.message != null || item.postback != null)
                {
                    await _fbBotService.SendMessage(item.message.text, item.sender.id,botId);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }     
    }

}