using System;
using Bot.Configuration;
using Core.Common.NoSqlDataAccess.DataAccess;
using Mentor.telegram.bot.api.Services.Chats;
using Mentor.telegram.bot.boundedContext.Services;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Triggers;

namespace Mentor.telegram.bot.api.Services
{
    public class FunnelInitializationService : IFunnelInitializationService
    {
        public const string FunnelsTable = "Funnels";
        public const string BotsTable = "Bots";

        private readonly INoSqlDb<Funnel> _funnelRepository;
        private readonly INoSqlDb<Bot.telegram.funnel.Bot> _botRepository;
        private readonly IAdminStorageService _adminStorageService;
        private readonly IConfigurationCommon _configurationCommon;

        public FunnelInitializationService(INoSqlDb<Funnel> funnelRepository, INoSqlDb<Bot.telegram.funnel.Bot> botRepository,
            IAdminStorageService adminStorageService, IConfigurationCommon configurationCommon)
        {
            _funnelRepository = funnelRepository;
            _botRepository = botRepository;
            _adminStorageService = adminStorageService;
            _configurationCommon = configurationCommon;
        }

        public void Initialize()
        {
            _funnelRepository.CreateTableIfNotExists(FunnelsTable, true);
            _funnelRepository.CreateTableIfNotExists(BotsTable);

            if (_configurationCommon.IsDevelopment())
            {
                var result = DemoForexBot.GetFunnel();

                var funnelId = AddAdminBot(out var bot);

                AddFunnelAndChatsDemoBot(funnelId, result, bot);

                var funnelSalesId = AddSalesBot();

                _funnelRepository.AddOrUpdate(FunnelsTable, funnelSalesId.ToString(), SalesBot.GetFunnel());

                var teleBinanceBotId = AddTeleBinanceBot();
                _funnelRepository.AddOrUpdate(FunnelsTable, teleBinanceBotId.ToString(), TeleBinanceBot.GetFunnel());

                var vladTestBotId = AddVladTestBot();
                _funnelRepository.AddOrUpdate(FunnelsTable, vladTestBotId.ToString(), VladTestBot.GetFunnel());

                var deepakTestBotId = AddDeepakTestBot();
                _funnelRepository.AddOrUpdate(FunnelsTable, deepakTestBotId.ToString(), DeepakTestBot.GetFunnel());

                var paypalSaleFunnelId = AddPaypalSaleBot();
                _funnelRepository.AddOrUpdate(FunnelsTable, paypalSaleFunnelId.ToString(), PaypalSaleBot.GetFunnel());
            }

            if (_configurationCommon.IsIvoProduction())
            {
                var adminIvoId = new Guid("cd791b71-4d6a-4dcb-b233-05476e97f4eb");
                var funnelIvoId = AddIvoBot(adminIvoId);

                _funnelRepository.AddOrUpdate(FunnelsTable, funnelIvoId.ToString(), IvoBot.GetFunnel());

                AddIvoChannels(adminIvoId);

                var porteroGuardBotId = AddPorteroGuardBot();
                _funnelRepository.AddOrUpdate(FunnelsTable, porteroGuardBotId.ToString(), PorteroGuardBot.GetFunnel());
            }
        }

