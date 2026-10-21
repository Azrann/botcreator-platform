using System;
using System.Collections.Generic;
using Core.Common.NoSqlDataAccess.DataAccess;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Xunit;

namespace Mentor.telegram.bot.api.test
{
    public class TagStorageServiceTests
    {
        private readonly string connection = @"REPLACE_WITH_YOUR_VALUE";
        private string botId = "TestBotID1";

        [Fact]
        public void WhenIRetrieveTagsOfNotExistingUserIGetEmptyObject()
        {
            ITagStorageService service = new TagStorageService(new DapperNoSql<UserTags>(connection));
            service.Initialize();
            var userTags = service.GetTagsById(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            Assert.True(userTags.Tags.Count == 0);
        }

        [Fact]
        public void WhenIIncrementTagsICanRetrieveIt()
        {
            var userId = Guid.NewGuid().ToString();
            ITagStorageService service = new TagStorageService(new DapperNoSql<UserTags>(connection));
            service.Initialize();

            service.IncrementTag(botId,userId, "tag1");
            service.IncrementTag(botId, userId, "tag1");
            service.IncrementTag(botId, userId, "tag2");
            service.IncrementTag(botId, userId, "tag3");

            var userTags = service.GetTagsById(botId, userId);

            Assert.True(userTags.GetTag("tag1") == 2);
            Assert.True(userTags.GetTag("tag2") == 1);
            Assert.True(userTags.GetTag("tag3") == 1);
        }

        [Fact]
        public void WhenIDecrementTagsICanRetrieveIt()
        {
            var userId = Guid.NewGuid().ToString();
            ITagStorageService service = new TagStorageService(new DapperNoSql<UserTags>(connection));
            service.Initialize();

            service.DecrementTag(botId, userId, "tag1");
            service.DecrementTag(botId, userId, "tag1");
            service.DecrementTag(botId, userId, "tag2");
            service.DecrementTag(botId, userId, "tag3");

            var userTags = service.GetTagsById(botId, userId);

            Assert.True(userTags.GetTag("tag1") == -2);
            Assert.True(userTags.GetTag("tag2") == -1);
            Assert.True(userTags.GetTag("tag3") == -1);
        }

        [Fact]
        public void WhenIIncrementTagsInBatchICanRetrieveIt()
        {
            var userId = Guid.NewGuid().ToString();
            ITagStorageService service = new TagStorageService(new DapperNoSql<UserTags>(connection));
            service.Initialize();

            service.IncrementTags(botId, userId, new List<string>()
            {
                "tag1","tag1","tag2","tag3"
            });

            var userTags = service.GetTagsById(botId, userId);

            Assert.True(userTags.GetTag("tag1") == 2);
            Assert.True(userTags.GetTag("tag2") == 1);
            Assert.True(userTags.GetTag("tag3") == 1);
        }

        [Fact]
        public void WhenIDecrementTagsInBatchICanRetrieveIt()
        {
            var userId = Guid.NewGuid().ToString();
            ITagStorageService service = new TagStorageService(new DapperNoSql<UserTags>(connection));
            service.Initialize();

            service.DecrementTags(botId, userId, new List<string>()
            {
                "tag1","tag1","tag2","tag3"
            });

            var userTags = service.GetTagsById(botId, userId);

            Assert.True(userTags.GetTag("tag1") == -2);
            Assert.True(userTags.GetTag("tag2") == -1);
            Assert.True(userTags.GetTag("tag3") == -1);
        }

        [Fact]
        public void WhenIRemoveTagsInBatchICannotRetrieveThem()
        {
            var userId = Guid.NewGuid().ToString();
            ITagStorageService service = new TagStorageService(new DapperNoSql<UserTags>(connection));
            service.Initialize();

            service.DecrementTags(botId, userId, new List<string>()
            {
                "tag1","tag1","tag2","tag3"
            });

            service.RemoveTags(botId, userId ,new List<string>()
            {
                "tag1","tag3"
            });

            var userTags = service.GetTagsById(botId, userId);
            Assert.True(userTags.GetTag("tag1") == null);
            Assert.True(userTags.GetTag("tag2") == -1);
            Assert.True(userTags.GetTag("tag3") == null);
        }

        [Fact]
        public void WhenIRemoveATagICannotRetrieveThem()
        {
            var userId = Guid.NewGuid().ToString();
            ITagStorageService service = new TagStorageService(new DapperNoSql<UserTags>(connection));
            service.Initialize();

            service.DecrementTags(botId, userId, new List<string>()
            {
                "tag1","tag1","tag2","tag3"
            });

            service.RemoveTag(botId, userId, "tag1");

            var userTags = service.GetTagsById(botId, userId);
            Assert.True(userTags.GetTag("tag1") == null);
            Assert.True(userTags.GetTag("tag2") == -1);
            Assert.True(userTags.GetTag("tag3") == -1);
        }
    }

}
