using BotCreator.Core.DomainModels.Bot;
using BotCreator.Core.DomainModels.Bot.CustomerService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCreator.Core.ServiceInterfaces
{
    public interface IBotService
    {
        Task<List<BotTemplateModel>> GetTemplatesAsync();

        Task<List<UserBotModel>> GetUserBotsAsync(int userId);

        Task<List<string>> GetBotsIdByUserId(int userId);

        // TODO that returns IBotDefinition which comes from SDK does it makes sense have the reference to SDK into the core project?
        object GetBotDefinitionById(string botId, int userId);

        Task<int> CreateCustomerServiceBot(CustomerServiceBotModel customerServiceBot, int userId);

        Task<bool> UpdateCustomerServiceBot(CustomerServiceBotModel customerServiceBot, int userId);

        Task<CsBotInboundChannelModel> GetInboundChannelForServiceBot(int userId);

        Task<CsWaitingUserConnectionModel> ConnectToWaitingUser(CsConnectToWaitingUserModel connDetails, int userId);

        Task RemoveBot(int botId, int userId);

        Task DeployBot(int botId, int userId);

        Task<BotModel> GetBotById(int botId);

        Task<string> GetBotEmbeddedHtml(int botId, bool isDev);
    }
}
