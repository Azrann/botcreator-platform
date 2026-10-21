using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Functions.Functions
{
    public static class Status
    {
        [FunctionName("Status")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Status executed.");

            return (ActionResult) new OkResult();
        }
    }
}

