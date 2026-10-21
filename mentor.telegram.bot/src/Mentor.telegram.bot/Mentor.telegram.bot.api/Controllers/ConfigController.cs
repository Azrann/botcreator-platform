using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Bot.Configuration;
using Mentor.telegram.bot.boundedContext.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.telegram.bot.api.Controllers
{
    [Route("api/[controller]")]
    public class ConfigController : Controller
    {
        private readonly IConfigurationPubnub _pubnubConfig;
        private static Dictionary<string, string> _db;

        public ConfigController(IConfigurationPubnub pubnubConfig)
        {
            _pubnubConfig = pubnubConfig;
            if (_db == null)
            { 
                _db = new Dictionary<string, string>();
            }
        }

        [HttpGet("pubnubValues")]
        public IActionResult Get()
        {
            return Ok($"PubKey:{_pubnubConfig.GetPublisherKey()}\nPubKey:{_pubnubConfig.GetSubscriberKey()}\nSecKey:{_pubnubConfig.GetSecretKey()}");
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> Post([FromRoute]string Id)
        {
            var config = await Request.GetRawBodyStringAsync();
            if (_db.ContainsKey(Id))
            {
                _db[Id] = config;
            }
            else
            {
                _db.Add(Id,config);
            }

            return Ok();
        }

        [HttpGet("{Id}")]
        public IActionResult Get([FromRoute]string Id)
        {
            if (_db.ContainsKey(Id))
            {
                return Ok(_db[Id]);
            }
           
            return Ok(GetDefault());
        }

        private static string GetDefault()
        {
            return @"{
                bot:
            {
                name: 'Bot Agent',
                url: 'insterurlhere',
                avarat_url: 'avatarimagestring',
                id: 'REPLACE_WITH_YOUR_VALUE'
            },
            user:
            {
                avarat_url: 'https://example/images/avatar.jpg'
            },
            head_title: 'Chat with us!',
            first_message_text: '<b>Let me help you :)</b>',
            description: 'I am the Bot agent, I can assist with frequently asked questions & and issues installing the widget.',
            icons: { },
            buttons:
            {
                chart_now_btn: 'Chat now',
                register_btn: 'Start'
            },
            form:
            {
                name:
                {
                    title: 'Your name:',
                    placeholder: 'Type your name...'
                },
                email:
                {
                    title: 'E-mail:',
                    placeholder: 'Type your email...'
                }
            },
            styles:
            {
                widget:
                {
                    fontFamily: 'Open Sans',
                    width: '350px',
                    right: '1em',
                    bottom: '1em'
                },
                modal_head:
                {
                    backgroundColor: '#ffffff',
                },
                modal_body:
                {
                    backgroundColor: '#E9EEF4',
                },
                btn_images: { },
                buttons:
                {
                    fontFamily: 'Open Sans',
                    backgroundColor: '#2ecc71',
                    color: '#ffffff',
                    minHeight: '45px'
                },
                message:
                {
                    backgroundColor: '#ffffff',
                    boxShadow: '0 1px 15px rgba(0,0,0,0.5)'
                }
            }
            }";
        }
    }

    public static class HttpRequestExtensions
    {

        /// <summary>
        /// Retrieve the raw body as a string from the Request.Body stream
        /// </summary>
        /// <param name="request">Request instance to apply to</param>
        /// <param name="encoding">Optional - Encoding, defaults to UTF8</param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            using (StreamReader reader = new StreamReader(request.Body, encoding))
                return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Retrieves the raw body as a byte array from the Request.Body stream
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetRawBodyBytesAsync(this HttpRequest request)
        {
            using (var ms = new MemoryStream(2048))
            {
                await request.Body.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}

