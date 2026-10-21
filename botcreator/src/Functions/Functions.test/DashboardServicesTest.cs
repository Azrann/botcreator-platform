using System;
using Functions.Services;
using Xunit;

namespace Functions.test
{
    public class DashboardServiceTest
    {
        private string Connection =
            "REPLACE_WITH_YOUR_VALUE";

        private string botId = "5bc2d86d-dbb2-48bd-b95b-4e276561103c";

        [Fact]
        public void ICanRetrieveDashboard()
        {
            DashboardService service = new DashboardService(Connection);
            var result = service.GetWidgetDashboard(botId,30);
            Assert.True(result.Visits > 8);
            Assert.True(result.BotOpenings > 9);
            Assert.True(result.Conversions > 2);
            Assert.True(result.Conversations.Count > 3);
            Assert.True(result.Conversations[0].LastVisit != DateTime.MinValue);
        }
    }
}
