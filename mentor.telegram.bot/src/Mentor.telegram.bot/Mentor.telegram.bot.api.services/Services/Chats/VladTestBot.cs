using System.Collections.Generic;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;

namespace Mentor.telegram.bot.api.Services.Chats
{
    public class VladTestBotChat
    {
    }

    public static class VladTestBot
    {
        public static Funnel GetFunnel()
        {
            var result = new Funnel();
            result.MessageActions.Add(new MessageTrigger("FAQ"),
                new List<FunnelAction>()
                {
                    new WidgetMessageAction("[Firstname] nice to see you."),
                    new WidgetMessageAction("Most common questions are:. /n/n 1) Where is my delivery.")
                });
            result.MessageActions.Add(new MessageTrigger("menu"),
                new List<FunnelAction>()
                {
                    new WidgetMessageButtonAction("Here are your options:", new List<List<Button>>()
                    {
                        new List<Button>()
                            {new Button(){Text = "See FAQ"}},
                        new List<Button>()
                            {new Button(){Text = "I have an Issue"}},
                        new List<Button>()
                            {new Button(){Text = "I need human help."}}
                    })
                });
            result.MessageActions.Add(new MessageTrigger("See FAQ"),
                new List<FunnelAction>()
                {
                    new WidgetMessageAction("[Firstname] nice to see you."),
                    new WidgetMessageAction("Most common questions are:. /n/n 1) Where is my delivery.")
                });

            result.CallbackActions.Add(new MessageTrigger("I need human help."),
                new List<FunnelAction>()
                {
                    new WidgetMessageAction("Wait while we connect you with an agent."),
                    new ClientRequesTalkActionAsync(),
                });
            return result;
        }
    }
}
