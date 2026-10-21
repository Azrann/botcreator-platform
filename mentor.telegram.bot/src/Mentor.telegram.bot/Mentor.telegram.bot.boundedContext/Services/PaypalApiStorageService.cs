using Bot.Configuration;
using Mentor.telegram.bot.models.Paypal;
using Core.Common.NoSqlDataAccess.DataAccess;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public interface IPaypalApiStorageService
    {
        void RemovePaypalApiData(string botId);

        PaypalApiData GetPaypalApiDataById(string botId);

        void InsertOrUpdate(string botId, PaypalApiData paypalApiData);

        void Initialize();
    }


    public class PaypalApiStorageService : IPaypalApiStorageService
    {
        private const string PaypalApiDataTable = "PaypalApiTable";

        private readonly IConfigurationCommon _config;
        private readonly INoSqlDb<PaypalApiData> _repository;

        public PaypalApiStorageService(INoSqlDb<PaypalApiData> repository, IConfigurationCommon config)
        {
            _config = config;
            _repository = repository;
        }

        public PaypalApiData GetPaypalApiDataById(string botId)
        {
            return _repository.GetByKey(PaypalApiDataTable, botId);
        }

        public void Initialize()
        {
            _repository.CreateTableIfNotExists(PaypalApiDataTable);

            if (_config.IsDevelopment())
            {
                var botId = "REPLACE_WITH_YOUR_VALUE";
                _repository.AddOrUpdate(PaypalApiDataTable, botId, new PaypalApiData
                {
                    ClientId = "REPLACE_WITH_YOUR_VALUE",
                    SecretId = "REPLACE_WITH_YOUR_VALUE",
                    WebhookId = "2UD852923Y447143C"
                });
            }
            else
            {
                // TODO: COnfigure production data
            }
        }

        public void InsertOrUpdate(string botId, PaypalApiData paypalApiData)
        {
            _repository.AddOrUpdate(PaypalApiDataTable, botId, paypalApiData);
        }

        public void RemovePaypalApiData(string botId)
        {
            _repository.Delete(PaypalApiDataTable, botId);
        }
    }
}
