using System;
using System.Threading.Tasks;
using Functions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public class PubnubFunction
    {
        private readonly PubnubService _pubnubService;

        public PubnubFunction(PubnubService pubnubService)
        {
            _pubnubService = pubnubService;
        }

        [FunctionName("PubnubRestart")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation("PubnubRestart Get triggered");

            try
            {
                var token = _pubnubService.Login("user@example.invalid", "%Qw5od%Ew%8h");
                var result1 = _pubnubService.Stop("540819", "44763", token);
                var result2 = _pubnubService.Start("540819", "44763", token);
                var result3 = _pubnubService.Stop("720538", "52926", token);
                var result4 = _pubnubService.Start("720538", "52926", token);
            }
            catch (Exception ex)
            {
                log.LogError(ex,"PubnubRestart Get error");
            }

            return (ActionResult)new OkResult();
        }
    }
}
