using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenTl.Schema;
using Xunit;

namespace Mentor.telegram.client.test
{
    public class TelegramBotTest
    {
        [Fact]
        public void WhenIRequestUserInformationICanRetrieveIt()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            int userId = 584308906;
            var result = bot.FindUser(userId, -1607532649804402046).Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void ICanSendMessagesWithTelegramClient()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);
            var username = "REPLACE_WITH_YOUR_VALUE";
            var result = bot.FindUserByUsername(username).Result;
            var result2 = bot.SendMessage(result.Id, result.AccessHash, "Running automated test", Guid.Empty).Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void ICanSendMessagesForPlatinumWithTelegramClient()
        {
            string WelcomeUserPlatinum =
            $"Welcome to our Premium groups!\n\n" +
            $"Make sure to take some time to read our onboarding page with useful documents like our manual, MoneyManagement tips, Risk Management, etc \n" +
            $"https://verifiedcryptotraders.com/member-area/member-onboarding/. \n\n Also checkout our Frequently Asked Questions here : https://verifiedcryptotraders.com/member-area/faq .\n\n" +
            $"If you still have any other questions, don't hesitate and contact our support team over here : @verifiedcrypto\n\n" +
            $"Please check here if your subscription has been set up correctly : @VerifiedCryptoTradersBot\n\n" +
            $"TRADING BOT: \n If you ordered our trading bot as well, or it's been included in your package: please visit the following link to sign the contract." +
            $"https://verifiedcryptotraders.com/contract-trading-bot-Gold/ \n\nYou will be redirected to the bot when you've signed the contract! \n\n" +
            $"You will find the premium channels below.\n\n";
        ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);
            var username = "REPLACE_WITH_YOUR_VALUE";
            var result = bot.FindUserByUsername(username).Result;//WelcomeUserPlatinum
            var result2 = bot.SendMessage(result.Id, result.AccessHash, WelcomeUserPlatinum, Guid.Empty).Result;
            Assert.NotNull(result);
        }
        

       [Fact]
        public void ICanSendMessageMarginWithTelegramClient()
        {
            string message = "Hi, \n This is Verified Margina Signals automated message. \n\n" +
                             "We have noticed that you did not received the access link to the Signals channel so here it is. \n" +
                             " https://t.me/joinchat/AAAAAEyphg9pP2FJxssqGw \n\n" +
                             "We have also extended your trial 7 more days so that you can make good use of it. \n\n" +
                             "If you have any further questions please contact support @larsvelthuis.";

            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);
            var username = "REPLACE_WITH_YOUR_VALUE";
            var result = bot.FindUserByUsername(username).Result;
            var result2 = bot.SendMessage(result.Id, result.AccessHash, message, Guid.Empty).Result;
            Assert.NotNull(result);
        }


        [Fact]
        public void ICanSendImagesWithTelegramClient()
        {
            string SubscriptionAboutExpired = "⏰ Hi [Username],\n\n " +
                                              "👉Your subscription with us(the Verified Crypto Group) is almost over 😢 If you don’t want to be trading alone, you can renew or upgrade your subscription in the next week with a onetime special offer.If you upgrade/renew to our new Gold subscription including the new trading BOT this week, You receive a 20% discount 🎉on your new subscription.Also you, can already use the new bot(for both channels) for the remainder of you contract.We will support you❤️ and , you will have a whole year of our support and great calls to your disposal! If you need help renewing just let me know I am here to help.\n\n" +
                                              "If you want to contact on of our agents immediately, please contact @verifiedcrypto\n Or if you want to renew your subscription, please click here : https://t.me/VerifiedCryptoTradersBot?start \n\n" +
                                              "Kind regards,\n Verified Crypto Group - Support Team \n\n";
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient("session").Result;
            var bot = new TelegramBot(client);
            var username = "REPLACE_WITH_YOUR_VALUE";
            var result = bot.FindUserByUsername(username).Result;
            var result2 = bot.SendImage(result.Id, result.AccessHash, "VcSubscriptionRenewal.jpg", Guid.NewGuid(),
                SubscriptionAboutExpired.Replace("[Username]", username)).Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void WhenIRequestUserByUserNameCanRetrieveIt()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            string username = "REPLACE_WITH_YOUR_VALUE";
            var result = bot.FindUserByUsername(username).Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void WhenIRequestUserByUserName2CanRetrieveIt()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            string username = "REPLACE_WITH_YOUR_VALUE";
            var result = bot.FindUserByUsername(username).Result;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ICanFindChannelsWhereUserIsAdmin()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            var chatData = await bot.FindChatData("[B] ForexTraders Chat");
            var channels = await bot.GetChannels();

            bool canAddUsers = false;
            foreach (var channel in channels)
            {
                if (channel.Title.Contains("[B] CryptoTraders"))
                {
                    var r = channel.AdminRights;
                    
                    if (channel.AdminRights != null &&
                        ((OpenTl.Schema.TChannelAdminRights) channel.AdminRights).InviteUsers)
                    {
                        canAddUsers = true;
                    }
                }
            }
            Assert.True(canAddUsers);

        }

        [Fact]
        public async Task ICanFindIVOChannelsWhereUserIsAdmin()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            var channels = await bot.GetChannels();
            var chats = await bot.GetChats();
            bool canAddUsers = false;
            string channelInfo = "";
            foreach (var channel in channels)
            {
                channelInfo += $"{channel.Title} with ID:{channel.Id} withHash:{channel.AccessHash} \n\n";
                if (channel.Title.Contains("Platinum"))
                {
                    var r = channel.AdminRights;
                    if (channel.AdminRights != null &&
                        ((OpenTl.Schema.TChannelAdminRights) channel.AdminRights).InviteUsers)
                    {
                        canAddUsers = true;
                    }
                }
            }

            foreach (var channel in chats)
            {
                if (channel.Title.Contains("Verified"))
                {
                    var r = channel.Id;
                }
            }

            Assert.True(canAddUsers);
        }

        [Fact]
        public async Task ICanGetParticipants()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            var userId = 460965021;
            var result = await bot.FindUser(userId);
            TUser user = (TUser) result.User;
            var chatData = await bot.FindChatData("Signals Plus Platinum Chat");

            var result2 = await bot.GetParticipants(chatData.ChatId, chatData.AccessHash, Guid.NewGuid());
        }

        [Fact]
        public async Task ICanAddUserToChat()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            int userId = 584308906;
            int chatId = 307005531;

            var result = bot.FindUser(userId).Result;
            TUser user = (TUser) result.User;
            var res = await bot.AddUserToChat(userId, chatId, user.AccessHash, Guid.Empty);
        }

        [Fact]
        public async Task ICanAddUserToChannel()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            int userId = 584308906;
            var channelId = 1327897835;
            long channelAccessHash = 0;

            var result = bot.FindUser(userId).Result;
            TUser user = (TUser) result.User;
            var res = await bot.AddUserToChannel(userId, channelId, user.AccessHash, channelAccessHash, Guid.Empty);
        }

        [Fact]
        public async Task ICanAddUserToIVoChannel()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);
            var username = "REPLACE_WITH_YOUR_VALUE";
            var channelId = 1208625946;
            long channelAccessHash = 0;

            var result = await bot.FindUserByUsername(username);
            TUser user = result;
            var res = await bot.AddUserToChannel(user.Id, channelId, user.AccessHash, channelAccessHash, Guid.Empty);
        }

        [Fact]
        public async Task ICanAddUserToSuperChat()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            int userId = 584308906;
            var result = bot.FindUser(userId).Result;
            TUser user = (TUser) result.User;
            var chatData4 = await bot.FindChatData("[B] CryptoTraders");
            var chatData = await bot.FindChatData("[B] ForexTraders Chat");
            var chatData2 = await bot.FindChatData("[B] ForexTraders");
            var chatData3 = await bot.FindChatData("[B] CryptoTraders Chat");


            bool res;
            if (chatData.ChannelType)
            {
                res = await bot.AddUserToChannel(userId, chatData.ChatId, user.AccessHash, chatData.AccessHash,
                    Guid.Empty);
            }
            else
            {
                res = await bot.AddUserToChat(userId, chatData.ChatId, user.AccessHash, Guid.Empty);
            }

            Assert.True(res);
        }

        [Fact]
        public async Task ICanRemoveUserFromChat()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            var chatData = await bot.FindChatData("[B] CryptoTraders Chat");
            int userId = 584308906;

            var result = bot.FindUser(userId).Result;
            TUser user = (TUser) result.User;
            var res = await bot.RemoveUserFromChat(userId, chatData.ChatId, user.AccessHash, Guid.Empty);
        }

        [Fact]
        public async Task ICanGetParticipantsForSilverChannel()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            var result = await bot.FindUserByUsername("verifiedcryptobutler");
            TUser user = (TUser) result;
            var chatData = await bot.FindChatData("Signals Plus Platinum");

            var result2 = await bot.GetAllParticipants(chatData.ChatId, chatData.AccessHash, Guid.NewGuid());
        }


        [Fact]
        public async Task ICanRemoveUserFromIVoSilverChat()
        {
            int userId = 584308906;
            string channelId = "REPLACE_WITH_YOUR_VALUE";
            var chatAccess = -2251056930658605218;
            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);
            var result = bot.FindUserByUsername("ElenaP1").Result;
            TUser user = (TUser) result;

            var chatId = int.Parse(channelId);
            await bot.UnBanUserFromChannel(userId, chatId, user.AccessHash, chatAccess, Guid.Empty);
        }


        [Fact]
        public async Task ICanUnBanMyselfFromAllChannels()
        {
            var channels = new List<dynamic>()
            {
                new
                {
                    Id = 1157202910,
                    AccessHash = 0
                },
                new
                {
                    Id = 1286178319,
                    AccessHash = 0
                },
                new
                {
                    Id = 1281895600,
                    AccessHash = 0
                },
                new
                {
                    Id = 1167107748,
                    AccessHash = 0
                },
                new
                {
                    Id = 1208625946,
                    AccessHash = 0
                }
            };

            ClientFactory clientFactory = new ClientFactory();
            var client = await clientFactory.GetClient();
            var bot = new TelegramBot(client);
            var result = bot.FindUserByUsername("plsarma").Result;
            TUser user = (TUser) result;

            foreach (var channel in channels)
            {
                await bot.UnBanUserFromChannel(result.Id, channel.Id, result.AccessHash, channel.AccessHash,
                    Guid.NewGuid());
            }
        }

        [Fact]
        public async Task ICanRetrieveRecentUsersFromChannel()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            var result = bot.FindChatData("Verified Crypto Chat 🤳").Result;
            var users = await bot.GetRecentUsers(12, result.ChatId, result.AccessHash, Guid.NewGuid(), 0);

            Assert.True(users.Count > 0);
        }

        [Fact]
        public async Task ICanRetrieveAllUsersFromChannel()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            var result = await bot.FindUserByUsername("verifiedcryptobutler");
            TUser user = (TUser) result;
            var chatData = await bot.FindChatData("Verified Crypto Traders ® (Gold)🎗");
            var users = await bot.GetAllParticipants(chatData.ChatId, chatData.AccessHash, Guid.NewGuid());

            var result2 = new StringBuilder();
            foreach (var user2 in users)
            {
                result2.AppendLine($"{user2.Username}, {user2.FirstName}, {user2.LastName}");
            }

            var final = result2.ToString();
            Assert.True(users.Count > 0);
        }

        [Fact]
        public async Task ICanRetrieveNonBannedUsersFromChannel()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            var result = await bot.FindUserByUsername("verifiedcryptobutler");
            TUser user = (TUser) result;
            var chatData = await bot.FindChatData("Verified Crypto Traders ® (Gold)🎗");
            var myList = new List<TUser>();

            var result2 = new StringBuilder();
            foreach (var user2 in myList)
            {
                result2.AppendLine($"{user2.Username}, {user2.FirstName}, {user2.LastName}");
            }

            var final = result2.ToString();
            Assert.True(myList.Count > 0);
        }


        [Fact]
        public async Task ICanRetrieveUsersThatAreInOneChannelButNotInTheOtherOne()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            var result = await bot.FindUserByUsername("verifiedcryptobutler");
            TUser user = (TUser) result;
            var chatData = await bot.FindChatData("Verified Crypto Traders ® (Gold)🎗");
            var myList = await bot.GetAllParticipants(chatData.ChatId, chatData.AccessHash, Guid.Empty);

            var id = 1167107748;
            var accessHash2 = 1924983784145476668;

            var myListChat = await bot.GetAllParticipants(id, accessHash2, Guid.Empty);
            var result2 = new List<TUser>();

            foreach (var user2 in myList)
            {
                if (!myListChat.Any(x => x.Id == user2.Id))
                {
                    result2.Add(user2);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var user2 in result2)
            {
                sb.AppendLine($"@{user2.Username},");
            }

            var final = sb.ToString();
            Assert.True(myList.Count > 0);
        }

        [Fact]
        public async Task ICanRetrieveUsersThatAreNotInBitMexChannel()
        {
            ClientFactory clientFactory = new ClientFactory();
            var client = clientFactory.GetClient().Result;
            var bot = new TelegramBot(client);

            var result = await bot.FindUserByUsername("verifiedcryptobutler");
            TUser user = (TUser) result;
            var chatData = await bot.FindChatData("Verified Margin Traders");
            var myList = await bot.GetAllParticipants(chatData.ChatId, chatData.AccessHash, Guid.Empty);

            var users = new List<string>()
            {
                "Charlienico", "Cufaru", "dTreee", "jcritical", "Bamischijf", "Pasha9243031", "gabeheim", "faisalsk01",
                "SriharshaP", "rapezzo", "Vipto123", "gpapageo10", "@subhankar536", "R8dr1g8", "Liamicious",
                "mohamedalkaddah", "Liamicious", "uciano", "miklosbereznai", "Leewo131", "chassan24", "@profser1121",
                "mandarshirke", "Omi98268", "cgcst1", "bilalidrees90", "kidsatoshi", "Mirko7"
            };

            var result2 = new List<string>();

            foreach (var user2 in users)
            {
                if (!myList.Any(x => x.Username == user2.Replace("@", "")))
                {
                    result2.Add(user2.Replace("@", ""));
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var user2 in result2)
            {
                sb.AppendLine($"@{user2},");
            }

            var final = sb.ToString();
            Assert.True(myList.Count > 0);
        }
    }
}