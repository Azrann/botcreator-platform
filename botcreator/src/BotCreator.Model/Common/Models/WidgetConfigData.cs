using System;
using System.Collections.Generic;
using System.Text;

namespace BotCreator.Core.Common.Models
{
   public class WidgetConfigData
    {
        public string BotId { get; set; }
        public string Language { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string HeaderTextColor { get; set; }
        public string ChatNow { get; set; }
        public string BubbleMessage { get; set; } //Capture the attention with this message
        public string Description { get; set; } //tell your customers why they should continue talking with the bot
        public string BotName { get; set; }
        public string BotAvatarUrl { get; set; }
        public bool RemovePrevious { get; set; }
        public bool Active { get; set; }
        public string WidgetUrl { get; set; }
    }
}
