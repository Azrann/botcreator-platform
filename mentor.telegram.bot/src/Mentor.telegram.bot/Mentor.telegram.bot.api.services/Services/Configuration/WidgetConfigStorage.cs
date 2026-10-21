using Core.Common.NoSqlDataAccess.DataAccess;

namespace Mentor.telegram.bot.api.services.Services.Configuration
{
    public interface IWidgetConfigStorageService
    {
        void RemoveWidgetConfig(string botId);
        WidgetConfigData GetWidgetConfigById(string botId);
        void InsertOrUpdate(string botId, WidgetConfigData WidgetConfig);
        void SetBotActive(string botId, bool isActive);
        void Initialize();
    }

    public class WidgetConfigStorageService : IWidgetConfigStorageService
    {
        private const string WidgetConfigTable = "WidgetConfigTable";
        private readonly INoSqlDb<WidgetConfigData> _repository;

        public WidgetConfigStorageService(INoSqlDb<WidgetConfigData> repository)
        {
            _repository = repository;
        }
        public WidgetConfigData GetWidgetConfigById(string botId)
        {
            return _repository.GetByKey(WidgetConfigTable, botId);
        }

        public void Initialize()
        {
            _repository.CreateTableIfNotExists(WidgetConfigTable, true);
        }

        public void InsertOrUpdate(string botId, WidgetConfigData widgetConfig)
        {
            _repository.AddOrUpdate(WidgetConfigTable, botId, widgetConfig);
        }

        public void RemoveWidgetConfig(string botId)
        {
            _repository.Delete(WidgetConfigTable, botId);
        }

        public void SetBotActive(string botId, bool isActive)
        {
            var widgetConfig = _repository.GetByKey(WidgetConfigTable, botId);
            widgetConfig.Active = isActive;
            _repository.AddOrUpdate(WidgetConfigTable, botId, widgetConfig);
        }
    }

    public class WidgetConfigData
    {
        public string BotId { get; set; }
        public string Language { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string HeaderTextColor { get; set; }
        public string ChatNow { get; set; }
        public string BubbleMessage { get; set; } //Capture the attention with this message
        public string Description { get; set; } //tell your customers why they should continue talking with the bot
        public string BotName { get; set; }
        public string BotAvatarUrl { get; set; }
        public bool RemovePrevious { get; set; }
        public bool Active { get; set; }
        public string WidgetUrl { get; set; }
    }
}