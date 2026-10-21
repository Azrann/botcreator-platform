namespace Bot.telegram.funnel.Actions
{
    public class StateUpdateAction : FunnelAction
    {
        public string State { get; set; }
        public string Value { get; set; }

        public StateUpdateAction(string state, string value)
        {
            State = state;
            Value = value;
        }
    }
}