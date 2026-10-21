using System;
using Xunit;
using Functions.Services;

namespace Functions.test
{
    public class WebMetricsServiceTest
    {
        private string ConnectionString = "REPLACE_WITH_YOUR_VALUE";

        [Fact]
        public void ICanInsertAndRetrieveData()
        {
            IWebMetricsService service = new WebMetricsService(ConnectionString);

            var metric1 = GetWebMetric();
            service.AddWebMetric(metric1);
            var metricFromDb = service.GetWebMetricById(metric1.Id);

            Assert.True(metricFromDb.UserAgent == metric1.UserAgent);
            Assert.True(metricFromDb.BotId == metric1.BotId);
            Assert.True(metricFromDb.WebUserId == metric1.WebUserId);
            service.RemoveWebMetricById(metric1.Id);
            metricFromDb = service.GetWebMetricById(metric1.Id);

            Assert.Null(metricFromDb);
        }

        private WebMetric GetWebMetric()
        {
            return new WebMetric()
            {
                Id = Guid.NewGuid(),
                WebUserId = Guid.NewGuid(),
                Cookies = "REPLACE_WITH_YOUR_VALUE",
                BrowserLanguage = "undefined",
                Language = "en-GB",
                Platform = "Win32",
                Connection = "undefined",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36",
                Webdriver = "undefined",
                CurrentUrl = "https://botcreatorapptest.azurewebsites.net/api/widgetconfig/",
                BotId = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.MinValue,
                Ip = "86.129.190.4",
                City = "Mitcham",
                Region = "England",
                Country = "GB",
                Loc = "51.4032,-0.1683",
                Postal = "CR4",
                Referrer = "http://facebook.com/pagina1"
            };
        }
    }
}
