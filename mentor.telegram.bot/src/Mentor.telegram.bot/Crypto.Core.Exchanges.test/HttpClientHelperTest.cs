using System.Collections.Generic;
using Core.Common;
using Xunit;

namespace Crypto.Core.Exchanges.test
{
    public class HttpClientHelperTest
    {
        [Fact]
        public async void ICanSendHttpPost()
        {
            var botId = "REPLACE_WITH_YOUR_VALUE";
            var parameters = new Dictionary<string, string>
            {
                {"Command", $"CleanUpSubscriptionTest"},
                {"BotId", botId},
            };
            var BaseUrl = "https://verifycryptogroupbotify.azurewebsites.net/api/cron/";
            //var BaseUrl = "http://demo1268368.mockable.io/";
            var body = "BotId=657453239%3AAAEAEIj2qDVeMxOm5FbZglc6q8Ik3uVghkM&Command=CleanUpSubscriptionTest";

            var createSignature = "REPLACE_WITH_YOUR_VALUE";

            var result2 = await HttpClientHelper.PostAsyncHmac(body, BaseUrl, "REPLACE_WITH_YOUR_VALUE", createSignature);
            var str = await result2.Content.ReadAsStringAsync();
        }
    }
}
