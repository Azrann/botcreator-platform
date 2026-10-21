using Core.Common.NoSqlDataAccess.DataAccess;
using BotCreator.Core.Common.Models;

namespace BotCreator.Service.Bot
{
    public interface IWidgetConfigStorageService
    {
        void RemoveWidgetConfig(string botId);
        WidgetConfigData GetWidgetConfigById(string botId);
        WidgetConfig GetWidgetHtmlConfigById(string botId, bool isDevEnv);
        void InsertOrUpdate(string botId, WidgetConfigData WidgetConfig);
        void SetBotActive(string botId, bool isActive);
        void Initialize();
    }

    public class WidgetConfigStorageService: IWidgetConfigStorageService
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

        public WidgetConfig GetWidgetHtmlConfigById(string botId, bool isDevEnv)
        {
            return WidgetConfig.NewWidget(GetWidgetConfigById(botId), botId, isDevEnv);
        }

        public void Initialize()
        {
            _repository.CreateTableIfNotExists(WidgetConfigTable,true);
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
}