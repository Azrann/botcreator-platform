using System.Collections.Generic;
using Core.Common.NoSqlDataAccess.DataAccess;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;
using Mentor.telegram.shared.Services;
using Newtonsoft.Json;
using Xunit;

namespace Mentor.telegram.bot.api.test
{
    public class FunnelStorageTests
    {
        private readonly string botId = "TestBotID1";
        private readonly string connection = @"REPLACE_WITH_YOUR_VALUE";
        private const string StartMessage = "/start";
        private const string WelcomeMessage = "Welcome to CryptoTraders";
        private const string BotYourselfWelcomeMessage = "Welcome to Botyourself. Your bot is up and running go on and create your own funnel";
        private const string ButtonBuyCourse = "Buy Course";
        private const string ButtonGoBack = "Go Back";
        private const string CourseSelectionMessage = "Which course would you like to buy:";
        private const string ButtonBegginer = "Begginer Trading";
        private const string ButtonIntermidiate = "Intermidiate Trading";
        private const string ButtonAdvance = "Advance Trading";
        private const string ButtonPro = "Proffesional Trading";

        [Fact]
        public void WhenSerializeAFunnelICanDeserializeIt()
        {
            var funnel = GetFunnel();

            string serializedJson = JsonConvert.SerializeObject(funnel, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });

            var deserializedObject = JsonConvert.DeserializeObject<Funnel>(serializedJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
          
            Assert.True(deserializedObject.CallbackActions.Count > 0);
            Assert.True(deserializedObject.MessageActions.Count > 0);
        }

        [Fact]
        public void WhenISaveAFunnelICanRetrieveIt()
        {
            IFunnelStorageService service = new FunnelStorageService(new DapperNoSql<Funnel>(connection));
            service.Initialize();
            var fun = GetFunnel();
            service.InsertOrUpdate(botId, fun);

            var funnel = service.GetById(botId);
            Assert.True(funnel.CallbackActions.Count > 0);
            Assert.True(funnel.MessageActions.Count > 0);
        }

        private Funnel GetFunnel()
        {
            var result = new Funnel();
            List<List<string>> botOptions = new List<List<string>>()
            {
                new List<string>(){ ButtonBuyCourse, ButtonGoBack},
            };
            result.MessageActions.Add(new MessageTrigger(StartMessage), new List<FunnelAction>()
            {
                new MessageButtonAction(WelcomeMessage,botOptions)
            });

            List<List<string>> courseOptions = new List<List<string>>()
            {
                new List<string>(){ButtonBegginer, ButtonIntermidiate},
                new List<string>(){ButtonAdvance, ButtonPro}
            };

            result.CallbackActions.Add(new MessageTrigger(ButtonBuyCourse), new List<FunnelAction>()
            {
                new MessageButtonAction(CourseSelectionMessage, courseOptions)
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonBegginer), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonBegginer}")
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonIntermidiate), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonIntermidiate}")
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonAdvance), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonAdvance}")
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonPro), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonPro}")
            });

            return result;
        }
    }

}
