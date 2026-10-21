using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Functions.Functions
{
    public class ExampleHttpFunction
    {
        private readonly IConfigurationRoot _config;

        public ExampleHttpFunction(IConfigurationRoot config)
        {
            _config = config;
        }

        [FunctionName("ExampleHttpFunction")]
        public HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "example")] HttpRequest req, ILogger log)
        {
            string connection =_config.GetValue<string>("BotDbConnection");
            return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent($"BotDBConnection: {connection} OK", Encoding.UTF8, "application/json")
                };
        }
    }
}
