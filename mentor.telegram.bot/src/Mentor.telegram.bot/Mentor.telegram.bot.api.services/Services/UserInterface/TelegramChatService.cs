using System;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.client;
using Bot.telegram.funnel;

namespace Mentor.telegram.bot.api.Services.UserInterface
{
    public class TelegramChatService : ITelegramChatService
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(TelegramChatService));

        public TelegramChatService()
        {
        }

        public async Task SendMessageByUsername(string username,string message, Guid correlationId)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);
            var user = await bot.FindUserByUsername(username);
            if (!(await bot.SendMessage(user.Id, user.AccessHash, message, correlationId)))
            {
                Log.LogErr($"TelegramChatService: Error sending message to username {username}", correlationId);

                client = clientFactory.GetTemporaryClient("SessionIVO2").Result;
                var bot2 = new TelegramBot(client);
                var user2 = await bot2.FindUserByUsername(username);

                if (!(await bot2.SendMessage(user2.Id, user2.AccessHash, message, correlationId)))
                {
                    Log.LogErr($"TelegramChatService: Error sending message with SessionIVO2 to username {username}", correlationId);
                }
            }
        }

        public async Task SendImageByUsername(string username, string imageName, Guid correlationId, string message = null)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);
            var user = await bot.FindUserByUsername(username);
            if (!(await bot.SendImage(user.Id, user.AccessHash, imageName, correlationId, message)))
            {
                Log.LogErr($"TelegramChatService: Error sending image to username {username}", correlationId);
            }
        }
    }
}
