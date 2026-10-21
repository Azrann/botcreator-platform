namespace Bot.telegram.funnel.Triggers
{
    public interface ITrigger
    {
        int Priority { get; set; }
        bool Check(Data data);
    }
}