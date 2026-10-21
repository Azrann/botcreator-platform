using System;
using System.Collections.Generic;
using System.Threading;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Newtonsoft.Json;

namespace Bot.telegram.funnel.Actions
{
    public class WidgetMessageActionProcessor : ActionProcessor<WidgetMessageAction>
    {
        private readonly IUserInterfaceService _userInterfaceService;
        private readonly IEventService _eventService;

        public WidgetMessageActionProcessor(IUserInterfaceService userInterfaceService, IEventService eventService)
        {
            _userInterfaceService = userInterfaceService;
            _eventService = eventService;
        }

        public override bool Execute(WidgetMessageAction action, Dictionary<string, object> parameters)
        {
            if (action.RemovePreviousMessage)
            {
                _userInterfaceService.RemovePreviousMessage(parameters);
            }

            if (action.Delay != 0)
            {
                Thread.Sleep(action.Delay);
            }

            _userInterfaceService.SendMessage(action.Message.ReplaceParameters(parameters), parameters);
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
                    Text = action.Message.ReplaceParameters(parameters),
                    Parameters = JsonConvert.SerializeObject(parameters)
                })
            });
            return true;
        }
    }
}