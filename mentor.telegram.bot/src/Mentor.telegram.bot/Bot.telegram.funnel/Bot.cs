using System;
using System.Collections.Generic;
using System.Linq;
using Bot.telegram.funnel.Triggers;
using Newtonsoft.Json;

namespace Bot.telegram.funnel
{
    [JsonConverter(typeof(BotConverter))]
    public class Bot
    {
        public Bot()
        {
            TriggersToFunnel= new Dictionary<ITrigger, Guid>();
        }

        public string Id { get; set; }
        public Guid AdminId { get; set; }
        public Dictionary<ITrigger, Guid> TriggersToFunnel {get ; set; }

        public Guid GetFirstFunnelId(Data data)
        {
            foreach (var trigger in TriggersToFunnel.Keys.OrderByDescending(x => x.Priority))
            {
                if (trigger.Check(data))
                {
                    return TriggersToFunnel[trigger];
                }
            }

            return Guid.Empty;
        }
    }
}