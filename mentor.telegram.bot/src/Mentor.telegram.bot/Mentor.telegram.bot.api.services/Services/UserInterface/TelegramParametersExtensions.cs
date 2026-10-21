using System;
using System.Collections.Generic;

namespace Mentor.telegram.bot.api.Services.UserInterface
{
    public static class TelegramParametersExtensions
    {
        private const string MessageId = "MessageId";

        public static long GetMessageId(this Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey(MessageId))
            {
                return (long)parameters[MessageId];
            }

            throw new ArgumentException($"MessageId not found in parameters");
        }

        public static void AddMessageId(this Dictionary<string, object> parameters, long messageId)
        {
            if (parameters.ContainsKey(MessageId))
            {
                parameters[MessageId] = messageId;
                return;
            }
            parameters.Add(MessageId, messageId);
        }
    }
}
