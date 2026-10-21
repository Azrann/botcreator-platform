using System.Collections.Generic;

namespace Bot.telegram.funnel.Triggers
{
    public class TagsAddedTrigger : ITrigger
    {
        public int Priority { get; set; }

        public List<string> Tags { get; set; }

        public TagsAddedTrigger(List<string> tags)
        {
            Tags = tags;
        }
        
        public bool Check(Data data)
        {
            if (data.Tags == null && Tags.Count > 0)
            {
                return false;
            }

            foreach (var tag in Tags)
            {
                if (!data.Tags.ContainsKey(tag))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class TagsNotAddedTrigger : ITrigger
    {
        public int Priority { get; set; }
        public List<string> Tags { get; set; }

        public TagsNotAddedTrigger(List<string> tags)
        {
            Tags = tags;
        }

        public bool Check(Data data)
        {
            if (data.Tags == null && Tags.Count > 0)
            {
                return true;
            }

            foreach (var tag in Tags)
            {
                if (data.Tags.ContainsKey(tag))
                {
                    return false;
                }
            }

            return true;
        }
    }


}