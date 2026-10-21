using AutoMapper;
using BotCreator.Core.Configs;
using BotCreator.Core.DomainModels.Bot;
using BotCreator.Core.ServiceInterfaces;
using BotCreator.Infrastructure.Data.EfCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mentor.telegram.bot.sdk;
using Mentor.telegram.shared.BotTemplates;
using BotCreator.Core.Common.Exceptions;
using BotCreator.Core.DomainModels.Bot.CustomerService;
using Microsoft.Extensions.Logging;
using BotCreator.Core.Common.Helpers;
using System.IO;
using BotCreator.Core.Common.Interfaces;
using Mentor.telegram.bot.sdk.Dtos;

namespace BotCreator.Service.Bot
{
    public class BotService : IBotService
    {
        private BotManagerContext _context;
        private IMapper _mapper;
        private AppSettings _settings;
        private IEmailProvider _email;
        private ILogger<BotService> _log;
        private IPathProvider _pathProvier;

        public BotService(BotManagerContext context, IMapper mapper, IOptions<AppSettings> appSettings,
            IEmailProvider email, ILogger<BotService> log, IPathProvider pathProvier)
        {
            _pathProvier = pathProvier;
            _settings = appSettings.Value;
            _context = context;
            _mapper = mapper;
            _email = email;
            _log = log;
        }

        public async Task DeployBot(int botId, int userId)
        {
            var bot = await _context.Bots.FirstOrDefaultAsync(s => s.Id == botId);

            if (bot == null)
                return;

            _log.LogInfo($"Deploying bot: {bot.ExternalId}", userId);
            BotApi api = new BotApi();
            api.DeployBot(bot.ExternalId);

            bot.IsDeployed = true;

            await _context.SaveChangesAsync();
            return;
        }

        public async Task<List<BotTemplateModel>> GetTemplatesAsync()
        {
            var templates = await _context.BotTemplates.ToListAsync();
            return templates.Select(s => _mapper.Map<BotTemplateModel>(s)).ToList();
        }

        private string SaveCustomerBotInSdk(CustomerServiceBotModel customerServiceBot, int userId)
        {
            BotApi api = new BotApi();

            var botId = string.IsNullOrEmpty(customerServiceBot.ExternalId)
                ? Guid.NewGuid().ToString()
                : customerServiceBot.ExternalId;

            _log.LogInfo($"Saving Customer service botId: {botId}", userId);

            Dictionary<string, string> menuBot = new Dictionary<string, string>
            {
                {customerServiceBot.ButtonText1, customerServiceBot.ResultText1},
                {customerServiceBot.ButtonText2, customerServiceBot.ResultText2},
                {customerServiceBot.ButtonText3, customerServiceBot.ResultText3}
            };
            IBotDefinitionDto aBotDefinition = new WidgetSupportBotDto()
            {
                ChatOnWeb = customerServiceBot.ChatOnWeb,
                FbChecked = customerServiceBot.FbChecked,
                FbUserName = customerServiceBot.FbUserName,
//                TalkToMeChecked = customerServiceBot.TalkToMeChecked,
                TalkToAgent = customerServiceBot.TalkToAgent,
                WhatsappChecked = customerServiceBot.WhatsappChecked,
                WhatsappNumber = customerServiceBot.WhatsappNumber,
                CallMeChecked = customerServiceBot.CallMeChecked,
                CallMeEmail = customerServiceBot.CallMeEmail,
                CallMeButtonText = customerServiceBot.CallMeButtonText ?? "Call me",
                MenuBot = menuBot,
                ConnectingToAgentMessage = "Connecting to first available agent."
            };

            api.CreateBot(botId, aBotDefinition);

            return botId;
        }

        private async Task AssertIfCustomerServiceBotExists(int userId)
        {
            var userBot = await _context.Bots
                .FirstOrDefaultAsync(s => s.BotTemplateId == (int)BotTemplateEnum.CustomerServiceTemplate
                            && s.OwnerId == userId);

            if (userBot != null)
            {
                throw new BotBaseException("Only one customer service is allowed");
            }
        }

        public async Task<int> CreateCustomerServiceBot(CustomerServiceBotModel customerServiceBot, int userId)
        {
            await AssertIfCustomerServiceBotExists(userId);
            var botId = SaveCustomerBotInSdk(customerServiceBot, userId);
            var bot = new Infrastructure.Data.EfCore.Bot
            {
                Name = customerServiceBot.Name,
                BotTemplateId = (int)BotTemplateEnum.CustomerServiceTemplate,
                ExternalId = botId,
                OwnerId = userId
            };

            _context.Bots.Add(bot);

            await _context.SaveChangesAsync();
            return bot.Id;
        }

