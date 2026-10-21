namespace Bot.telegram.funnel
{
    public interface IStepStates
    {
        void AddToFunnel(Funnel funnel, string sequenceId, int i);
        void AddToFunnelAsFirst(Funnel funnel, string sequenceId, string firstStepCallback);
    }
}