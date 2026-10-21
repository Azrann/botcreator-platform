using System.Collections.Generic;
using Bot.telegram.funnel.Actions;

namespace Bot.telegram.funnel
{
    public class Sequence
    {
        public string StartingButton { get; set; }
        public List<FunnelAction> FirstActions { get; set; }
        public List<FunnelAction> FinalActions { get; set; }
        
        public string Id { get; set; }

        public List<Step> Steps { get; set; }

        public Sequence()
        {
            Steps = new List<Step>();
        }
    }
}
