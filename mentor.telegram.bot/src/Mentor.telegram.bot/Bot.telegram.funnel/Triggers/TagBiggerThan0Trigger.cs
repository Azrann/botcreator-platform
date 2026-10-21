namespace Bot.telegram.funnel.Triggers
{
    public class TagBiggerThan0Trigger : ITrigger
    {
        public int Priority { get; set; }
        public string Tag { get; set; }

        public TagBiggerThan0Trigger(string tag)
        {
            Tag = tag;
        }

        public bool Check(Data data)
        {
            if (data.Tags == null)
            {
                return false;
            }

            if (data.Tags.ContainsKey(Tag))
            {
                return data.Tags[Tag] > 0;
            }

            return false;
        }
    }

    public class TagNotBiggerThan0Trigger : ITrigger
    {
        public int Priority { get; set; }
        public string Tag { get; set; }

        public TagNotBiggerThan0Trigger(string tag)
        {
            Tag = tag;
        }

        public bool Check(Data data)
        {
            if (data.Tags == null)
            {
                return true;
            }

            if (data.Tags.ContainsKey(Tag))
            {
                return data.Tags[Tag] < 1;
            }

            return true;
        }
    }
}