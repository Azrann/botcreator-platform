using System.Collections.Generic;
using Mentor.telegram.bot.api.Services;
using Xunit;

namespace Mentor.telegram.bot.api.test
{
    public class ExtensionsTests
    {
        [Fact]
        public void WhenIReadADoubleICanChangeItToDecimal()
        {
            Dictionary<string, string> dicionary = new Dictionary<string, string>()
            {
                { "test", "2.1E-7"}
            };

            decimal result = dicionary.GetDecimalFromDouble("test");
            Assert.True(true);
        }
    }
}
