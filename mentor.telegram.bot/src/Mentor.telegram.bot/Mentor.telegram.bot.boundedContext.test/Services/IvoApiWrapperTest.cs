using System;
using System.Linq;
using Mentor.telegram.bot.boundedContext.Services;
using Xunit;

namespace Mentor.telegram.bot.boundedContext.test.Services
{
    public class IvoApiWrapperTest
    {
        [Fact]
        public async void WhenIGetUsersAboutToExpiryICanHandleEmptyString()
        {
            IvoApiWrapper ivoApiWrapper = new IvoApiWrapper();

            var result = await ivoApiWrapper.GetUsersThatExpireInDays(10, "platinum-monthly", Guid.NewGuid());

            Assert.True(result.Count() == 0);
        }
    }
}
