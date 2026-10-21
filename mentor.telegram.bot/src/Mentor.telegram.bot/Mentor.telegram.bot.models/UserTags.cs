using System.Collections.Generic;

namespace Mentor.telegram.bot.models
{
    public class UserTags
    {
        public Dictionary<string, int> Tags { get; set; }

        public bool HasTag(string tag)
        {
            return Tags.ContainsKey(tag);
        }

        public int? GetTag(string tag)
        {
            if (Tags.ContainsKey(tag))
            {
                return Tags[tag];
            }

            return null;
        }
    }
}
