using System.Collections.Generic;
using Bot.telegram.funnel.Actions;

namespace Bot.telegram.funnel
{
    public class StepConditionalData
    {
        public string StateName { get; set; }
        public string StateValue { get; set; }
        public int NextStep { get; set; }
        public int PreviousStep { get; set; }
        public List<FunnelAction> Actions { get; set; }
        public List<FunnelAction> PreviousStepActions { get; set; }
    }
}