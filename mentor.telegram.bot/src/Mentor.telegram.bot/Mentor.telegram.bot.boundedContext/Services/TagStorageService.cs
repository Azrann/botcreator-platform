using System.Collections.Generic;
using Core.Common.NoSqlDataAccess.DataAccess;
using Mentor.telegram.bot.models;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public class TagStorageService : ITagStorageService
    {
        private const string UserTagsTable = "UserTagsTable";
        private readonly INoSqlDb<UserTags> _repository;

        public TagStorageService(INoSqlDb<UserTags> repository)
        {
            _repository = repository;
        }


        public bool RemoveTags(string botId, string userId, IEnumerable<string> tagsToRemove)
        {
            var userTags = _repository.GetByKey(UserTagsTable, GetKey(botId, userId));

            foreach (var tag in tagsToRemove)
            {
                userTags.Tags.Remove(tag);
            }

            return InsertOrUpdate(botId, userId, userTags);
        }

        public UserTags GetTagsById(string botId, string userId)
        {
            var userTags = _repository.GetByKey(UserTagsTable, GetKey(botId, userId));

            if (userTags != null)
            {
                return userTags;
            }

            var result = new UserTags()
            {
                Tags = new Dictionary<string, int>()
            };
            
            return result;
        }

        private static string GetKey(string botId, string userId)
        {
            return $"{botId}{userId}";
        }

        public bool InsertOrUpdate(string botId, string userId, UserTags UserTags)
        {
            _repository.AddOrUpdate(UserTagsTable, GetKey(botId, userId), UserTags);
            return true;
        }

        public void Initialize()
        {
            _repository.CreateTableIfNotExists(UserTagsTable);
        }

        public bool IncrementTag(string botId, string userId, string tag, int incrementValue = 1)
        {
            UserTags tags = GetTagsById(botId, userId);

            if (tags.Tags.ContainsKey(tag))
            {
                tags.Tags[tag] = tags.Tags[tag] + incrementValue;
            }
            else
            {
                tags.Tags.Add(tag,incrementValue);
            }

            return InsertOrUpdate(botId, userId, tags);
        }

        public bool IncrementTags(string botId, string userId, IEnumerable<string> userTags, int incrementValue = 1)
        {
            UserTags tags = GetTagsById(botId, userId);

            foreach (var tag in userTags)
            {
                if (tags.Tags.ContainsKey(tag))
                {
                    tags.Tags[tag] = tags.Tags[tag] + incrementValue;
                }
                else
                {
                    tags.Tags.Add(tag, incrementValue);
                }
            }

            return InsertOrUpdate(botId, userId, tags);
        }

        public bool DecrementTag(string botId, string userId, string tag, int decrementValue = 1)
        {
            UserTags tags = GetTagsById(botId, userId);

            if (tags.Tags.ContainsKey(tag))
            {
                tags.Tags[tag] = tags.Tags[tag] - decrementValue;
            }
            else
            {
                tags.Tags.Add(tag, decrementValue*-1);
            }

            return InsertOrUpdate(botId, userId, tags);
        }

        public bool DecrementTags(string botId, string userId, IEnumerable<string> userTags, int decrementValue = 1)
        {
            UserTags tags = GetTagsById(botId, userId);

            foreach (var tag in userTags)
            {
                if (tags.Tags.ContainsKey(tag))
                {
                    tags.Tags[tag] = tags.Tags[tag] - decrementValue;
                }
                else
                {
                    tags.Tags.Add(tag, decrementValue * -1);
                }
            }

            return InsertOrUpdate(botId, userId, tags);
        }

        public bool RemoveTag(string botId, string userId, string tag)
        {
            var userTags = _repository.GetByKey(UserTagsTable, GetKey(botId, userId));
            userTags.Tags.Remove(tag);

            return InsertOrUpdate(botId, userId, userTags);
        }
    }

    public interface ITagStorageService
    {
        void Initialize();
        bool IncrementTag(string botId, string userId, string tag, int incrementValue = 1);
        bool DecrementTag(string botId, string userId, string tag, int decrementValue = 1);
        bool IncrementTags(string botId, string userId, IEnumerable<string> tag, int incrementValue = 1);
        bool DecrementTags(string botId, string userId, IEnumerable<string> userTags, int decrementValue = 1);
        bool RemoveTag(string botId, string userId, string tag);
        bool RemoveTags(string botId, string userId, IEnumerable<string> tagsToRemove);
        UserTags GetTagsById(string botId, string userId);
        bool InsertOrUpdate(string botId, string userId, UserTags userTags);
    }
}
