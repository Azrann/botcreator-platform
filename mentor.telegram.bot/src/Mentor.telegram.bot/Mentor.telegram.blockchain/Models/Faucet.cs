#region

using Newtonsoft.Json;

#endregion

namespace Mentor.telegram.blockchain.Models {
    public class Faucet : BaseObject {
        [JsonProperty("tx_ref")]
        public string TxReference { get; set; }
    }
}
