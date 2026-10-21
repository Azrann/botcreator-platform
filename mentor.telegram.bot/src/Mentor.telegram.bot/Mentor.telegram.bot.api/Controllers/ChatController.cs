using System;
using System.Threading.Tasks;
using Bot.Configuration;
using Core.Common.Extensions;
using Mentor.bot.boundedContext.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.telegram.bot.api.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IPubnubMessageBus _pubnubMessageBus;
        private readonly IChatLiveUserService _chatLiveUserService;
        private readonly ILiveChatQueue _liveChatQueue;
        private readonly IConfigurationFunnel _configurationFunnel;
        private readonly IConfigurationCommon _configurationCommon;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(WidgetController));
        private const string WebhookChannel = "bc-message";

        public ChatController(IPubnubMessageBus pubnubMessageBus, IChatLiveUserService chatLiveUserService, 
            ILiveChatQueue liveChatQueue, IConfigurationFunnel configurationFunnel, IConfigurationCommon configurationCommon)
        {
            _pubnubMessageBus = pubnubMessageBus;
            _chatLiveUserService = chatLiveUserService;
            _liveChatQueue = liveChatQueue;
            _configurationFunnel = configurationFunnel;
            _configurationCommon = configurationCommon;
        }

        [Route("Configuration")]
        [HttpGet]
        public IActionResult Configuration()
        {
            string result = _configurationFunnel.GetServerUrl + "\n\n" + _configurationCommon.Environment + "\n\n" +
                            _configurationCommon.IsAzProduction() +
                            "\n\n" + _configurationCommon.IsDevelopment() + "\n\n" +
                            _configurationCommon.IsIvoProduction();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] InitChat initChat)
        {
            if (initChat == null)
            {
                return BadRequest();
            }

            Guid correlationId = Guid.NewGuid();
            string authKey = Guid.NewGuid().ToString().Replace("-", "");
            var channel = GetRandomChannelId();
            string
                webhookChannelId =
                    WebhookChannel +
                    "1"; // Added "1" that could be a random between 1 and 5 and have 5 functions to process more load.

            try
            {
                Log.LogInf($"Post channel received {JsonConvertExt.SerializeObject(initChat)}", correlationId);

                _pubnubMessageBus.Start(correlationId);
                _pubnubMessageBus.Grantd(authKey, channel, true, false);
                _pubnubMessageBus.Grantd(authKey, webhookChannelId, false, true);
                _pubnubMessageBus.Grantd(authKey + 1, channel, true, true); // For testing.

                var userId = Guid.NewGuid();
                _chatLiveUserService.AddChatLiveUser(new ChatLiveUser()
                {
                    ChannelId = channel,
                    Auth = authKey,
                    BotId = initChat.BotId,
                    Id = userId,
                    Email = initChat.Email,
                    Name = initChat.Name,
                    ClientId = initChat.ClientId ?? Guid.Empty
                });

                return Ok(new ChannelResponse
                {
                    Token = authKey,
                    ChannelId = channel,
                    PublishChannel = webhookChannelId,
                    UserId = userId.ToString()
                });
            }
            catch (Exception ex)
            {
                Log.LogErr($"Post channel called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return BadRequest();
            }
        }

        [Route("AgentAuth/{botId}")]
        [HttpGet]
        public IActionResult AgentAuth([FromRoute] string botId)
        {
            Guid correlationId = Guid.NewGuid();
            string authKey = Guid.NewGuid().ToString().Replace("-", "");
            var channel = botId;

            try
            {
                Log.LogInf($"AgentAuth channel received for botId={botId}", correlationId);

                _pubnubMessageBus.Start(correlationId);
                _pubnubMessageBus.Grantd(authKey, channel, true, false);
               

                return Ok(new
                {
                    data = authKey
                });
            }
            catch (Exception ex)
            {
                Log.LogErr($"AgentAuth called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return BadRequest();
            }
        }

        [Route("TalkToAgent")]
        [HttpPost]
        public async Task<IActionResult> TalkToAgent([FromBody] ClientUser user)
        {
            Guid correlationId = Guid.NewGuid();
            var channel = GetRandomChannelId(); ;

            try
            {
                Log.LogInf($"TalkToAgent channel received", correlationId);

                _pubnubMessageBus.Start(correlationId);
                _pubnubMessageBus.Grantd(user.Token, channel, true, true);

                var userInfo = _chatLiveUserService.GetChatLiveUserByToken(user.BotId, user.Token);

                await _liveChatQueue.AddClientToQueue(new QueueItem()
                {
                    BotId = user.BotId,
                    ChannelId = channel,
                    AuthKey = user.Token
                });

                _pubnubMessageBus.Publish(new
                    {
                        action = "join",
                        user = new {
                            userId = userInfo.Id,
                            userName = userInfo.Name,
                            AvatarUrl = ""
                    }},
                    user.BotId
                );
                return Ok(channel);
            }
            catch (Exception ex)
            {
                Log.LogErr($"TalkToAgent called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return BadRequest();
            }
        }

        private static string GetRandomChannelId()
        {
            Random rnd = new Random();
            string channel = rnd.Next(1, 99999999).ToString();
            return channel;
        }

        [HttpPost("Connect")]
        public async Task<IActionResult> ConnectToChatClient([FromBody] Agent customerServiceAgent)
        {
            Guid correlationId = Guid.NewGuid();

            try
            {
                Log.LogInf($"ConnectToChatClient user received request {JsonConvertExt.SerializeObject(customerServiceAgent)}", correlationId);

                var liveChatUser = await _liveChatQueue.GetClientFromQueue(customerServiceAgent.BotId);
                Log.LogInf($"ConnectToChatClient GetClientFromQueue object {JsonConvertExt.SerializeObject(liveChatUser)}", correlationId);

                if (liveChatUser == null)
                {
                    return NotFound();
                }

                _pubnubMessageBus.Start(correlationId);
                _pubnubMessageBus.Grantd(customerServiceAgent.AuthKey, liveChatUser.ChannelId, true, true);

                var userInfo = _chatLiveUserService.GetChatLiveUserByToken(liveChatUser.BotId, liveChatUser.AuthKey);

                _pubnubMessageBus.Publish(new
                {
                    action = "leave",
                    user = new
                    {
                        userId = userInfo.Id,
                    }
                }, customerServiceAgent.BotId);

                return Ok(
                    new
                    {
                        data = new
                        {
                            channel = liveChatUser.ChannelId,
                            name = userInfo.Name,
                            userId = userInfo.Id
                        }
                    }
                );
            }
            catch (Exception ex)
            {
                Log.LogErr($"ConnectToChatClient channel called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return BadRequest();
            }
        }
    }

    public class ChannelResponse
    {
        public string Token { get; set; }
        public string ChannelId { get; set; }
        public string PublishChannel { get; set; }
        public string UserId { get; set; }
    }

    public class InitChat
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string BotId { get; set; }
        public Guid? ClientId { get; set; }
    }

    public class ClientUser
    {
        public string Token { get; set; }
        public string BotId { get; set; }
    }
}

