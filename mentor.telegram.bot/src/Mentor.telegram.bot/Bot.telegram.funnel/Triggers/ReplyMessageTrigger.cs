namespace Bot.telegram.funnel.Triggers
{
    public class ReplyMessageTrigger : ITrigger
    {
        public int Priority { get; set; }
        public string ReplyMessage { get; set; }

        public ReplyMessageTrigger(string message)
        {
            ReplyMessage = message;
        }

        public bool Check(Data data)
        {
            return ReplyMessage == data.ReplyMessage;
        }
    }
}