        private void AddIvoChannels(Guid adminIvoId)
        {
            _adminStorageService.AddChannel(adminIvoId.ToString(), new TelegramChannel()
            {
                Name = "Verified Crypto Traders Gold - Chat 💬",
                Type = TelegramChannelType.Channel,
                Id = 1167107748,
                AccessHash = 0
            });
            _adminStorageService.AddChannel(adminIvoId.ToString(), new TelegramChannel()
            {
                Name = "Verified Crypto Chat \U0001f933",
                Type = TelegramChannelType.Channel,
                Id = 1270765724,
                AccessHash = 0
            });

            _adminStorageService.AddChannel(adminIvoId.ToString(), new TelegramChannel()
            {
                Name = "Verified Silver Traders - Chat",
                Type = TelegramChannelType.Channel,
                Id = 1208625946,
                AccessHash = 0
            });

            _adminStorageService.AddChannel(adminIvoId.ToString(), new TelegramChannel()
            {
                Name = "Verified Crypto Traders ® (SolverOld)",
                Type = TelegramChannelType.Channel,
                Id = 1157202910,
                AccessHash = 0
            });

            _adminStorageService.AddChannel(adminIvoId.ToString(), new TelegramChannel()
            {
                Name = "Verified Crypto Traders ® (Gold)🎗",
                Type = TelegramChannelType.Channel,
                Id = 1281895600,
                AccessHash = 0
            });

            _adminStorageService.AddChannel(adminIvoId.ToString(), new TelegramChannel()
            {
                Name = "Verified Silver Traders",
                Type = TelegramChannelType.Channel,
                Id = 1286178319,
                AccessHash = 0
            });

            _adminStorageService.AddChannel(adminIvoId.ToString(), new TelegramChannel()
            {
                Name = "Verified Crypto News ✅ ®",
                Type = TelegramChannelType.Channel,
                Id = 1216864205,
                AccessHash = 0
            });
        }

        private void AddFunnelAndChatsDemoBot(Guid funnelId, Funnel result, Bot.telegram.funnel.Bot bot)
        {
            _funnelRepository.AddOrUpdate(FunnelsTable, funnelId.ToString(), result);
            _adminStorageService.AddChannel(bot.AdminId.ToString(), new TelegramChannel()
            {
                Name = "[B] CryptoTraders",
                Type = TelegramChannelType.Channel,
                Id = 1327897835,
                AccessHash = 0
            });
            _adminStorageService.AddChannel(bot.AdminId.ToString(), new TelegramChannel()
            {
                Name = "[B] ForexTraders Chat",
                Type = TelegramChannelType.Channel,
                Id = 1346179174,
                AccessHash = 0
            });
            _adminStorageService.AddChannel(bot.AdminId.ToString(), new TelegramChannel()
            {
                Name = "[B] ForexTraders",
                Type = TelegramChannelType.Channel,
                Id = 1383906823,
                AccessHash = 0
            });
            _adminStorageService.AddChannel(bot.AdminId.ToString(), new TelegramChannel()
            {
                Name = "[B] CryptoTraders Chat",
                Type = TelegramChannelType.Chat,
                Id = 307005531
            });
        }

        private Guid AddAdminBot(out Bot.telegram.funnel.Bot bot)
        {
            string botId = "REPLACE_WITH_YOUR_VALUE";
            Guid funnelId = new Guid("128ac8b7-806c-4660-b428-e3eab8d4c587");
            bot = new Bot.telegram.funnel.Bot()
            {
                Id = botId,
                AdminId = Guid.NewGuid(),
            };

            bot.TriggersToFunnel.Add(new TagNotAddedTrigger(botId), funnelId);

            _botRepository.AddOrUpdate(BotsTable, botId, bot);
            return funnelId;
        }

        private Guid AddSalesBot()
        {
            string botId = "REPLACE_WITH_YOUR_VALUE";
            Guid funnelId = new Guid("808ac8b7-806c-4660-b428-e3eab8d4c587");
            var bot = new Bot.telegram.funnel.Bot()
            {
                Id = botId,
                AdminId = new Guid("998ac8b7-806c-4660-b428-e3eab8d4c598"),
            };

            bot.TriggersToFunnel.Add(new TagNotAddedTrigger(botId), funnelId);

            _botRepository.AddOrUpdate(BotsTable, botId, bot);
            return funnelId;
        }

        private Guid AddPorteroGuardBot()
        {
            string botId = "REPLACE_WITH_YOUR_VALUE";
            Guid funnelId = new Guid("b6edfad2-4b75-40b5-b9f2-d734c33c0da3");
            var bot = new Bot.telegram.funnel.Bot()
            {
                Id = botId,
                AdminId = new Guid("c5edfad2-4b75-40b5-b9f2-d734c33c0da4"),
            };

            bot.TriggersToFunnel.Add(new TagNotAddedTrigger(botId), funnelId);

            _botRepository.AddOrUpdate(BotsTable, botId, bot);
            return funnelId;
        }

