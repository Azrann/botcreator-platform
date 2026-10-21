using System.Collections.Generic;

namespace Bot.telegram.funnel.Triggers
{
    public class PropertyUsernameIsTrigger : ITrigger
    {
        public int Priority { get; set; }
        public List<string> Usernames { get; set; }

        public PropertyUsernameIsTrigger(List<string> usernames)
        {
            Usernames = usernames;
        }

        public bool Check(Data data)
        {
            if (data.Properties == null || data.Properties.Count == 0 
                || !data.Properties.ContainsKey(ParametersExtensions.Username))
            {
                return false;
            }

            var username = data.Properties.GetUsername();

            return Usernames.Contains(username);
        }
    }

    public class PropertyUsernameIsNotTrigger : ITrigger
    {
        public int Priority { get; set; }
        public List<string> Usernames { get; set; }

        public PropertyUsernameIsNotTrigger(List<string> usernames)
        {
            Usernames = usernames;
        }

        public bool Check(Data data)
        {
            if (data.Properties == null || data.Properties.Count == 0
                                        || !data.Properties.ContainsKey(ParametersExtensions.Username))
            {
                return false;
            }

            var username = data.Properties.GetUsername();

            return !Usernames.Contains(username);
        }
    }
}