using Core.Common.NoSqlDataAccess.DataAccess;
using Mentor.telegram.shared.BotTemplates;
using Newtonsoft.Json;

namespace Mentor.telegram.shared.Services
{
    public class BotDefinitionStorageService : IBotDefinitionStorageService
    {
        private const string BotDefinitionTable = "BotDefinition";

        private readonly INoSqlDb<BotDefinition> _botConfigRepository;

        public BotDefinitionStorageService(INoSqlDb<BotDefinition> botConfigRepository)
        {
            _botConfigRepository = botConfigRepository;
        }

        public BotDefinition GetById(string botId)
        {
            var botConfig = _botConfigRepository.GetByKey(BotDefinitionTable, botId);

            return botConfig;
        }

        public IBotDefinition GetDefinitionById(string botId)
        {
            var botConfig = _botConfigRepository.GetByKey(BotDefinitionTable, botId);

            switch (botConfig.Type)
            {
                case "BasicChatBot":
                    return JsonConvert.DeserializeObject<BasicChatBot>(botConfig.SerializedDefinition);
                case "WidgetSupportBot":
                    return JsonConvert.DeserializeObject<WidgetSupportBot>(botConfig.SerializedDefinition);
                default:
                    return null;
            }
           
        }

        public bool InsertOrUpdate(string botId, IBotDefinition botConfig)
        {
            BotDefinition config = new BotDefinition()
            {
                BotId = botId,
                Type = botConfig.GetType().Name,
                SerializedDefinition = JsonConvert.SerializeObject(botConfig)
            };

            _botConfigRepository.AddOrUpdate(BotDefinitionTable, botId, config);
            return true;
        }

        public void Initialize()
        {
            _botConfigRepository.CreateTableIfNotExists(BotDefinitionTable, true);
        }
    }

    public interface IBotDefinitionStorageService
    {
        void Initialize();
        BotDefinition GetById(string botId);
        bool InsertOrUpdate(string botId, IBotDefinition botConfig);
        IBotDefinition GetDefinitionById(string botId);
    }

    public class BotDefinition
    {
        public string BotId { get; set; }
        public string Type { get; set; }
        public string SerializedDefinition { get; set; }
    }
}
