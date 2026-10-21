using System.Collections.Generic;
using Bot.Configuration;
using Microsoft.Extensions.Options;
using MihaZupan;
using Telegram.Bot;

namespace Mentor.telegram.bot.api.Services
{
    public class BotService : IBotService
    {
        private readonly BotConfiguration _config;
        private readonly Dictionary<string, TelegramBotClient> _clients;

        public BotService(IOptions<BotConfiguration> config)
        {
            _config = config.Value;
            _clients = new Dictionary<string, TelegramBotClient>();
        }

        
        public TelegramBotClient GetClient(string botId)
        {
            if (_clients.ContainsKey(botId))
            {
                return _clients[botId];
            }

            var newClient = string.IsNullOrEmpty(_config.Socks5Host)
                ? new TelegramBotClient(botId)
                : new TelegramBotClient(
                   botId,
                    new HttpToSocks5Proxy(_config.Socks5Host, _config.Socks5Port));
            _clients.Add(botId, newClient);
            return newClient;
        }
    }
}
