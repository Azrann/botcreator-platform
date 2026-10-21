using System.Collections.Generic;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;

namespace Mentor.telegram.shared.BotTemplates
{
    public class BasicChatBot : IBotDefinition
    {
        // Property need to be public so that can be Serialized.
        public string ConnectingToAgentMessage { get; set; }

        public Funnel GetFunnel()
        {
            var result = new Funnel();
            
            result.MessageActions.Add(new MessageTrigger("menu"),
                new List<FunnelAction>()
                {
                    new WidgetMessageAction(ConnectingToAgentMessage),
                    new ClientRequesTalkActionAsync(),
                });
            return result;
        }
    }
}