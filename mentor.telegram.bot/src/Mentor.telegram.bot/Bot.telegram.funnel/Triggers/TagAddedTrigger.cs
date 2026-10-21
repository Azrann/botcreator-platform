namespace Bot.telegram.funnel.Triggers
{
    public class TagAddedTrigger : ITrigger
    {
        public int Priority { get; set; }
        public string Tag { get; set; }

        public TagAddedTrigger(string tag)
        {
            Tag = tag;
        }

        public bool Check(Data data)
        {
            if (data.Tags == null && !string.IsNullOrEmpty(Tag))
            {
                return true;
            }

            return data.Tags.ContainsKey(Tag);
        }
    }

    public class TagNotAddedTrigger : ITrigger
    {
        public int Priority { get; set; }
        public string Tag { get; set; }

        public TagNotAddedTrigger(string tag)
        {
            Tag = tag;
        }

        public bool Check(Data data)
        {
            if (data.Tags == null && !string.IsNullOrEmpty(Tag))
            {
                return true;
            }

            return !data.Tags.ContainsKey(Tag);
        }
    }
}