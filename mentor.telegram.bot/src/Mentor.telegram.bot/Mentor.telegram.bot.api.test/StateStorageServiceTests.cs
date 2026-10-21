using System;
using System.Collections.Generic;
using Core.Common.NoSqlDataAccess.DataAccess;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Xunit;

namespace Mentor.telegram.bot.api.test
{
    public class StateStorageServiceTests
    {
        private readonly string connection = @"REPLACE_WITH_YOUR_VALUE";
        private string botId = "TestBotID1";

        [Fact]
        public void WhenIRetrieveStatesOfNotExistingUserIGetEmptyObject()
        {
            IStateStorageService service = new StateStorageService(new DapperNoSql<UserStates>(connection));
            service.Initialize();
            var userStates = service.GetStateById(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            Assert.True(userStates.States.Count == 0);
        }

        [Fact]
        public void WhenIAddStatesICanRetrieveIt()
        {
            var userId = Guid.NewGuid().ToString();
            IStateStorageService service = new StateStorageService(new DapperNoSql<UserStates>(connection));
            service.Initialize();

            service.AddOrUpdateState(botId, userId, "State1", "value1");
            service.AddOrUpdateState(botId, userId, "State1", "value12");
            service.AddOrUpdateState(botId, userId, "State2", "value2");
            service.AddOrUpdateState(botId, userId, "State3", "value3");

            var userStates = service.GetStateById(botId, userId);

            Assert.True(userStates.GetState("State1") == "value12");
            Assert.True(userStates.GetState("State2") == "value2");
            Assert.True(userStates.GetState("State3") == "value3");
        }

        [Fact]
        public void WhenIRemoveStatesInBatchICannotRetrieveThem()
        {
            var userId = Guid.NewGuid().ToString();
            IStateStorageService service = new StateStorageService(new DapperNoSql<UserStates>(connection));
            service.Initialize();

            service.AddOrUpdateState(botId, userId, "State1", "value1");
            service.AddOrUpdateState(botId, userId, "State1", "value12");
            service.AddOrUpdateState(botId, userId, "State2", "value2");
            service.AddOrUpdateState(botId, userId, "State3", "value3");

            service.RemoveStates(botId, userId ,new List<string>()
            {
                "State1","State3"
            });

            var userStates = service.GetStateById(botId, userId);
            Assert.True(userStates.GetState("State1") == null);
            Assert.True(userStates.GetState("State2") == "value2");
            Assert.True(userStates.GetState("State3") == null);
        }

        [Fact]
        public void WhenIRemoveAStateICannotRetrieveThem()
        {
            var userId = Guid.NewGuid().ToString();
            IStateStorageService service = new StateStorageService(new DapperNoSql<UserStates>(connection));
            service.Initialize();

            service.AddOrUpdateState(botId, userId, "State1", "value1");
            service.AddOrUpdateState(botId, userId, "State1", "value12");
            service.AddOrUpdateState(botId, userId, "State2", "value2");
            service.AddOrUpdateState(botId, userId, "State3", "value3");

            service.RemoveState(botId, userId, "State1");

            var userStates = service.GetStateById(botId, userId);
            Assert.True(userStates.GetState("State1") == null);
            Assert.True(userStates.GetState("State2") == "value2");
            Assert.True(userStates.GetState("State3") == "value3");
        }
    }

}
