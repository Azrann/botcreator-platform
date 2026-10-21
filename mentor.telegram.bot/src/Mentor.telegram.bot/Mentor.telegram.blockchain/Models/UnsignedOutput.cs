#region

using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Mentor.telegram.blockchain.Models {
    public class UnsignedOutput {
        [JsonProperty("addresses")]
        public IList<string> Addresses { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("script_type")]
        public string ScriptType { get; set; }

        [JsonProperty("spent_by")]
        public string SpentBy { get; set; }

        [JsonProperty("value")]
        public Satoshi Value { get; set; }
    }
}
