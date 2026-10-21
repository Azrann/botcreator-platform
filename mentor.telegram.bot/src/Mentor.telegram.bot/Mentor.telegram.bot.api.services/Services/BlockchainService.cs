using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;

namespace Mentor.telegram.bot.api.Services
{
    public class BlockchainService : IBlockchainService
    {
        public bool CheckTransactionId(string transactionIdString, decimal amount, string destinationAddress)
        {
            QBitNinjaClient client = new QBitNinjaClient(Network.Main);
            if (transactionIdString.Length == 64)
            {
                var transactionId = uint256.Parse(transactionIdString);
                GetTransactionResponse transactionResponse = client.GetTransaction(transactionId).Result;

                if (transactionResponse != null)
                {
                    foreach (var receivedCoin in transactionResponse.ReceivedCoins)
                    {
                        if (((Money) receivedCoin.Amount).ToDecimal(MoneyUnit.BTC) == amount)
                        {
                            var paymentScript = receivedCoin.TxOut.ScriptPubKey;
                            var address = paymentScript.GetDestinationAddress(Network.Main);
                            if (address.ToString() == destinationAddress)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool CheckTransactionIdDemo(string transactionIdString, decimal amount, string destinationAddress)
        {
            if (transactionIdString.Length > 9)
            {
                return true;
            }

            return false;
        }
    }

    public interface IBlockchainService
    {
        bool CheckTransactionId(string transactionIdString, decimal amount, string destinationAddress);
        bool CheckTransactionIdDemo(string transactionIdString, decimal amount, string destinationAddress);
    }
}
