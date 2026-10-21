using System.Collections.Generic;

namespace Bot.telegram.funnel.Triggers
{

    public class MessageTrigger : ITrigger
    {
        public int Priority { get; set; }
        public string Message { get; set; }

        public MessageTrigger(string message)
        {
            Message = message;
        }

        public bool Check(Data data)
        {
            return Message == data.Message;
        }
    }

    public class MessageIsNotTrigger : ITrigger
    {
        public int Priority { get; set; }
        public List<string> Messages { get; set; }

        public MessageIsNotTrigger(List<string> messages)
        {
            Messages = messages;
        }

        public bool Check(Data data)
        {
            foreach (var message in Messages)
            {
                if (message == data.Message)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class MessageStartWithTrigger : ITrigger
    {
        public int Priority { get; set; }
        public string Message { get; set; }

        public MessageStartWithTrigger(string message)
        {
            Message = message;
        }

        public bool Check(Data data)
        {
            return data.Message.StartsWith(Message);
        }
    }
}