using System.Collections.Generic;

namespace Mentor.telegram.bot.models
{
    public class UserStates
    {
        public Dictionary<string, string> States { get; set; }

        public bool HasState(string tag)
        {
            return States.ContainsKey(tag);
        }

        public string GetState(string tag)
        {
            if (States.ContainsKey(tag))
            {
                return States[tag];
            }

            return null;
        }
    }
}