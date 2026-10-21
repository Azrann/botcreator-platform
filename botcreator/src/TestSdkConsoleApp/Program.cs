using System;
using Mentor.telegram.bot.sdk;
using Mentor.telegram.shared.BotTemplates;

namespace TestSdkConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BotApi api = new BotApi();
            IBotDefinition aBotDefinition = new BasicChatBot()
            {
                ConnectingToAgentMessage = "Connecting to first available agent."
            };
           // api.CreateBot("Testing123432", aBotDefinition);
            api.DeployBot("5bc2d86d-dbb2-48bd-b95b-4e276561103c");
        }
    }
}
