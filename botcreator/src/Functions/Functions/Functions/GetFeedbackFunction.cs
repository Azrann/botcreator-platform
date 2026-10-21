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
    public class GetFeedbackFunction
    {
        private readonly IAccessTokenProvider _tokenProvider;
        private readonly IBotService _botService;
        private readonly IFeedbackService _feedbackService;

        public GetFeedbackFunction(IAccessTokenProvider tokenProvider, IBotService botService, IFeedbackService feedbackService)
        {
            _tokenProvider = tokenProvider;
            _botService = botService;
            _feedbackService = feedbackService;
        }

    [FunctionName("GetFeedbackFunction")]
        public HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "feedback/{botId}")] HttpRequest req,
            ILogger log, string botId,
            ExecutionContext context)
        {
            log.LogInformation("Feedback Post triggered");

            var result = _tokenProvider.ValidateToken(req);

            if ((result.Status == AccessTokenStatus.Valid) && (_botService.IsMyBot(result.TokenInfo.UserId, botId)))
            {
                    var feedbackList = _feedbackService.GetFeedbackByBotId(botId);
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(feedbackList), Encoding.UTF8, "application/json")
                    };
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}
