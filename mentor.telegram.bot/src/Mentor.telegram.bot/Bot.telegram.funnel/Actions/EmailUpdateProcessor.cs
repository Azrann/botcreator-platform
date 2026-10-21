using System.Collections.Generic;
using Mentor.telegram.bot.boundedContext.Services;
using Bot.telegram.funnel.Triggers;

namespace Bot.telegram.funnel.Actions
{
    public class EmailUpdateProcessor : ActionProcessor<EmailUpdateAction>
    {
        private readonly ITagStorageService _tagStorageService;
        private readonly IClientService _clientService;

        public EmailUpdateProcessor(ITagStorageService tagStorageService, IClientService clientService)
        {
            _tagStorageService = tagStorageService;
            _clientService = clientService;
        }

        public override bool Execute(EmailUpdateAction action, Dictionary<string, object> parameters)
        {
            var email = parameters.GetMessageText();
            var userId = parameters.GetUserId();
            var botId = parameters.GetBotId();
            _clientService.UpdateClientEmail(botId,userId.ToString(), email);
            _tagStorageService.IncrementTag(botId, userId.ToString(), TagName.EmailUpdated);
            return true;
        }
    }
}