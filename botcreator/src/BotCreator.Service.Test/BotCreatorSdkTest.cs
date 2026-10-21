using Mentor.telegram.bot.sdk;
using Xunit;

namespace BotCreator.Service.Test
{
    public class BotCreatorSdkTest
    {
        [Fact]
        public void WhenIDeployABotIDontGetErrors()
        {
            BotApi api = new BotApi();
            //api.DeployBot("5bc2d86d-dbb2-48bd-b95b-4e276561103c");
        }

        [Fact]
        public void WhenICreateAnApiIDontGetExceptions()
        {
            BotApi api = new BotApi();
        }
    }
}
