using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Mentor.telegram.bot.boundedContext.test
{
    public class MentorDbContextTest
    {
        [Fact]
        public void WhenICreateADbContextIGetNoExceptions()
        {
            var connection = @"REPLACE_WITH_YOUR_VALUE";
            DbContextOptionsBuilder<MentorDbContext> builder = new DbContextOptionsBuilder<MentorDbContext>();
            builder.UseSqlServer(connection);
            var dbContext = new MentorDbContext(builder.Options);
            dbContext.Database.EnsureCreated();
            var result = dbContext.Mentors.FirstOrDefaultAsync(x => true).Result;
        }
    }
}
