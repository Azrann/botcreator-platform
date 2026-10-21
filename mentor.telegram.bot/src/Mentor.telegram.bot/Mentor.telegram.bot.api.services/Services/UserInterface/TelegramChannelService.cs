using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.client;
using OpenTl.Schema;

namespace Mentor.telegram.bot.api.Services.UserInterface
{
    public class TelegramChannelService : IChannelService
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(TelegramChannelService));

        public TelegramChannelService()
        {
        }

        public async Task AddUserToChannel(int userId, string channelId, long channelHash, Guid correlationId)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);
            var result = await bot.FindUser(userId);
            TUser user = (TUser)result.User;

            var chatId = int.Parse(channelId);
            if (!(await bot.AddUserToChannel(userId, chatId, user.AccessHash, channelHash, correlationId)))
            {
                Log.LogErr($"TelegramChannelService: Error adding user {userId} to channel {channelId}", correlationId);
            }
        }

        public async Task AddUserToChat(int userId, string channelId, Guid correlationId)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);
            var result = await bot.FindUser(userId);
            TUser user = (TUser)result.User;

            var chatId = int.Parse(channelId);
            if (!(await bot.AddUserToChat(userId, chatId, user.AccessHash, correlationId)))
            {
                Log.LogErr($"TelegramChannelService: Error adding user {userId} to chat {chatId}", correlationId);
            }
        }

        public async Task AddUserToChannelByUsername(string username, string channelId, long channelHash,
            Guid correlationId)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);
            var user = await bot.FindUserByUsername(username);

            var chatId = int.Parse(channelId);
            if (!(await bot.AddUserToChannel(user.Id, chatId, user.AccessHash, channelHash, correlationId)))
            {
                Log.LogErr($"TelegramChannelService: Error adding user {username} to channel {channelId}",
                    correlationId);
            }
        }

        public async Task AddUserToChatByUsername(string username, string channelId, Guid correlationId)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);
            var user = await bot.FindUserByUsername(username);

            var chatId = int.Parse(channelId);
            if (!(await bot.AddUserToChat(user.Id, chatId, user.AccessHash, correlationId)))
            {
                Log.LogErr($"TelegramChannelService: Error adding user {username} to chat {chatId}", correlationId);
            }
        }

        public async Task RemoveUserFromChat(int userId, string channelId, Guid correlationId)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);
            var result = await bot.FindUser(userId);
            TUser user = (TUser)result.User;

            var chatId = int.Parse(channelId);
            if (!(await bot.RemoveUserFromChat(userId, chatId, user.AccessHash, correlationId)))
            {
                Log.LogErr($"TelegramChannelService: Error removing user {userId} from chat {chatId}", correlationId);
            }
        }

        public async Task<Tuple<int, long>> GetUserByUserName(string username, Guid correlationId, int retryCount = 0)
        {
            ClientFactory clientFactory = new ClientFactory();
            var forceNew = retryCount > 0;
            var client = await clientFactory.GetClient(forceNew:forceNew);
            var bot = new TelegramBot(client);
            try
            {
                var result = await bot.FindUserByUsername(username);
                return Tuple.Create(result.Id,result.AccessHash);
            }
            catch (Exception e)
            {
                Log.LogErr($"TelegramChannelService: GetUserByUserName Error {e.Message} for {username} retryCount {retryCount}", correlationId);
                if (retryCount > 0)
                {
                    return null;
                }

                return await GetUserByUserName(username, correlationId, retryCount+1);
            }
        }

        public async Task<string> GetUserNameByUserId(int userId, Guid correlationId)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);
            var result = await bot.FindUser(userId);
            TUser user = (TUser)result.User;
            return user.Username;
        }

        public async Task<List<GenericUser>> GetRecentUsersByChannel(int numUsers, int channelId, long channelHash,
            Guid correlationId)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);
            var channelParticipants = await bot.GetRecentUsers(numUsers, channelId, channelHash, correlationId,0);

            var result = channelParticipants.Users.OfType<TUser>().Select(x => new GenericUser
            {
                Hash = x.AccessHash,
                Id = x.Id,
                Username = x.Username,
                Firstname = x.FirstName,
                Lastname = x.LastName
            });

            return result.ToList();
        }

        public async Task<List<GenericUser>> GetParticipantsByChannel(int channelId, long channelHash,
            Guid correlationId, string session = "session")
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient(session);
            var bot = new TelegramBot(client);

            var channelParticipants = await bot.GetAllParticipants(channelId, channelHash, correlationId);

            var result = channelParticipants.Select(x => new GenericUser
            {
                Hash = x.AccessHash,
                Id = x.Id,
                Username = x.Username,
                Firstname = x.FirstName,
                Lastname = x.LastName
            });

            return result.ToList();
        }

        public async Task BanUserFromChannel(int userId, int channelId, long accessHash, long accessHashChannel
            , Guid correlationId)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);

            if (!(await bot.BanUserFromChannel(userId, channelId, accessHash, accessHashChannel, correlationId)))
            {
                Log.LogErr($"TelegramChannelService: Error BanUserFromChannel user {userId} from chat {channelId}", correlationId);
            }
        }

        public async Task UnBanUserFromChannel(int userId, int channelId, long accessHash, long accessHashChannel
            , Guid correlationId)
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);

            if (!(await bot.UnBanUserFromChannel(userId, channelId, accessHash, accessHashChannel, correlationId)))
            {
                Log.LogErr($"TelegramChannelService: Error UnBanUserFromChannel user {userId} from chat {channelId}", correlationId);
            }
        }
    }
}
