using System;
using System.Collections.Generic;
using System.Threading;
using Core.Common.Extensions;
using Mentor.bot.boundedContext.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Newtonsoft.Json;

namespace Bot.telegram.funnel.Actions
{
    public class WidgetMessageForKeyActionProcessor : ActionProcessor<WidgetMessageForKeyAction>
    {
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IEventService _eventService;
        private readonly ILanguageService _languageService;

        public WidgetMessageForKeyActionProcessor(IUserInterfaceService userInterfaceService, IEventService eventService, ILanguageService languageService)
        {
            _userInterfaceService = userInterfaceService;
            _eventService = eventService;
            _languageService = languageService;
        }

        public override bool Execute(WidgetMessageForKeyAction action, Dictionary<string, object> parameters)
        {
            Log.LogInf($"WidgetMessageForKeyAction Processing", action.CorrelationId);

            if (action.RemovePreviousMessage)
            {
                _userInterfaceService.RemovePreviousMessage(parameters);
            }

            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }
            Log.LogInf($"WidgetMessageForKeyAction building message for Language {parameters.GetLanguage()} and key {action.Key}", action.CorrelationId);
            var message = _languageService.Get(parameters.GetLanguage(), action.Key);
            Log.LogInf($"WidgetMessageForKeyAction sending message {message}", action.CorrelationId);
            _userInterfaceService.SendMessage(message.ReplaceParameters(parameters), parameters);
            _eventService.AddEvent(new EventItem()
            {
                Id = Guid.NewGuid(),
                BotId = parameters.GetBotId(),
                ChannelId = parameters.GetChatId().ToString(),
                Name = "WidgetResponse",
                Username = parameters.GetFirstname(),
                ClientId = parameters.GetClientId(),
                DataJson = JsonConvert.SerializeObject(new
                {
                    Type = "response",
                    ChatId = parameters.GetChatId().ToString(),
                    Text = message.ReplaceParameters(parameters),
                    Parameters = JsonConvert.SerializeObject(parameters)
                })
            });
            return true;
        }
    }
}