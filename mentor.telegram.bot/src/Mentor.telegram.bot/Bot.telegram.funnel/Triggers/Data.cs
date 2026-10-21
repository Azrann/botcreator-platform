using System.Collections.Generic;

namespace Bot.telegram.funnel.Triggers
{
    public class Data
    {
        public string Message { get; set; }
        public string ReplyMessage { get; set; }
        public Dictionary<string, int> Tags { get; set; }
        public Dictionary<string, string> State { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        /* Probably no need for properties as all the info goes into Tags
         * and the logic for a trigger is maximum check if tags exists.*/
    }
}