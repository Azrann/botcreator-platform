using System.Collections.Generic;
using Core.Common.NoSqlDataAccess;
using CryptoCore.Model;
using Newtonsoft.Json;
using Xunit;

namespace Crypto.Core.Exchanges.test
{
    public class SecurityHelperTest
    {
        [Fact]
        public void ICanEncryptAndDecrypt()
        {
            var result = SecurityHelper.EncryptString("\"Rules\":{\"test\":{\"Mi");
            var original = SecurityHelper.DecryptString(result);
        }

        [Fact]
        public void ICanEncryptAndDecryptJson()
        {
            TickerRules rules = new TickerRules()
            {
                Rules = new Dictionary<string, Rule>()
                {
                    {"test", new Rule(32,2)},
                    {"tes22t", new Rule(12,2)}
                }
            };
            var result = SecurityHelper.EncryptString(JsonConvert.SerializeObject(rules));
            var jsonString = SecurityHelper.DecryptString(result);
            JsonConvert.DeserializeObject<TickerRules>(jsonString);
        }
    }
}
