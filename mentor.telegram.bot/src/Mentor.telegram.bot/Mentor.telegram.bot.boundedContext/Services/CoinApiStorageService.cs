using Core.Common.NoSqlDataAccess.DataAccess;
using Mentor.telegram.bot.models;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public interface ICoinApiStorageService
    {
        void RemoveCoinApiData(string botId);
        CoinApiData GetCoinApiDataById(string botId);
        void InsertOrUpdate(string botId, CoinApiData coinApiData);
        void Initialize();
    }

    public class CoinApiStorageService : ICoinApiStorageService
    {
        private const string CoinApiDataTable = "CoinApiTable";
        private readonly INoSqlDb<CoinApiData> _repository;

        public CoinApiStorageService(INoSqlDb<CoinApiData> repository)
        {
            _repository = repository;
        }

        public void RemoveCoinApiData(string botId)
        {
            _repository.Delete(CoinApiDataTable, botId);
        }

        public CoinApiData GetCoinApiDataById(string botId)
        {
            return _repository.GetByKey(CoinApiDataTable, botId);
        }

        public void InsertOrUpdate(string botId, CoinApiData coinApiData)
        {
            _repository.AddOrUpdate(CoinApiDataTable, botId, coinApiData);
        }

        public void Initialize()
        {
            _repository.CreateTableIfNotExists(CoinApiDataTable);

            _repository.AddOrUpdate(CoinApiDataTable, "REPLACE_WITH_YOUR_VALUE",
                new CoinApiData()
                {
                    PublicKey = "REPLACE_WITH_YOUR_VALUE",
                    PrivateKey = "REPLACE_WITH_YOUR_VALUE",
                    IpnSecret = "REPLACE_WITH_YOUR_VALUE"
                });

            _repository.AddOrUpdate(CoinApiDataTable, "REPLACE_WITH_YOUR_VALUE",
                new CoinApiData()
                {
                    PublicKey = "REPLACE_WITH_YOUR_VALUE",
                    PrivateKey = "REPLACE_WITH_YOUR_VALUE",
                    IpnSecret = "REPLACE_WITH_YOUR_VALUE"
                });

            _repository.AddOrUpdate(CoinApiDataTable, "REPLACE_WITH_YOUR_VALUE",
                new CoinApiData()
                {
                    PublicKey = "REPLACE_WITH_YOUR_VALUE",
                    PrivateKey = "REPLACE_WITH_YOUR_VALUE",
                    IpnSecret = "REPLACE_WITH_YOUR_VALUE"
                });

            _repository.AddOrUpdate(CoinApiDataTable, "REPLACE_WITH_YOUR_VALUE",
                new CoinApiData()
                {
                    PublicKey = "REPLACE_WITH_YOUR_VALUE",
                    PrivateKey = "REPLACE_WITH_YOUR_VALUE",
                    IpnSecret = "REPLACE_WITH_YOUR_VALUE"
                });
            /*  _repository.AddOrUpdate(CoinApiDataTable, "REPLACE_WITH_YOUR_VALUE",
                  new CoinApiData()
                  {
                      PublicKey = "REPLACE_WITH_YOUR_VALUE",
                      PrivateKey = "REPLACE_WITH_YOUR_VALUE",
                      IpnSecret = "REPLACE_WITH_YOUR_VALUE"
                  });
              _repository.AddOrUpdate(CoinApiDataTable, "abcabc",
                  new CoinApiData()
                  {
                      PublicKey = "",
                      PrivateKey = "REPLACE_WITH_YOUR_VALUE",
                      IpnSecret = "REPLACE_WITH_YOUR_VALUE"
                  });

*/
        }
    }
}