        public async Task<bool> UpdateCustomerServiceBot(CustomerServiceBotModel customerServiceBot, int userId)
        {
            var bot = await _context.Bots.FirstOrDefaultAsync(s => s.Id == customerServiceBot.Id && s.OwnerId == userId);

            if (bot == null)
                return false;

            customerServiceBot.ExternalId = bot.ExternalId;
            SaveCustomerBotInSdk(customerServiceBot, userId);

            return true;
        }

        public object GetBotDefinitionById(string botDefinitionId, int userId)
        {
            BotApi api = new BotApi();
            _log.LogInfo($"GetBotDefinitionById service botId: {botDefinitionId}", userId);

            return api.GetBotDefinition(botDefinitionId);
        }

        public async Task<BotModel> GetBotById(int botId)
        {
            var bot = await _context.Bots.FirstOrDefaultAsync(s => s.Id == botId);
            return _mapper.Map<BotModel>(bot);
        }

        public async Task<string> GetBotEmbeddedHtml(int botId, bool isDev)
        {
            var filename = isDev ? @"Assets\EmbeddedHtmlTest.txt" : @"Assets\EmbeddedHtml.txt";
            var path = Path.Combine(_pathProvier.ExecutableRootPath, filename);
            var bot = await _context.Bots.FirstOrDefaultAsync(s => s.Id == botId);

            var fileContent = await File.ReadAllTextAsync(path);
            var result = fileContent.Replace("{botId}", bot.ExternalId);

            return result;
        }

        public async Task<List<UserBotModel>> GetUserBotsAsync(int userId)
        {
            var bots = await _context.Bots.Include(b => b.BotTemplate)
                                     .Where(b => b.OwnerId == userId).ToListAsync();

            return bots.Select(s => new UserBotModel
            {
                Id = s.Id,
                Name = s.Name,
                TemplateName = s.BotTemplate.Name,
                IsDeployed = s.IsDeployed
            }).ToList();
        }

        public async Task<List<string>> GetBotsIdByUserId(int userId)
        {
            var bots = await _context.Bots.Where(a => a.OwnerId == userId).ToListAsync();
            return bots.Select(s => s.ExternalId).ToList();
        }

        public async Task<CsBotInboundChannelModel> GetInboundChannelForServiceBot(int userId)
        {
            var botId = await GetCustomerServiceBotId(userId);

            BotApi api = new BotApi();
            var authKey = api.AgentAuth(botId);

            _log.LogInfo($"Get inbound channel, authKey: {authKey}, channel: {botId}", userId);
            return new CsBotInboundChannelModel
            {
                AuthKey = authKey,
                Channel = botId
            };
        }

        public async Task<CsWaitingUserConnectionModel> ConnectToWaitingUser(CsConnectToWaitingUserModel connDetails, int userId)
        {
            var botId = await GetCustomerServiceBotId(userId);
            if (string.IsNullOrWhiteSpace(botId))
            {
                throw new BotBaseException("No Customer Service bot found");
            }
            BotApi api = new BotApi();

            var connectClients = await api.Connect(new Mentor.telegram.bot.models.Agent
            {
                AuthKey = connDetails.AuthKey,
                BotId = botId
            });

            if (connectClients == null)
            {
                _log.LogInfo($"Connect to waiting users, no waiting users", userId);
                return null;
            }

            _log.LogInfo($"Connect to waiting user returns with channel: {connectClients.ChannelId}, userId: {connectClients.UserId.ToString()}", userId);
            return new CsWaitingUserConnectionModel
            {
                Channel = connectClients.ChannelId,
                UserId = connectClients.UserId.ToString()
            };
        }

        private async Task<string> GetCustomerServiceBotId(int userId)
        {
            var findBot = await _context.Bots
              .Where(u => u.OwnerId == userId &&
                     u.BotTemplateId == (int)BotTemplateEnum.CustomerServiceTemplate &&
                     u.ExternalId != null)
              .Select(s => s).FirstOrDefaultAsync();

            if (findBot == null)
                throw new BotBaseException("No ServiceBot was found");

            return findBot.ExternalId;
        }

        public async Task RemoveBot(int botId, int userId)
        {
            var bot = await _context.Bots.FirstOrDefaultAsync(s => s.Id == botId && s.OwnerId == userId);
            if (bot == null)
                return;

            var externalId = bot.ExternalId;

            _context.Bots.Remove(bot);

            await _context.SaveChangesAsync();

            //TODO Remove bot using SDK Remove botDefinition and Funnel if deployed.
        }
    }
}
