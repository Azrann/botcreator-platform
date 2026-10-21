using Core.Common.NoSqlDataAccess.DataAccess;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public enum ExchangeName
    {
        Binance
    }

    public class ExchangeApiData
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }

    public interface IExchangeApiStorageService
    {
        void RemoveExchangeApiData(string userId, ExchangeName exchangeName);
        ExchangeApiData GetExchangeApiDataById(string userId, ExchangeName exchangeName);
        void InsertOrUpdate(string userId, ExchangeName exchangeName, ExchangeApiData ExchangeApiData);
        void Initialize();
    }

    public class ExchangeApiStorageService : IExchangeApiStorageService
    {
        private const string ExchangeApiDataTable = "ExchangeApiTable";
        private readonly INoSqlDb<ExchangeApiData> _repository;

        public ExchangeApiStorageService(INoSqlDb<ExchangeApiData> repository)
        {
            _repository = repository;
        }

        public void RemoveExchangeApiData(string userId, ExchangeName exchangeName)
        {
            _repository.Delete(ExchangeApiDataTable, GetKey(userId,exchangeName));
        }

        public ExchangeApiData GetExchangeApiDataById(string userId, ExchangeName exchangeName)
        {
            return _repository.GetByKey(ExchangeApiDataTable, GetKey(userId, exchangeName));
        }

        public void InsertOrUpdate(string userId, ExchangeName exchangeName, ExchangeApiData ExchangeApiData)
        {
            _repository.AddOrUpdate(ExchangeApiDataTable, GetKey(userId, exchangeName), ExchangeApiData);
        }

        private string GetKey(string userId, ExchangeName exchangeName)
        {
            return $"{userId}{exchangeName.ToString()}";
        }

        public void Initialize()
        {
            _repository.CreateTableIfNotExists(ExchangeApiDataTable);
        }
    }
}
