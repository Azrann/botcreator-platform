using System;
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
    public static class AddFeedbackFunction
    {
        [FunctionName("AddFeedbackFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "feedback")] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation("Feedback Post triggered");

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var botConnectionString = config.GetValue<string>("BotDbConnection");

            string json = await req.ReadAsStringAsync();
            var feedback = JsonConvert.DeserializeObject<Feedback>(json);

            IFeedbackService service = new FeedbackService(botConnectionString);
            service.AddFeedback(feedback);

            return (ActionResult)new OkObjectResult(feedback.Id);
        }
    }
}
