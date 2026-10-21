namespace Bot.telegram.funnel.Actions
{
    public class StateUpdateActionWithData : FunnelAction
    {
        public string State { get; set; }

        public StateUpdateActionWithData(string state)
        {
            State = state;
        }
    }
}