using Core.Common.NoSqlDataAccess.DataAccess;

namespace Mentor.telegram.shared.Services
{
    public class BotStorageService : IBotStorageService
    {
        private const string BotsTable = "Bots";

        private readonly INoSqlDb<Bot.telegram.funnel.Bot> _botRepository;

        public BotStorageService(INoSqlDb<Bot.telegram.funnel.Bot> botRepository)
        {
            _botRepository = botRepository;
        }

        public Bot.telegram.funnel.Bot GetById(string botId)
        {
            return _botRepository.GetByKey(BotsTable, botId);
        }
        
        public bool InsertOrUpdate(string botId, Bot.telegram.funnel.Bot bot)
        {
            _botRepository.AddOrUpdate(BotsTable, botId, bot);
            return true;
        }

        public void Initialize()
        {
            _botRepository.CreateTableIfNotExists(BotsTable);
        }
    }

    public interface IBotStorageService
    {
        void Initialize();
        Bot.telegram.funnel.Bot GetById(string botId);
        bool InsertOrUpdate(string botId, Bot.telegram.funnel.Bot bot);
    }
}
