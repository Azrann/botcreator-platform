using System;

namespace Bot.telegram.funnel.Actions
{
    public class FunnelAction
    {
        public Guid CorrelationId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}