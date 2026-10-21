using System;
using System.Collections.Generic;
using Core.Common.NoSqlDataAccess.DataAccess;

namespace channel.telegram.bot.boundedContext.Services
{
    public interface IChannelStorageService
    {
        void Remove(int channelId);
        TelegramChannelData GetById(int channelId);
        void InsertOrUpdate(int channelId, TelegramChannelData channelData);
        void Initialize();
    }

    public class ChannelStorageService : IChannelStorageService
    {
        private readonly INoSqlDb<TelegramChannelData> _repository;
        private const string ChannelDataTable = "ChannelDataTable";

        public ChannelStorageService(INoSqlDb<TelegramChannelData> repository)
        {
            _repository = repository;
        }

        public void Remove(int channelId)
        {
            _repository.Delete(ChannelDataTable, channelId.ToString());
        }

        public TelegramChannelData GetById(int channelId)
        {
            var result = _repository.GetByKey(ChannelDataTable, channelId.ToString());

            if (result != null)
            {
                return result;
            }

            return result;
        }

        public void InsertOrUpdate(int channelId, TelegramChannelData channelData)
        {
            _repository.AddOrUpdate(ChannelDataTable, channelId.ToString(), channelData);
        }

        public void Initialize()
        {
            _repository.CreateTableIfNotExists(ChannelDataTable);
            var admins = new List<string>()
            {
                "REPLACE_WITH_YOUR_VALUE","dev92847" }; //"larsvelthuis" 
            //,"verifiedcryptotraders_admin", "flemishcarpenter", "kryptodansky", "nickalbernas","rushi3219"
            var excluded = new List<string>()
            {
                "VerifiedCryptoGroup", "larsvelthuis", "verifiedcryptotraders_admin","VerifiedCrypto", "TastyBean", "BitReKKer", "rushi3219", "Kryptodansky", "VerifiedCrypto", "jegerche"
                ,"REPLACE_WITH_YOUR_VALUE","BotYourself","verifiedcryptobutler","verified_crypto_tracker_bot","verified_crypto_trading_bot","Flemishcarpenter","verifiedcryptotraders_admin",
                "Kryptodansky","TastyBean","jegerche","BitReKKer","NickAlbernas","browneyedmommy","Cryptobrandstudio","Smart_Options","smartoptions_io",
                "KolonelKakapo","rushi3219","kevinputty","Sam_INDX","safetradingsupport","Koen","Flemishcarpenter","verified_crypto_notification_bot",
                "verifiedcryptobutler","andreassschmidt","cfa710","OriKl","verified_crypto_status_bot","MacroMillennial","plsarma","kchrome7","CryptoRey","freeland89","LuckF","Satfinderr","arr4946","youngtrashjr","crptdesh"
            };

            InsertOrUpdate(1456376137, new TelegramChannelData()
            {
                DataStreamType = DataStreamType.Url,
                DataStreamUrl = new List<string>() { "https://verifiedcryptotraders.com/member-area/members/platinum",
                "https://verifiedcryptotraders.com/member-area/members/platinum-monthly"},
                Admins = admins,
                Name = "Signals Plus Platinum (short)",
                Id = 1456376137,
                AccessHash = 0,
                ActionType = ActionType.KickAndBan,
                LastCheck = DateTime.Now,
                Excluded = excluded
            });

            InsertOrUpdate(1248811572, new TelegramChannelData()
            {
                DataStreamType = DataStreamType.Url,
                DataStreamUrl = new List<string>() { "https://verifiedcryptotraders.com/member-area/members/platinum",
                "https://verifiedcryptotraders.com/member-area/members/platinum-monthly" },
                Admins = admins,
                Name = "Signals Plus Platinum Chat",
                Id = 1248811572,
                AccessHash = 0,
                ActionType = ActionType.KickAndBan,
                LastCheck = DateTime.Now,
                Excluded = excluded
            });

            InsertOrUpdate(1227233204, new TelegramChannelData()
            {
                DataStreamType = DataStreamType.Url,
                DataStreamUrl = new List<string>() { "https://verifiedcryptotraders.com/member-area/members/platinum" },
                Admins = admins,
                Name = "Signal Plus Platinum - CryptoCoffee",
                Id = 1227233204,
                AccessHash = 0,
                ActionType = ActionType.KickAndBan,
                LastCheck = DateTime.Now,
                Excluded = excluded
            });

            InsertOrUpdate(1286178319, new TelegramChannelData()
            {
                DataStreamType = DataStreamType.Url,
                DataStreamUrl = new List<string>()
                {
                    "https://verifiedcryptotraders.com/member-area/members/silver",
                    "https://verifiedcryptotraders.com/member-area/members/platinum",
                    "https://verifiedcryptotraders.com/member-area/members/gold",
                    "https://verifiedcryptotraders.com/member-area/members/gold-subscription-trial-7-days",
                    "https://verifiedcryptotraders.com/member-area/members/platinum-monthly",
                    "https://verifiedcryptotraders.com/member-area/members/gold-monthly",
                    "https://verifiedcryptotraders.com/member-area/members/silver-monthly"
                },
                Admins = admins,
                Name = "Verified Silver Traders",
                Id = 1286178319,
                AccessHash = 0,
                ActionType = ActionType.KickAndBan,
                LastCheck = DateTime.Now,
                Excluded = excluded
            });

            InsertOrUpdate(1281895600, new TelegramChannelData()
            {
                DataStreamType = DataStreamType.Url,
                DataStreamUrl = new List<string>()
                {
                    "https://verifiedcryptotraders.com/member-area/members/platinum",
                    "https://verifiedcryptotraders.com/member-area/members/gold",
                    "https://verifiedcryptotraders.com/member-area/members/gold-subscription-trial-7-days",
                    "https://verifiedcryptotraders.com/member-area/members/platinum-monthly",
                    "https://verifiedcryptotraders.com/member-area/members/gold-monthly",
                    "https://verifiedcryptotraders.com/member-area/members/silver-monthly"
                },
                Admins = admins,
                Name = "Verified Gold",
                Id = 1281895600,
                AccessHash = 0,
                ActionType = ActionType.KickAndBan,
                LastCheck = DateTime.Now,
                Excluded = excluded
            });

            InsertOrUpdate(1167107748, new TelegramChannelData()
            {
                DataStreamType = DataStreamType.Url,
                DataStreamUrl = new List<string>()
                {
                    "https://verifiedcryptotraders.com/member-area/members/platinum",
                    "https://verifiedcryptotraders.com/member-area/members/gold",
                    "https://verifiedcryptotraders.com/member-area/members/gold-subscription-trial-7-days",
                    "https://verifiedcryptotraders.com/member-area/members/platinum-monthly",
                    "https://verifiedcryptotraders.com/member-area/members/gold-monthly"
                },
                Admins = admins,
                Name = "Verified Crypto Traders Gold - Chat 💬",
                Id = 1167107748,
                AccessHash = 0,
                ActionType = ActionType.KickAndBan,
                LastCheck = DateTime.Now,
                Excluded = excluded
            });

            InsertOrUpdate(1208625946, new TelegramChannelData()
            {
                DataStreamType = DataStreamType.Url,
                DataStreamUrl = new List<string>()
                {
                    "https://verifiedcryptotraders.com/member-area/members/silver",
                    "https://verifiedcryptotraders.com/member-area/members/platinum",
                    "https://verifiedcryptotraders.com/member-area/members/gold",
                    "https://verifiedcryptotraders.com/member-area/members/gold-subscription-trial-7-days",
                    "https://verifiedcryptotraders.com/member-area/members/platinum-monthly",
                    "https://verifiedcryptotraders.com/member-area/members/gold-monthly",
                    "https://verifiedcryptotraders.com/member-area/members/silver-monthly"
                },
                Admins = admins,
                Name = "Verified Silver Traders - Chat",
                Id = 1208625946,
                AccessHash = 0,
                ActionType = ActionType.KickAndBan,
                LastCheck = DateTime.Now,
                Excluded = excluded
            });
        }
    }

    public class TelegramChannelData     
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public long AccessHash { get; set; }
        public DateTime LastCheck { get; set; }
        public DataStreamType DataStreamType { get; set; }
        public List<string> DataStreamUrl { get; set; }
        public ActionType ActionType { get; set; }
        public List<string> Admins { get; set; }
        public List<string> Excluded { get; set; }
    }

    public enum DataStreamType  
    {
        Url,
        GoogleDrive
    }

    public enum ActionType
    {
        Notify,
        Kick,
        None,
        KickAndBan
    }
}
