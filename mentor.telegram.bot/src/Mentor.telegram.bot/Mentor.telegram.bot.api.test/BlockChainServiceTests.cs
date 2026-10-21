using Mentor.telegram.bot.api.Services;
using Xunit;

namespace Mentor.telegram.bot.api.test
{
    public class BlockChainServiceTests
    {
        [Fact]
        public void WhenIGetATransactionIRetrieveAResponseVoid()
        {
            IBlockchainService service = new BlockchainService();
            var result = service.CheckTransactionId(
                "472bcfbcccdd8e29c1bb0c8a28abdcb0d107523a3be67eb65795de26674400e4", 
                 1.71498002m, "3BMEXpdwmzBvP7gyVcXAchcWyLvJKRz51d");
            Assert.True(result);
        }

        [Fact]
        public void WhenIGetLitecoinTransactionIRetrieveAResponseVoid()
        {
            IBlockchainService service = new BlockchainService();
            var result = service.CheckTransactionId(
                "1d4ff37c18698b9616b55d588511ab49ee2f25101924ebbdc6bccdef09e57c82",
                0.2053m, "LLxat7ox5chjd6aWL86qzxKYCapRGEfKiu");
            Assert.True(result);
        }
        

    }
}
