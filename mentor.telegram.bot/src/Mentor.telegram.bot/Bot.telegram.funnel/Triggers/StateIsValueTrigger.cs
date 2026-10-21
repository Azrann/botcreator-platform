namespace Bot.telegram.funnel.Triggers
{
    public class StateIsValueTrigger : ITrigger
    {
        public int Priority { get; set; }
        public string State { get; set; }
        public string Value { get; set; }

        public StateIsValueTrigger(string state, string value)
        {
            State = state;
            Value = value;
        }

        public bool Check(Data data)
        {
            if (data.State == null && !string.IsNullOrEmpty(State))
            {
                return true;
            }

            return data.State != null && (data.State.ContainsKey(State) && data.State[State] == Value);
        }
    }

    public class StateIsNotValueTrigger : ITrigger
    {
        public int Priority { get; set; }
        public string State { get; set; }
        public string Value { get; set; }

        public StateIsNotValueTrigger(string state, string value)
        {
            State = state;
            Value = value;
        }

        public bool Check(Data data)
        {
            if (data.State == null)
            {
                return false;
            }

            return !data.State.ContainsKey(State) || data.State[State] != Value;
        }
    }

    public class StateIsValueEmpty : ITrigger
    {
        public int Priority { get; set; }
        public string State { get; set; }

        public bool Check(Data data)
        {
            return (data.State == null) || !data.State.ContainsKey(State) || data.State[State] == string.Empty;
        }
    }

    public class StateIsNotValueEmpty : ITrigger
    {
        public int Priority { get; set; }
        public string State { get; set; }

        public StateIsNotValueEmpty()
        {
        }

        public StateIsNotValueEmpty(string state)
        {
            State = state;
        }

        public bool Check(Data data)
        {
            return (data.State != null) && data.State.ContainsKey(State) && data.State[State] != string.Empty;
        }
    }
}