using System.Collections.Generic;
using Core.Common.NoSqlDataAccess.DataAccess;
using Mentor.telegram.bot.models;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public class StateStorageService : IStateStorageService
    {
        private const string UserStateTable = "UserStateTable";
        private readonly INoSqlDb<UserStates> _repository;

        public StateStorageService(INoSqlDb<UserStates> repository)
        {
            _repository = repository;
        }


        public bool RemoveStates(string botId, string userId, IEnumerable<string> StateToRemove)
        {
            var userState = _repository.GetByKey(UserStateTable, GetKey(botId, userId));

            foreach (var State in StateToRemove)
            {
                userState.States.Remove(State);
            }

            return InsertOrUpdate(botId, userId, userState);
        }

        public UserStates GetStateById(string botId, string userId)
        {
            var userState = _repository.GetByKey(UserStateTable, GetKey(botId, userId));

            if (userState != null)
            {
                return userState;
            }

            var result = new UserStates()
            {
                States = new Dictionary<string, string>()
            };
            
            return result;
        }

        private static string GetKey(string botId, string userId)
        {
            return $"{botId}{userId}";
        }

        public bool InsertOrUpdate(string botId, string userId, UserStates userStates)
        {
            _repository.AddOrUpdate(UserStateTable, GetKey(botId, userId), userStates);
            return true;
        }

        public void Initialize()
        {
            _repository.CreateTableIfNotExists(UserStateTable);
        }

        public bool AddOrUpdateState(string botId, string userId, string state, string value)
        {
            UserStates userStates = GetStateById(botId, userId);

            if (userStates.States.ContainsKey(state))
            {
                userStates.States[state] = value;
            }
            else
            {
                userStates.States.Add(state, value);
            }

            return InsertOrUpdate(botId, userId, userStates);
        }

        public bool RemoveState(string botId, string userId, string State)
        {
            var userState = _repository.GetByKey(UserStateTable, GetKey(botId, userId));
            userState.States.Remove(State);

            return InsertOrUpdate(botId, userId, userState);
        }
    }

    public interface IStateStorageService
    {
        void Initialize();
        bool AddOrUpdateState(string botId, string userId, string state, string value);
        bool RemoveState(string botId, string userId, string State);
        bool RemoveStates(string botId, string userId, IEnumerable<string> StateToRemove);
        UserStates GetStateById(string botId, string userId);
        bool InsertOrUpdate(string botId, string userId, UserStates userStates);
    }
}
