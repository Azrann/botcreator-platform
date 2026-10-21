using System.Linq;
using Mentor.telegram.bot.boundedContext.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Mentor.telegram.bot.boundedContext.test.Services
{
    public class InitializationServiceTest
    {
        [Fact]
        public void WhenICreateADbAndInitializeIGetNoExceptions()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";
            var botConnection = @"REPLACE_WITH_YOUR_VALUE";

            DbContextOptionsBuilder<MentorDbContext> builder = new DbContextOptionsBuilder<MentorDbContext>();
            builder.UseSqlServer(connection);

            DbContextOptionsBuilder<BotDbContext> botBuilder = new DbContextOptionsBuilder<BotDbContext>();
            botBuilder.UseSqlServer(botConnection);

            var dbContext = new MentorDbContext(builder.Options);
            var botDbContext = new BotDbContext(botBuilder.Options);
            var result  = dbContext.PaymentInfos.Select(x => x.Username == "");
            IInitializacionService service = new InitializationService(dbContext);
            service.DatabaseEnsureCreated();
            service.InitializeStoreProcs();
            botDbContext.Dispose();
            dbContext.Dispose();
        }
    }
}
