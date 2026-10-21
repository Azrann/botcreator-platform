using System.Net.Http;
using BraintreeHttp;

namespace Mentor.telegram.paypalpayments.WebhookPatch
{
    class WebhookVerifySignatureRawRequest : HttpRequest
    {
        public WebhookVerifySignatureRawRequest() : base("/v1/notifications/verify-webhook-signature?", HttpMethod.Post, typeof(PayPal.v1.Webhooks.VerifyWebhookSignatureResponse))
        {

            this.ContentType = "custom";
        }

        public WebhookVerifySignatureRawRequest RequestBody(object verifyWebhookSignature)
        {
            this.Body = verifyWebhookSignature;
            return this;
        }
    }
}
