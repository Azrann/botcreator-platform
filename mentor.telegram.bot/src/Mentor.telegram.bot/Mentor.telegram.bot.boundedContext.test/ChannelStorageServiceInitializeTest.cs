using Xunit;
using channel.telegram.bot.boundedContext.Services;
using Core.Common.NoSqlDataAccess.DataAccess;

namespace Mentor.telegram.bot.boundedContext.test
{
    public class ChannelStorageServiceInitializeTest
    {
        [Fact]
        public void WhenIUseChannelStorageServerIcanInitalize()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";

            IChannelStorageService service = new ChannelStorageService(new DapperNoSql<TelegramChannelData>(connection));
            service.Initialize();

        }
    }
}
