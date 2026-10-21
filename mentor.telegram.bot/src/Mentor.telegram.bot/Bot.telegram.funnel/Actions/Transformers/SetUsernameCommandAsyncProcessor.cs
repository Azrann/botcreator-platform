using System.Collections.Generic;

namespace Bot.telegram.funnel.Actions
{
    public class SetUsernameCommandProcessor : ActionProcessor<SetUsernameCommandAction>
    {
        public override bool Execute(SetUsernameCommandAction action, Dictionary<string, object> parameters)
        {
            parameters.AddUsernameCommand(action.Username);
            return true;
        }
    }

    public class SetUsernameCommandAction : FunnelAction
    {
        public string Username { get; set; }
    }
}
