#region

using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Mentor.telegram.blockchain.Models {
    public class TxOutput {
        [JsonProperty("addresses")]
        public IList<string> Addresses { get; set; }

        [JsonProperty("value")]
        public Satoshi Value { get; set; }
    }
}
