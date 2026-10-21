using System;
using System.Collections.Generic;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Bot.telegram.funnel.Triggers;

namespace Bot.telegram.funnel.Actions
{
    public class ClientCreateProcessor : ActionProcessor<ClientCreateAction>
    {
        private readonly ITagStorageService _tagStorageService;
        private readonly IClientService _clientService;

        public ClientCreateProcessor(ITagStorageService tagStorageService, IClientService clientService)
        {
            _tagStorageService = tagStorageService;
            _clientService = clientService;
        }

        public override bool Execute(ClientCreateAction action, Dictionary<string, object> parameters)
        {
            var email = parameters.GetMessageText();
            var userId = parameters.GetUserId();
            var botId = parameters.GetBotId();
            var firstname = parameters.GetFirstname();
            var lastname = parameters.GetLastname();
            var username = parameters.GetUsername();
            var chatId = parameters.GetChatId();

            _clientService.AddClient(new Client()
            {
                Id = Guid.NewGuid(),
                BotId = botId,
                User = new TelegramUser()
                {
                    Id = userId.ToString(),
                    CreatedOn = DateTime.Now,
                    FirstName = firstname,
                    LastName = lastname,
                    UserName = username,
                },
                ChatId = chatId,
                CreatedOn = DateTime.Now,
                LastUpdated = DateTime.Now
            });
            _tagStorageService.IncrementTag(botId, userId.ToString(), TagName.ClientCreated);

            return true;
        }
    }
}