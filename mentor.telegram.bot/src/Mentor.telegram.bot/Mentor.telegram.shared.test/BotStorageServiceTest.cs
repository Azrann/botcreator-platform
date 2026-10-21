using System;
using Core.Common.NoSqlDataAccess.DataAccess;
using Mentor.telegram.shared.BotTemplates;
using Mentor.telegram.shared.Services;
using Newtonsoft.Json;
using Xunit;

namespace Mentor.telegram.shared.test
{
    public class BotStorageServiceTest
    {
        private const string ConnectionString = @"REPLACE_WITH_YOUR_VALUE";

        [Fact]
        public void ICanStoreValuesAndRetrieveById()
        {
            IBotDefinitionStorageService botStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            botStorageService.Initialize();

            BasicChatBot basicChatBot = new BasicChatBot()
            {
                ConnectingToAgentMessage = "Connecting to Agent..."
            };
            string botId = Guid.NewGuid().ToString();
            botStorageService.InsertOrUpdate(botId, basicChatBot);

            var result = botStorageService.GetById(botId);

            Assert.NotNull(result);
        }

        [Fact]
        public void ICanStoreValuesAndRetrieveObjectById()
        {
            IBotDefinitionStorageService botStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            botStorageService.Initialize();

            BasicChatBot basicChatBot = new BasicChatBot()
            {
                ConnectingToAgentMessage = "Connecting to Agent..."
            };
            string botId = Guid.NewGuid().ToString();
            botStorageService.InsertOrUpdate(botId, basicChatBot);

            var result = botStorageService.GetDefinitionById(botId);

            Assert.NotNull(result);
        }

        [Fact]
        public void ICanStoreValuesAndRetrieveBasicChatBotById()
        {
            IBotDefinitionStorageService botStorageService = new BotDefinitionStorageService(new DapperNoSql<BotDefinition>(ConnectionString));
            botStorageService.Initialize();

            BasicChatBot basicChatBot = new BasicChatBot()
            {
                ConnectingToAgentMessage = "Connecting to Agent..."
            };
            string botId = Guid.NewGuid().ToString();
            botStorageService.InsertOrUpdate(botId, basicChatBot);

            var result = botStorageService.GetById(botId);

            var bot = JsonConvert.DeserializeObject<BasicChatBot>(result.SerializedDefinition);

            Assert.NotNull(bot);
        }
    }
}//Server=den1.mssql2.gear.host;Database=mentor;User Id=Mentor;Password=REPLACE_WITH_YOUR_VALUE;
