using System;
using System.Collections.Generic;
using Mentor.telegram.bot.boundedContext.Services;

namespace Bot.telegram.funnel.Actions
{
    public class ExchangeUpdateActionProcesor : ActionProcessor<ExchangeUpdateAction>
    {
        private readonly IExchangeApiStorageService _apiStorageService;

        public ExchangeUpdateActionProcesor(IExchangeApiStorageService apiStorageService)
        {
            _apiStorageService = apiStorageService;
        }

        public override bool Execute(ExchangeUpdateAction action, Dictionary<string, object> parameters)
        {
            if (parameters.ContainsKey("PublicKey") && parameters.ContainsKey("PrivateKey")
                                                    && parameters.ContainsKey("ExchangeName"))
            {
                _apiStorageService.InsertOrUpdate(parameters.GetUserId().ToString(), Enum.Parse<ExchangeName>(parameters["ExchangeName"].ToString()),
                    new ExchangeApiData()
                    {
                        ApiKey = parameters["PublicKey"].ToString(),
                        ApiSecret = parameters["PrivateKey"].ToString(),
                    });

                return true;
            }

            return false;
        }
    }
}