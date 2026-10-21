using System.Threading.Tasks;
using Mentor.telegram.blockchain.Models;
using Xunit;

namespace Mentor.telegram.blockchain.test
{
    public class BlockchainServiceTest
    {
        [Fact]
        public async Task WhenIGetATransactionIRetrieveAResponseVoid()
        {
            IBlockchainService service = new BlockchainService("", Endpoint.BtcMain);
            var result = await service.GetInfoByTrxId("472bcfbcccdd8e29c1bb0c8a28abdcb0d107523a3be67eb65795de26674400e4");
        }

        [Fact]
        public async Task WhenIGetLitecoinTransactionIRetrieveAResponseVoid()
        {
            IBlockchainService service = new BlockchainService("", Endpoint.LtcMain);
            await service.GetInfoByTrxId("1d4ff37c18698b9616b55d588511ab49ee2f25101924ebbdc6bccdef09e57c82");
        }
    }
}
