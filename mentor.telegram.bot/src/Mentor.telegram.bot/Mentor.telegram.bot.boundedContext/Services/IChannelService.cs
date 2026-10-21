using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public interface IChannelService    
    {
        Task AddUserToChannel(int userId, string channelId, long channelHash,Guid correlationId);
        Task AddUserToChat(int userId, string channelId, Guid correlationId);
        Task AddUserToChannelByUsername(string username, string channelId, long channelHash, Guid correlationId);
        Task AddUserToChatByUsername(string username, string channelId, Guid correlationId);
        Task RemoveUserFromChat(int userId, string channelId, Guid correlationId);
        Task<Tuple<int, long>> GetUserByUserName(string username, Guid correlationId, int retryCount = 0);
        Task<string> GetUserNameByUserId(int userId, Guid correlationId);
        Task<List<GenericUser>> GetRecentUsersByChannel(int numUsers, int channelId, long channelHash,
            Guid correlationId);
        Task<List<GenericUser>> GetParticipantsByChannel(int channelId, long channelHash, Guid correlationId, string session = "session");
        Task BanUserFromChannel(int userId, int channelId, long accessHash, long accessHashChannel
            , Guid correlationId);
        Task UnBanUserFromChannel(int userId, int channelId, long accessHash, long accessHashChannel
            , Guid correlationId);
    }

    public class GenericUser
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Id { get; set; }
        public long Hash { get; set; }
    }
}