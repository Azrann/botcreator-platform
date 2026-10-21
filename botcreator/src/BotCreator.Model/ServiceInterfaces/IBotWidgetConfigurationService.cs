using BotCreator.Core.DomainModels.Bot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCreator.Core.ServiceInterfaces
{
    public interface IBotWidgetConfigurationService
    {
        Task<List<BotTemplateModel>> GetTemplatesAsync();

        Task<List<UserBotModel>> GetUserBotsAsync(int userId);

        Task<bool> SaveCustomerServiceBot(CustomerServiceBotModel customerServiceBot, int userId);
    }
}
