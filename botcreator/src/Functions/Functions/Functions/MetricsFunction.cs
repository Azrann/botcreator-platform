using System.Threading.Tasks;
using Functions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions
{
    public class Metrics
    {
        private readonly IConfigurationRoot _config;

        public Metrics(IConfigurationRoot config)
        {
            _config = config;
        }

        [FunctionName("Metric")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation("Metric Post triggered");

            var botConnectionString = _config.GetValue<string>("BotDbConnection");

            string json = await req.ReadAsStringAsync();
            var metric = JsonConvert.DeserializeObject<WebMetric>(json);

            IWebMetricsService service = new WebMetricsService(botConnectionString);
            service.AddWebMetric(metric);

            return (ActionResult)new OkObjectResult(metric.Id);
        }
    }
}
