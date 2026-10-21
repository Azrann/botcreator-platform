#region

using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Mentor.telegram.blockchain.Models {
    public class TxInput {
        [JsonProperty("addresses")]
        public IList<string> Addresses { get; set; }
    }
}
