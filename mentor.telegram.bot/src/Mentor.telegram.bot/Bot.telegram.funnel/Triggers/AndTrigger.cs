using System.Collections.Generic;

namespace Bot.telegram.funnel.Triggers
{
    public class AndTrigger : ITrigger
    {
        public int Priority { get; set; }
        public List<ITrigger> Triggers { get; set; }

        public AndTrigger()
        {
            Triggers= new List<ITrigger>();
        }
        
        public bool Check(Data data)
        {
            foreach (var trigger in Triggers)
            {
                if (!trigger.Check(data))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class OrTrigger : ITrigger
    {
        public int Priority { get; set; }
        public List<ITrigger> Triggers { get; set; }

        public OrTrigger()
        {
            Triggers = new List<ITrigger>();
        }

        public bool Check(Data data)
        {
            foreach (var trigger in Triggers)
            {
                if (trigger.Check(data))
                {
                    return true;
                }
            }
            return false;
        }
    }

}