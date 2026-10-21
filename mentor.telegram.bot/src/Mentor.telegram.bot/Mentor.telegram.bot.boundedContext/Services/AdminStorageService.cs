using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.NoSqlDataAccess.DataAccess;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public interface IAdminStorageService
    {
        void Remove(string adminId);
        AdminData GetById(string adminId);
        void InsertOrUpdate(string adminId, AdminData adminData);
        void AddChannel(string adminId, TelegramChannel telegramChannel);
        void RemoveChannel(string adminId, int channelId);
        void Initialize();
        TelegramChannel GetChannel(string adminId, int channelId);
    }

    public class AdminStorageService : IAdminStorageService
    {
        private readonly INoSqlDb<AdminData> _repository;
        private const string AdminDataTable = "AdminDataTable";
        public AdminStorageService(INoSqlDb<AdminData> repository)
        {
            _repository = repository;
        }

        public void Remove(string adminId)
        {
            _repository.Delete(AdminDataTable, adminId);
        }

        public AdminData GetById(string adminId)
        {
            var result = _repository.GetByKey(AdminDataTable, adminId);

            if (result != null)
            {
                return result;
            }

            result = new AdminData()
            {
                BotIds = new List<string>(),
                Channels = new List<TelegramChannel>(),
                FunnelInfos = new List<FunnelInfo>()
            };

            return result;
        }

        public void InsertOrUpdate(string adminId, AdminData adminData)
        {
            _repository.AddOrUpdate(AdminDataTable, adminId, adminData);
        }

        public void AddChannel(string adminId, TelegramChannel telegramChannel)
        {
            AdminData adminData = GetById(adminId);
            adminData.Channels.Add(telegramChannel);
            _repository.AddOrUpdate(AdminDataTable, adminId, adminData);
        }

        public void RemoveChannel(string adminId, int channelId)
        {
            AdminData adminData = GetById(adminId);
            adminData.Channels.RemoveAll(x => x.Id == channelId);
            _repository.AddOrUpdate(AdminDataTable, adminId, adminData);
        }

        public void Initialize()
        {
            _repository.CreateTableIfNotExists(AdminDataTable);
        }

        public TelegramChannel GetChannel(string adminId, int channelId)
        {
            var mentorData = GetById(adminId);
            return mentorData.Channels.FirstOrDefault(x => x.Id == channelId);
        }
    }

    public class AdminData     
    {
        public List<TelegramChannel> Channels { get; set; }
        public List<string> BotIds { get; set; }
        public List<FunnelInfo> FunnelInfos { get; set; }
    }

    public class TelegramChannel    
    {
        public TelegramChannelType Type { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public long AccessHash { get; set; }
    }

    public class FunnelInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid FunnelId { get; set; }
    }

    public enum TelegramChannelType
    {
        Channel,
        Chat
    }
}
