using System.Net;
using System.Net.Http;
using System.Text;
using Functions.Services;
using Functions.Token;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Functions.Functions
{
    public class StatsFunction
    {
        private readonly IAccessTokenProvider _tokenProvider;
        private readonly IBotService _botService;
        private readonly IWebMetricsService _webMetricsService;

        public StatsFunction(IAccessTokenProvider tokenProvider, IBotService botService, IWebMetricsService webMetricsService)
        {
            _tokenProvider = tokenProvider;
            _botService = botService;
            _webMetricsService = webMetricsService;
        }

        [FunctionName("Stats")]
        public HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stats/{botId}")] HttpRequest req,
            ILogger log, string botId, ExecutionContext context)
        {
            log.LogInformation("Stats Get triggered");

            var result = _tokenProvider.ValidateToken(req);

            if ((result.Status == AccessTokenStatus.Valid) && (_botService.IsMyBot(result.TokenInfo.UserId, botId)))
            {
                var metricsList = _webMetricsService.GetWebMetricsByBotId(botId);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(metricsList), Encoding.UTF8, "application/json")
                };
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}
