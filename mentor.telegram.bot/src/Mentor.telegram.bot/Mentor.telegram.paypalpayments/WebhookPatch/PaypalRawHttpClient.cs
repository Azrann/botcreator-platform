using PayPal.Core;

namespace Mentor.telegram.paypalpayments.WebhookPatch
{
    public class PaypalRawHttpClient : PayPalHttpClient
    {
        public PaypalRawHttpClient(PayPalEnvironment environment) : base(environment)
        {
            Encoder.RegisterSerializer(new RawStreamTransferSerializer());
        }
    }
}
