using Core.Common.NoSqlDataAccess.DataAccess;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;
using System.Collections.Generic;

namespace Mentor.telegram.shared.Services
{
    public class FunnelStorageService : IFunnelStorageService
    {
        private const string FunnelsTable = "Funnels";

        private readonly INoSqlDb<Funnel> _funnelRepository;
        private const string BotYourselfWelcomeMessage = "Welcome to Botyourself. Your bot is up and running go on and create your own funnel";
        private const string StartMessage = "/start";

        public FunnelStorageService(INoSqlDb<Funnel> funnelRepository)
        {
            _funnelRepository = funnelRepository;
        }

        public Funnel GetById(string funnelId)
        {
            var funnelResult = _funnelRepository.GetByKey(FunnelsTable, funnelId);

            if (funnelResult != null)
            {
                return funnelResult;
            }

            var result = new Funnel();
            result.MessageActions.Add(new MessageTrigger(StartMessage), new List<FunnelAction>()
            {
                new MessageAction(BotYourselfWelcomeMessage)
            });
            return result;
        }

        public bool InsertOrUpdate(string botId, Funnel funnel)
        {
            _funnelRepository.AddOrUpdate(FunnelsTable, botId, funnel);
            return true;
        }

        public void Initialize()
        {
            _funnelRepository.CreateTableIfNotExists(FunnelsTable, true);
        }
    }

    public interface IFunnelStorageService
    {
        void Initialize();
        Funnel GetById(string funnelId);
        bool InsertOrUpdate(string botId, Funnel funnel);
    }
}
