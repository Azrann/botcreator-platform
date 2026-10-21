using Core.Common.NoSqlDataAccess.DataAccess;
using Bot.Configuration;
using Mentor.telegram.bot.models.Facebook;

namespace Mentor.bot.boundedContext.Services
{
    public interface IFacebookBotStorageService
    {
        void RemoveFacebookBotData(string botId);

        FacebookBotData GetFacebookBotDataById(string botId);

        void InsertOrUpdate(string botId, FacebookBotData facebookBotData);

        void Initialize();
    }
    public class FacebookBotStorageService : IFacebookBotStorageService
    {
        private const string FacebookBotDataTable = "FacebookBotTable";
        private readonly INoSqlDb<FacebookBotData> _repository;
        private readonly IConfigurationCommon _config;
        public FacebookBotStorageService(INoSqlDb<FacebookBotData> repository,IConfigurationCommon configuration)
        {
            _config = configuration;
            _repository = repository;
        }
        public FacebookBotData GetFacebookBotDataById(string botId)
        {
            return _repository.GetByKey(FacebookBotDataTable, botId);
        }

        public void Initialize()
        {
            _repository.CreateTableIfNotExists(FacebookBotDataTable);
            if (_config.IsDevelopment())
            {
                var botId = "c4f418ab-c690-4a3c-bffa-fd65f3ac72f2";
                _repository.AddOrUpdate(FacebookBotDataTable, botId, new FacebookBotData
                {
                    AppSecret = "REPLACE_WITH_YOUR_VALUE",
                    PageToken= "REPLACE_WITH_YOUR_VALUE"
                });
            }

            else
            {
                //ToDo: Discuss the logic for production with Alberto. We will get the app secret and page token from new web app/page after fb user login.
                //Bot id can be new guid
                
            }
        }

        public void InsertOrUpdate(string botId, FacebookBotData facebookBotData)
        {
            _repository.AddOrUpdate(FacebookBotDataTable, botId, facebookBotData);

        }

        public void RemoveFacebookBotData(string botId)
        {
            _repository.Delete(FacebookBotDataTable, botId);
        }
    }
}
