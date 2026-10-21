using System.Collections.Generic;

namespace Bot.telegram.funnel.Actions
{
    public class CleanUpChannelActionAsync : FunnelAction
    {
        public int ChannelId { get; set; }
    }

    public class GetUsernamesResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public List<string> Payload { get; set; }
    }
}
