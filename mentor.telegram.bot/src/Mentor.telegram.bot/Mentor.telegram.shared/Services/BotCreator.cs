using System;
using Core.Common.Extensions;
using Bot.telegram.funnel.Triggers;
using Mentor.telegram.shared.BotTemplates;

namespace Mentor.telegram.shared.Services
{
    public class BotCreator
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(BotCreator));
        private readonly IFunnelStorageService _funnelStorageService;
        private readonly IBotStorageService _botStorageService;
        private readonly IBotDefinitionStorageService _botDefinitionStorageService;

        public BotCreator(IFunnelStorageService funnelStorageService, IBotStorageService botStorageService
        , IBotDefinitionStorageService botDefinitionStorageService)
        {
            _funnelStorageService = funnelStorageService;
            _botStorageService = botStorageService;
            _botDefinitionStorageService = botDefinitionStorageService;
        }

        public bool DeployBot(string botId)
        {
            var correlationId = Guid.NewGuid();
            if (string.IsNullOrEmpty(botId))
            {
                Log.LogErr("DeployBot missing parameter botId.", correlationId);
                return false;
            }

            var botDefinition = _botDefinitionStorageService.GetDefinitionById(botId);

            return UpdateBot(botDefinition, botId);
        }


        public IBotDefinition GetBotDefinition(string botId)
        {
            var correlationId = Guid.NewGuid();
            if (string.IsNullOrEmpty(botId))
            {
                Log.LogErr("GetBotDefinition missing parameter botId.", correlationId);
                return null;
            }

            return _botDefinitionStorageService.GetDefinitionById(botId);
        }
        

        private bool UpdateBot(IBotDefinition botDefinition, string botId)
        {
            Guid funnelId = Guid.NewGuid();
            var bot = new Bot.telegram.funnel.Bot()
            {
                Id = botId,
                AdminId = Guid.NewGuid()
            };
            bot.TriggersToFunnel.Add(new TagNotAddedTrigger(botId), funnelId);

            var funnel = botDefinition.GetFunnel();
            
            _funnelStorageService.InsertOrUpdate(funnelId.ToString(), funnel);
            _botStorageService.InsertOrUpdate(botId, bot);
            return true;
        }
    }
}
