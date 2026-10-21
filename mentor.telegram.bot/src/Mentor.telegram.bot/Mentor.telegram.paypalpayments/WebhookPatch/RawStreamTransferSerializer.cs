using BraintreeHttp;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Mentor.telegram.paypalpayments.WebhookPatch
{
    public class RawStreamTransferSerializer : ISerializer
    {
        public RawStreamTransferSerializer()
        {
        }
        public object Decode(HttpContent content, Type responseType)
        {
            return new BraintreeHttp.JsonSerializer().Decode(content, responseType);
        }

        public HttpContent Encode(HttpRequest request)
        {
            var json = JsonConvert.SerializeObject(request.Body, Formatting.Indented);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public string GetContentTypeRegexPattern() => "custom";
    }
}
