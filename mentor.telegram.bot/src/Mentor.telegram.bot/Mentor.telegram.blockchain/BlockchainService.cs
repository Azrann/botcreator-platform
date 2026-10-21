using System;
using System.Threading.Tasks;
using Core.Common;
using Mentor.telegram.blockchain.Models;
using Newtonsoft.Json;

namespace Mentor.telegram.blockchain
{
    public class BlockchainService : IBlockchainService
    {
        public Uri BaseUrl { get; set; }
        public Endpoint Endpoint { get; set; }
        public string UserToken { get; set; }

        public BlockchainService(string token = "", Endpoint endpoint = Endpoint.BtcMain)
        {
            UserToken = token;
            Endpoint = endpoint;

            switch (endpoint)
            {
                case Endpoint.BcyTest:
                    BaseUrl = new Uri("https://api.blockcypher.com/v1/bcy/test");
                    break;

                case Endpoint.BtcMain:
                    BaseUrl = new Uri("https://api.blockcypher.com/v1/btc/main");
                    break;

                case Endpoint.BtcTest3:
                    BaseUrl = new Uri("https://api.blockcypher.com/v1/btc/test3");
                    break;

                case Endpoint.LtcMain:
                    BaseUrl = new Uri("https://api.blockcypher.com/v1/ltc/main");
                    break;

                case Endpoint.UroMain:
                    BaseUrl = new Uri("https://api.blockcypher.com/v1/uro/main");
                    break;

                case Endpoint.DogeMain:
                    BaseUrl = new Uri("https://api.blockcypher.com/v1/doge/main");
                    break;
            }
        }
        public async Task<Transaction> GetInfoByTrxId(string txId)
        {
            var result = await HttpClientHelper.GetAsync(BaseUrl.ToString(), $"/txs/{txId}");
            var resultString = await result.Content.ReadAsStringAsync();
            var transaction = JsonConvert.DeserializeObject<Transaction>(resultString);
            return transaction;
        }
    }

    public interface IBlockchainService
    {
        Task<Transaction> GetInfoByTrxId(string txId);
    }
}
