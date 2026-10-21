using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mentor.telegram.paypalpayments.Models
{
    public class VerifyWebhookSignatureRaw
    {
        [JsonProperty(PropertyName = "auth_algo")]
        public string AuthAlgo { get; set; }

        [JsonProperty(PropertyName = "cert_url")]
        public string CertUrl { get; set; }

        [JsonProperty(PropertyName = "transmission_id")]
        public string TransmissionId { get; set; }

        [JsonProperty(PropertyName = "transmission_sig")]
        public string TransmissionSig { get; set; }

        [JsonProperty(PropertyName = "transmission_time")]
        public string TransmissionTime { get; set; }

        [JsonProperty(PropertyName = "webhook_event")]
        public JRaw WebhookEvent { get; set; }

        [JsonProperty(PropertyName = "webhook_id")]
        public string WebhookId { get; set; }
    }
}
