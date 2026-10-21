using System;
using System.Collections.Generic;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;

namespace Mentor.telegram.bot.api.Services.Chats
{
    public class PorteroGuardBotChat
    {
        public static string MainMenuTextAdmin = "🔸 Please click in one of the options bellow. Here you can manually triggers check for your channels.\n\n" +
                                                 "Which channel would you like to check? \n";

        public static string GoldChat = "Gold Chat";
        public static string SilverChat = "Silver Chat";
        public static string Gold = "Gold";
        public static string Silver = "Silver";
        public static string Platinum = "Platinum";
        public static string PlatinumChat = "Platinum Chat";
        public static string PlatinumChatCoffe = "Platinum Chat Coffe";

        public static string GoldCron = "Gold";
        public static string SilverCron = "Silver";
        public static string GoldChatCron = "GoldChat";
        public static string SilverChatCron = "SilverChat";
        public static string PlatinumCron = "Platinum";
        public static string PlatinumChatCron = "PlatinumChat";
        public static string PlatinumCryptoCoffeCron = "PlatinumCryptoCoffe";
    }

    public static class PorteroGuardBot
    {
        private static readonly List<string> AdminList = new List<string>(){"REPLACE_WITH_YOUR_VALUE", "dev92847","verifiedcryptotraders_admin","nickalbernas","tastybean" };

        public static Funnel GetFunnel()
        {
            var result = new Funnel();

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new PropertyUsernameIsTrigger(AdminList),
                    new MessageTrigger("/start"),
                }
            }, new List<FunnelAction>()
            {
                GetTestMenu()
            });

            CleanupButtonForChannel(result, PorteroGuardBotChat.Gold, 1281895600);
            CleanupButtonForChannel(result, PorteroGuardBotChat.Silver, 1286178319);
            CleanupButtonForChannel(result, PorteroGuardBotChat.GoldChat, 1167107748);
            CleanupButtonForChannel(result, PorteroGuardBotChat.SilverChat, 1208625946);
            CleanupButtonForChannel(result, PorteroGuardBotChat.Platinum, IvoBotChat.PlatinumId);
            CleanupButtonForChannel(result, PorteroGuardBotChat.PlatinumChat, IvoBotChat.PlatinumChatId);
            CleanupButtonForChannel(result, PorteroGuardBotChat.PlatinumChatCoffe, IvoBotChat.PlatinumCryptoCoffeId);

            CleanupCronForChannel(result, PorteroGuardBotChat.GoldCron, 1281895600);
            CleanupCronForChannel(result, PorteroGuardBotChat.SilverCron, 1286178319);
            CleanupCronForChannel(result, PorteroGuardBotChat.GoldChatCron, 1167107748);
            CleanupCronForChannel(result, PorteroGuardBotChat.SilverChatCron, 1208625946);
            CleanupCronForChannel(result, PorteroGuardBotChat.PlatinumCron, IvoBotChat.PlatinumId);
            CleanupCronForChannel(result, PorteroGuardBotChat.PlatinumChatCron, IvoBotChat.PlatinumChatId);
            CleanupCronForChannel(result, PorteroGuardBotChat.PlatinumCryptoCoffeCron, IvoBotChat.PlatinumCryptoCoffeId);

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger($"CleanUpSubscriptionTest")
                }
            }, new List<FunnelAction>()
            {
                new FindChatIdCommandByUsernameActionAsync()
                {
                    CorrelationId = Guid.NewGuid(),
                    Username = "REPLACE_WITH_YOUR_VALUE"
                },
                new CommandMessageActionAsync("Test cron call received.")
            });

            return result;
        }

        private static void CleanupButtonForChannel(Funnel result,string messageButton, int channelId)
        {
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new PropertyUsernameIsTrigger(AdminList),
                    new MessageTrigger(messageButton)
                }
            }, new List<FunnelAction>()
            {
                new CleanUpChannelActionAsync()
                {
                    ChannelId = channelId,
                    CorrelationId = Guid.NewGuid(),
                },
                GetTestMenu(false)
            });
        }

        private static void CleanupCronForChannel(Funnel result, string command, int channelId)
        {
            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger($"CleanUpSubscription{command}")
                }
            }, new List<FunnelAction>()
            {
                new CleanUpChannelActionAsync()
                {
                    ChannelId = channelId,
                    CorrelationId = Guid.NewGuid(),
                }
            });
        }

        private static MessageMenuButtonAction GetTestMenu(bool removePrevious = true)
        {
            return new MessageMenuButtonAction(PorteroGuardBotChat.MainMenuTextAdmin, new List<List<string>>()
            {
                new List<string>()
                    {PorteroGuardBotChat.Gold,PorteroGuardBotChat.Platinum,PorteroGuardBotChat.Silver},
                new List<string>()
                    {PorteroGuardBotChat.GoldChat,PorteroGuardBotChat.PlatinumChat,PorteroGuardBotChat.SilverChat},
                new List<string>()
                    {PorteroGuardBotChat.PlatinumChatCoffe}
            }, removePrevious);
        }
    }
}