        private Guid AddTeleBinanceBot()
        {
            string botId = "REPLACE_WITH_YOUR_VALUE";
            Guid funnelId = new Guid("99e8de11-ea1a-42a4-9a48-e9688230fc00");
            var bot = new Bot.telegram.funnel.Bot()
            {
                Id = botId,
                AdminId = new Guid("c3562e5f-e551-42be-96db-65afe6c47141"),
            };

            bot.TriggersToFunnel.Add(new TagNotAddedTrigger(botId), funnelId);

            _botRepository.AddOrUpdate(BotsTable, botId, bot);
            return funnelId;
        }

        private Guid AddVladTestBot()
        {
            string botId = "REPLACE_WITH_YOUR_VALUE";
            Guid funnelId = new Guid("aae8de11-ea1a-42a4-9a48-e9688230fc11");
            var bot = new Bot.telegram.funnel.Bot()
            {
                Id = botId,
                AdminId = new Guid("ad562e5f-e551-42be-96db-65afe6c47133"),
            };

            bot.TriggersToFunnel.Add(new TagNotAddedTrigger(botId), funnelId);

            _botRepository.AddOrUpdate(BotsTable, botId, bot);
            return funnelId;
        }

        private Guid AddDeepakTestBot()
        {
            string botId = "REPLACE_WITH_YOUR_VALUE";
            Guid funnelId = new Guid("6760e353-8753-4ea4-a038-243a08a99eae");

            var bot = new Bot.telegram.funnel.Bot()
            {
                Id = botId,
                AdminId = new Guid("8dd837da-6707-4863-b821-991660e6ec76"),
            };


            bot.TriggersToFunnel.Add(new TagNotAddedTrigger(botId), funnelId);

            _botRepository.AddOrUpdate(BotsTable, botId, bot);
            return funnelId;
        }

        private Guid AddPaypalSaleBot()
        {
            var saleBotId = "REPLACE_WITH_YOUR_VALUE";
            var funnelId = new Guid("3355176A-B67C-4A67-A680-D51B32CBABF0");
            var adminId = new Guid("C0B1B554-D46A-4604-A5D9-6D2AA4AA6933");

            var saleBot = new Bot.telegram.funnel.Bot
            {
                Id = saleBotId,
                AdminId = adminId
            };
            saleBot.TriggersToFunnel.Add(new TagNotAddedTrigger("stop"), funnelId);
            _botRepository.AddOrUpdate(BotsTable, saleBotId, saleBot);

            return funnelId;
        }

        private Guid AddIvoTestingBot()
        {
            var ivoTestingBotId = "REPLACE_WITH_YOUR_VALUE";
            var funnelId = new Guid("6555176A-B67C-4A67-A680-D51B32CBAB56");
            var adminId = new Guid("54B1B554-D46A-4604-A5D9-6D2AA4AA6956");

            var ivoTestingBot = new Bot.telegram.funnel.Bot
            {
                Id = ivoTestingBotId,
                AdminId = adminId
            };
            ivoTestingBot.TriggersToFunnel.Add(new TagNotAddedTrigger("stop"), funnelId);
            _botRepository.AddOrUpdate(BotsTable, ivoTestingBotId, ivoTestingBot);

            return funnelId;
        }

        private Guid AddIvoBot(Guid adminId)
        {
            string botId = "REPLACE_WITH_YOUR_VALUE";
            Guid funnelId = new Guid("bc791b71-4d6a-4dcb-b233-05476e97f4da");
            var bot = new Bot.telegram.funnel.Bot()
            {
                Id = botId,
                AdminId = adminId,
            };

            bot.TriggersToFunnel.Add(new TagNotAddedTrigger(botId), funnelId);

            _botRepository.AddOrUpdate(BotsTable, botId, bot);
            return funnelId;
        }
    }

    public interface IFunnelInitializationService
    {
        void Initialize();
    }
}
