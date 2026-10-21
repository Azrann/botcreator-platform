using System;
using Functions.Services;
using Xunit;

namespace Functions.test
{
    public class FeedbackServiceTest
    {
        private string ConnectionString = "REPLACE_WITH_YOUR_VALUE";

        [Fact]
        public void ICanInsertAndRetrieveData()
        {
            IFeedbackService service = new FeedbackService(ConnectionString);

            var metric1 = GetFeedback();
            service.AddFeedback(metric1);
            var metricFromDb = service.GetFeedbackById(metric1.Id);

            Assert.True(metricFromDb.UserAgent == metric1.UserAgent);
            Assert.True(metricFromDb.BotId == metric1.BotId);
            Assert.True(metricFromDb.WebUserId == metric1.WebUserId);
            Assert.True(metricFromDb.Message == metric1.Message);
            Assert.True(metricFromDb.ReplyToId == metric1.ReplyToId);
            service.RemoveFeedbackById(metric1.Id);
            metricFromDb = service.GetFeedbackById(metric1.Id);

            Assert.Null(metricFromDb);
        }

        private Feedback GetFeedback()
        {
            return new Feedback()
            {
                Id = Guid.NewGuid(),
                WebUserId = Guid.NewGuid(),
                Email = "user@example.invalid",
                Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Platform = "Win32",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36",
                CurrentUrl = "https://botcreatorapptest.azurewebsites.net/api/widgetconfig/",
                BotId = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.MinValue,
            };
        }
    }
}
