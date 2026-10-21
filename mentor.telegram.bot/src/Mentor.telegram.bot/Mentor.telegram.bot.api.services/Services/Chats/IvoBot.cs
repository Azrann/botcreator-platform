using System;
using System.Collections.Generic;
using Mentor.telegram.bot.boundedContext.Services;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;

namespace Mentor.telegram.bot.api.Services.Chats
{
    public class IvoBotChat
    {
        public const int PlatinumId = 1456376137;
        public const int PlatinumChatId = 1248811572;
        public const int PlatinumCryptoCoffeId = 1227233204;
        public static string AddUserGold = "Add User Gold";
        public static string AddUserPlatinum = "Add User Platinum";
        public static string AddUserSolverOld = "Add User SolverOld";
        public static string AddUserSilver = "Add User Silver";
        public static string CheckSubscription = "Check subscription";
        public static string MySubscription = "My subscription";
        public static string Return = "↩ RETURN";
        public static string ToMainMenu = "👉 To Main menu";
        public static string MoreInfo = "More info 👾";
        public static string FAQ = "❓ FAQ";
        public static string FAQCommand = "/faq";
        public static string FAQText = $"<b>1⃣. What Payment Methods do you provide❓</b>\n\n" +
                                       "✅ You can pay through the following payment methods:\n\n " +
                                       "👉 Pay with crypto - Over 55 crypto's are supported (International)\n " +
                                       "👉 Pay with ideal(belgium and netherlands only)\n " +
                                       "👉 Pay with PayPal(International)\n " +
                                       "👉 Pay with Credit Card(International)\n\n\n " +
                                       "<b>2⃣. What Are the prices for your plans❓</b>\n\n" +
                                       "✅ The current prices are as follows: \n\n " +
                                       "👉 Auto Trading Bot Advanced - 🚀 $349,- 🚀 - 1 Year 🏎\n " +
                                       "👉 Auto Trading Bot Basic - 🚀 $299,- 🚀 - 1 Year 🏎\n " +
                                       "👉 Platinum Subscription - 🚀 $2299,- 🚀 - 1 year 🏎\n " +
                                       "👉 Gold Subscription -🚀$1299,- 🚀-1 year 🏎\n" +
                                       "👉 Silver Subscription - 🚀 $999,- 🚀 - 1 year 🏎\n " +
                                        "If you renew your subscription using the bot you get a 5% discount on prices above.\n\n" +

                                       "<b>3⃣. If I renew my subscription, will my remaining days be deducted❓</b>\n\n" +
                                       "✅ No, The membership will be updated including your remaining Days.\n\n\n " +

                                       "<b>4⃣. The checkout asks me for a telegram username, but I don't Have one❓</b>\n\n\n " +
                                       "✅ For our automated systems it's mandatory that you provide your telegram username " +
                                       "when you perform a checkout.Watch the video at the link below to see how you can set up a Telegram" +
                                       "username. It will only take you 1 minute!\n\n" +
                                       "<a href='https://www.youtube.com/watch?v=ioEsen7kTbI'>https://www.youtube.com/watch?v=ioEsen7kTbI</a>\n\n\n " +
                                       "<b>5⃣. I don't have the funds to pay for the subscription at once.Can I split my payment❓</b>\n\n" +
                                       "✅ Yes, you can pay for your subscription in terms:\n" +
                                       "In order to do that, please contact our staff members at" +
                                       "@verifiedcrypto and they will make an arrangement with you!";

        public static string AddUserText =
            "👉 To add a new user to {0} Channels just type the username bellow without @ example larsvelthuis \n\n";

        public static string GetSubscriptionUserText =
            "👉 To get the subscriptions for a user just type the username bellow without @ example larsvelthuis \n\n";

        public static string Welcome =
            "👉 Welcome to bot. \n\n";

        public static string SubscriptionAboutExpired = "⏰ Hi [Username],\n\n " +
                                                        "👉Your subscription with us(the Verified Crypto Group) is almost over 😢 If you don’t want to be trading alone, you can renew or upgrade your subscription in the next week with a onetime special offer.If you upgrade/renew to our new Gold subscription including the new trading BOT this week, You receive a 20% discount 🎉on your new subscription.Also you, can already use the new bot(for both channels) for the remainder of you contract.We will support you❤️ and , you will have a whole year of our support and great calls to your disposal! If you need help renewing just let me know I am here to help.\n\n"+
                                                        "If you want to renew your subscription you can use this bot to do it or contact one of our agents @verifiedcrypto \n\n" +
                                                        "Kind regards,\n Verified Crypto Group - Support Team \n\n";

        public static string SubscriptionAboutExpiredTrial = "⏰ Hi [Username],\n\n " +
                                                "👉Your Trial with us(the Verified Crypto Group) is almost over 😢 If you don’t want to be trading alone, you can renew or upgrade your subscription in the next week with a onetime special offer.If you upgrade/renew to our new Gold subscription including the new trading BOT this week, You receive a 20% discount 🎉on your new subscription.Also you, can already use the new bot(for both channels) for the remainder of you contract.We will support you❤️ and , you will have a whole year of our support and great calls to your disposal! If you need help renewing just let me know I am here to help.\n\n" +
                                                "If you want to renew your subscription you can use this bot to do it or contact one of our agents @verifiedcrypto \n\n" +
                                                "Kind regards,\n Verified Crypto Group - Support Team \n\n";


        public static string SubscriptionExpiredUserMessage = "⏰ Hi [Username],\n\n " +
                                                        "👉Your subscription with us(the Verified Crypto Group) is over 😢 If you don’t want to be trading alone, you can renew or upgrade your subscription in the next week with a onetime special offer.If you upgrade/renew to our new Gold subscription including the new trading BOT this week, You receive a 20% discount 🎉on your new subscription.Also you, can already use the new bot(for both channels) for the remainder of you contract.We will support you❤️ and , you will have a whole year of our support and great calls to your disposal! If you need help renewing just let me know I am here to help.\n\n" +
                                                              "If you want to contact on of our agents immediately, please contact @verifiedcrypto\n\n" +
                                                              "Kind regards,\n Verified Crypto Group - Support Team \n\n";

        public static string ActivateNotifications = "⏰ Activate Notifications";
        public static string ActivateNotificationsText = "⏰ Notifications are activated successfully.";
        
        public static string RenewPlatinum = "Renew Platinum";
        public static string RenewGold = "Renew Gold";
        public static string RenewSilver = "Renew Silver";
        public static string NeedHelp = "Need help? 🙋‍♂️";
        public static string NeedHelpText = "Click <a href='https://t.me/Verifiedcrypto'>HERE</a> to speak with us";
        public static string SubscriptionCreated = "Thank you for your interest in renewing your [ItemName] at Verified Crypto Traders. \n \n"+
                                                   "The cost is {0} {1} \n\n " +
            "I have generated a payment link where you can complete your order. After successful payment confirmation, your subscription is being renewed automatically.\n\n"+
            "click the link to complete the payment [PaymentUrl]";

        public static string ProccesingYourPayment =
                $"📢 \n" +
                $"We are processing your payment for your [ItemName] soon you will be given access to our services.{Environment.NewLine}";

        public static string SubscriptionExpired =
            "📢 \n The {0} subscription for user @[Username] has expired. It is a good practice contact the client before removing it from channels. ";

        public static string WelcomeUserGold =
            $"Welcome to our Premium Gold groups!\n\n" +
            $"Make sure to take some time to read our onboarding page with useful documents like our manual, MoneyManagement tips, Risk Management, etc \n" +
            $"https://verifiedcryptotraders.com/member-area/member-onboarding/. \n Also checkout our Frequently Asked Questions here : https://verifiedcryptotraders.com/member-area/faq .\n" +
            $"If you still have any other questions, don't hesitate and contact our support team over here : @verifiedcrypto\n\n" +
            $"Please check here if your subscription has been set up correctly : @VerifiedCryptoTradersBot\n\n" +
            $"You will find the premium channels below.\n\n";
      

        public static string WelcomeUserGoldTrial =
            $"Welcome to our Premium Gold groups! We hope you enjoy your Trial.\n\n" +
            $"Make sure to take some time to read our onboarding page with useful documents like our manual, MoneyManagement tips, Risk Management, etc \n" +
            $"https://verifiedcryptotraders.com/member-area/member-onboarding/. \n Also checkout our Frequently Asked Questions here : https://verifiedcryptotraders.com/member-area/faq .\n" +
            $"If you still have any other questions, don't hesitate and contact our support team over here : @verifiedcrypto\n\n" +
            $"Please check here if your subscription has been set up correctly : @VerifiedCryptoTradersBot\n\n" +
            $"You will find the premium channels below.\n\n";

        public static string WelcomeUserPlatinum =
            $"Welcome to our Premium group. Please read the past information (1 -2 weeks) of the Verified Crypto Traders Platinum channel), so you are up to date on all the needed information. \n\n" +
            $"Be sure to take some time to read our onboarding page with useful documents like our manual, Money Management Tips, Risk Management, etc " +
            $"https://verifiedcryptotraders.com/member-area/member-onboarding\n" +
        $"TRADING BOT:\n" +
        $"QUESTIONS:  \n" +
        $"1️⃣  If you have any general questions, you can ask one of the admins in the free Verified Crypto chat(PINK => my name there is @verifiedcrypto) \n" +
            $"If you have a complaint or something that bothers you, just ask in a PM(Private Message) to one of the admins or by emailing feedback to info @verifiedcryptogroup.com We will try to help you within 24h.Keep in mind that sometimes our response could take a bit of time because we also have other people to attend too and have to sleep😉. Have fun trading!  \n\n";
        
        public static string WelcomeUserPlatinumReminder = 
            $"❗️IMPORTANT NOTE:\n\n" +
            $"👉 The Platinum service is a yearly plan and results can fluctuate month by month, this is part of trading. The past performance is no guarantee of future performances. Our results are only shared/discussed at the end of the year. With the current members, the service has performed perfectly over the last year. You are welcome to be a part of this. \n\n" +
            $"❗️Please don’t join if you like to push for more signals, don’t want to learn, have a negative mindset, have no money or little money to trade with or want fast results. Negative draining people are not allowed in the group and will be removed for the group's benefit.\n\n" +
            $"We think of you as a fallow adult and so behave like one.\n\n" +
            $"👉 We are not a fund, we provide signals and information about the market, opportunities (Alts) that you can trade with your BOT. \n" +
            $"If you just follow the signals and chat that should be enough to enjoy the information service at its best.\n\n" +
            $"❗️With trading comes risk and rewards, the risk of losing it all is always a possibility, never trade with money that you can't miss to lose.\n\n" +
            $"👉The amount you deposit in your exchange accounts is up to you but most of our members have 1BTC or more divided between the two channels to have the best results possible. (Because of the hedging style of trading)\n" +
            $"The best time to ask questions about the market is after the release of the Platinum (weekly) update video. \n\n" +
            $"The video is released in the Platinum chat channel. \n\n" +
            $"❗️ Erik is a knowledgable trader and has over 30 years of experience. Learn, support and in the end, we want to have fun and make profits in trading.\n\n" +
            $"👉 Ivo can help you with the setup of the BOT, account and general questions en settings assistance. \n\n" +
            $"❗️We can't see your BOT trades because it is a 3rd party tool and because of legal en security reasons. That is one of the reasons why you are responsible for the setup and to make sure it does it's tasks for you. Updates for the BOT will be done by Cornix. \n\n" +
            $"👉 If you want to share ideas, help or have any improvements/feedback for us please send me a direct message.\n\n";


        public static string WelcomeUserSilver =
            $"Welcome to our Premium Silver groups!\n\n" +
            $"Make sure to take some time to read our onboarding page with useful documents like our manual, MoneyManagement tips, Risk Management, etc \n" +
            $"https://verifiedcryptotraders.com/member-area/member-onboarding/. \n Also checkout our Frequently Asked Questions here : https://verifiedcryptotraders.com/member-area/faq .\n" +
            $"If you still have any other questions, don't hesitate and contact our support team over here : @verifiedcrypto\n\n" +
            $"Please check here if your subscription has been set up correctly : @VerifiedCryptoTradersBot\n\n" +
            $"You will find the premium channels below.\n\n";

        public static string ReceivedYourPayment =
            $"Thank you for your order! \n\n" +
            $"We've received your payment of [ReceivedAmount] [BuyerCurrency] for your [ItemName] and updated your membership in our system. You will now get notified by our Verified Crypto Traders Butler Bot. He will send you all the links for the channels. If you have questions about your order please don't hesitate and contact us here: @verifiedcrypto";
           
        public static string GoldChannelLink = $"https://t.me/joinchat/AAAAAExoLLBJTyPi-StqmA \n\n";// Gold 
        public static string GoldChatLink = "https://t.me/joinchat/HftnpEWQpqTYQzrBSOueYg \n\n"; // Gold chat
        public static string GoldBot = "https://t.me/verified_crypto_trading_bot?start=invite-eb63e3b9736842b2b27378c5b0d04d5e \n\n";
        public static string SilverLink = "https://t.me/joinchat/AAAAAEyphg_NMOYFb18okg \n\n"; // Silver 
        public static string SilverChatLink = "https://t.me/joinchat/JCnjnkgKKxqxopEgFYhGFQ \n\n"; // Silver chat
        public static string SilverBot = "https://t.me/verified_crypto_trading_bot?start=invite-3ca61997868c4f24b6db55243891bdbc \n\n";
        public static string CryptoNewsLink = "https://t.me/VerifiedCryptoNews \n\n";
        public static string SolverOldLink = "https://t.me/joinchat/AAAAAET5g948f7vQo0cLXg \n\n";
        public static string PlatinumLink = "https://t.me/joinchat/AAAAAFbOiUnAnuv_T9_r6g \n\n";
        public static string PlatinumChatLink = "https://t.me/joinchat/HftnpEpvWjTMexlhqoz2vA \n\n";
        public static string PlatinumBot = "https://t.me/verified_crypto_trading_bot?start=invite-6990b8ef3a3a408f9298358d7a0f81a4 \n\n";
        public static string PlatinumLongLink = "https://t.me/joinchat/AAAAAEkmF7Qvs3xc0RSgBA \n\n";

        public static string FreeInviteLinks = $"📢 \n" +
                                               "https://t.me/VerifiedCryptoNews \n\n";

        public static string MainMenuTextUser = "<b>Main Menu:</b> \nChoose an option";
        public static string MainMenuTextAdmin = "<b>Main Menu:</b> \nChoose an option";

        /*DiscountCodes*/
        public static string Vctsovip5Gold = "vctsovip5Gold";
        public static string Vctsovip5SolverOld = "vctsovip5SolverOld";
        public static string Vctsovip5Silver = "vctsovip5Silver";
        public static string Vctstvip5Gold = "vctstvip5Gold";
        public static string Vctstvip5SolverOld = "vctstvip5SolverOld";
        public static string Vctstvip5Silver = "vctstvip5Silver";

        public static string TenOffGold = "10offGold";
        public static string GoldDiscount20 = "Gold discount 20%";
        public static string SolverOldToGold = "SolverOldtoGold";
        public static string SilverToGold = "SilvertoGold";
        public static string BronzeToGold = "bronzetoGold";
        public static string GoldDiscount = "Golddiscount";
        public static string SilverDiscount = "Silverdiscount";
        public static string SilverRenewTrialDiscount = "Silverrenewtrial";
    }

    public static class IvoBot
    {
        private static readonly List<string> AdminList = new List<string>(){ "verifiedcryptotraders_admin", "REPLACE_WITH_YOUR_VALUE", "dev92847" };
        private static readonly List<string> TypeCommandsList = new List<string>() {
            IvoBotChat.RenewGold,IvoBotChat.RenewPlatinum,IvoBotChat.RenewSilver, IvoBotChat.NeedHelp, IvoBotChat.Vctsovip5Gold,IvoBotChat.Vctsovip5SolverOld,IvoBotChat.Vctsovip5Silver,IvoBotChat.Vctstvip5Gold
            ,IvoBotChat.Vctstvip5SolverOld,IvoBotChat.Vctstvip5Silver,IvoBotChat.TenOffGold,IvoBotChat.GoldDiscount20,IvoBotChat.ActivateNotifications
            ,IvoBotChat.SolverOldToGold,IvoBotChat.SilverToGold,IvoBotChat.BronzeToGold,IvoBotChat.GoldDiscount20,IvoBotChat.GoldDiscount,IvoBotChat.SilverDiscount,
            IvoBotChat.BronzeToGold,
        };

        public static Funnel GetFunnel()
        {
            var result = new Funnel();

            AddAdminMenuCommand(result);

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new PropertyUsernameIsNotTrigger(AdminList),
                    new MessageIsNotTrigger(TypeCommandsList)
                }
            }, new List<FunnelAction>()
            {
                new ClientCreateAction(),
                GetUserMainMenu()
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new PropertyUsernameIsNotTrigger(AdminList),
                    new MessageTrigger(IvoBotChat.ToMainMenu)
                }
            }, new List<FunnelAction>()
            {
                GetUserMainMenu()
            });

            AddWebCommands(result);

            // SubscriptionExpired Flow
            SubscriptionExpiredReminder(result);

            SubscriptionExpired(result);

            DiscountCodes(result);

            AddFAQCallback(result);

            AddMySubscriptionCallback(result);

            NotifyAboutToExpire(result);

            return result;
        }

        private static void AddWebCommands(Funnel result)
        {
            /*result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("AddUserSolverOld"),
                }
            }, new List<FunnelAction>()
            {                new ChannelUnBanMyselfActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1157202910, // SolverOld
                    }
                },
                new TgClientMessageActionAsync(IvoBotChat.FreeInviteLinks,150),
                new TgClientMessageActionAsync(IvoBotChat.SolverOldLink,150),
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserSolverOld,150),
            });*/

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("AddUserSilver"),
                }
            }, new List<FunnelAction>()
            {
                new ChannelUnBanMyselfActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, // Silver
                        1286178319
                    }
                },
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserSilver),
                new TgClientMessageActionAsync(GetLinksFormat("Silver",IvoBotChat.SilverLink,IvoBotChat.SilverChatLink,IvoBotChat.SilverBot),150),
                new TgClientMessageActionAsync(IvoBotChat.FreeInviteLinks,150),
                new FindChatIdCommandByUsernameActionAsync()
                {
                    CorrelationId = Guid.NewGuid(),
                    Username = "REPLACE_WITH_YOUR_VALUE"
                },
                new CommandMessageActionAsync("New user in Silver -> @[Username]")
            });

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("AddUserGold"),
                }
            }, new List<FunnelAction>()
            {
                new ChannelUnBanMyselfActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, // Silver
                        1286178319,
                        1167107748, // Gold
                        1281895600
                    }
                },
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserGold,150),
                new TgClientMessageActionAsync(GetLinksFormat("Gold",IvoBotChat.GoldChannelLink,IvoBotChat.GoldChatLink,IvoBotChat.GoldBot)),
                new TgClientMessageActionAsync(GetLinksFormat("Silver",IvoBotChat.SilverLink,IvoBotChat.SilverChatLink,IvoBotChat.SilverBot),150),
                new TgClientMessageActionAsync(IvoBotChat.CryptoNewsLink,150),
                new FindChatIdCommandByUsernameActionAsync()
                {
                    CorrelationId = Guid.NewGuid(),
                    Username = "REPLACE_WITH_YOUR_VALUE"
                },
                new CommandMessageActionAsync("New user in Gold -> @[Username]")
            });

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("AddUserGoldTrial"),
                }
            }, new List<FunnelAction>()
            {
                new ChannelUnBanMyselfActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, // Silver
                        1286178319,
                        1167107748, // Gold
                        1281895600
                    }
                },
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserGoldTrial,150),
                new TgClientMessageActionAsync(GetLinksFormat("Gold",IvoBotChat.GoldChannelLink,IvoBotChat.GoldChatLink,IvoBotChat.GoldBot)),
                new TgClientMessageActionAsync(GetLinksFormat("Silver",IvoBotChat.SilverLink,IvoBotChat.SilverChatLink,IvoBotChat.SilverBot),150),
                new TgClientMessageActionAsync(IvoBotChat.CryptoNewsLink,150),
                new FindChatIdCommandByUsernameActionAsync()
                {
                    CorrelationId = Guid.NewGuid(),
                    Username = "REPLACE_WITH_YOUR_VALUE"
                },
                new CommandMessageActionAsync("New user in Gold Trial -> @[Username]")
            });

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("AddUserPlatinum"),
                }
            }, new List<FunnelAction>()
            {
                new ChannelUnBanMyselfActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, // Silver
                        1286178319,
                        1167107748, // Gold
                        1281895600,
                        IvoBotChat.PlatinumId,
                        IvoBotChat.PlatinumChatId,
                        IvoBotChat.PlatinumCryptoCoffeId
                    }
                },
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserPlatinum),
                new TgClientMessageActionAsync(GetLinksFormat("Platinum",IvoBotChat.PlatinumLink,IvoBotChat.PlatinumChatLink, IvoBotChat.PlatinumLongLink, IvoBotChat.PlatinumBot),150),
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserPlatinumReminder),
                new FindChatIdCommandByUsernameActionAsync()
                {
                    CorrelationId = Guid.NewGuid(),
                    Username = "REPLACE_WITH_YOUR_VALUE"
                },
                new CommandMessageActionAsync("New user in Platinum -> @[Username]")
            });

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("AddUserBronze"),
                }
            }, new List<FunnelAction>()
            {
                new ChannelUnBanMyselfActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, // Silver
                        1286178319,
                        1167107748, // Gold
                        1281895600
                    }
                },
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserGold),
                new TgClientMessageActionAsync(GetLinksFormat("Gold",IvoBotChat.GoldChannelLink,IvoBotChat.GoldChatLink,IvoBotChat.GoldBot),150),
                new TgClientMessageActionAsync(GetLinksFormat("Silver",IvoBotChat.SilverLink,IvoBotChat.SilverChatLink,IvoBotChat.SilverBot),150),
                new TgClientMessageActionAsync(IvoBotChat.CryptoNewsLink,150),
                new FindChatIdCommandByUsernameActionAsync()
                {
                    CorrelationId = Guid.NewGuid(),
                    Username = "REPLACE_WITH_YOUR_VALUE"
                },
                new CommandMessageActionAsync("New user in Bronze -> @[Username]")
            });
        }

        private static void AddAdminMenuCommand(Funnel result)
        {
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new PropertyUsernameIsTrigger(AdminList),
                    new MessageIsNotTrigger(TypeCommandsList),
                    new StateIsNotValueTrigger("AddingUserGold","true"),
                    new StateIsNotValueTrigger("AddingUserPlatinum","true"),
                    new StateIsNotValueTrigger("AddingUserSolverOld","true"),
                    new StateIsNotValueTrigger("AddingUserSilver","true"),
                    new StateIsNotValueTrigger("CheckingSubscription","true")
                }
            }, new List<FunnelAction>()
            {
                GetAdminMainMenu()
            });

            result.MessageActions.Add(GetTriggerAddUser("AddingUserGold"), new List<FunnelAction>()
            {
                new MessageButtonAction("Adding user to Gold channels and sending messages", new List<List<string>>()
                {
                    new List<string>()
                        {IvoBotChat.Return}
                },false,200),
                new FindChatIdFromMessageTextActionAsync(),
                new CommandChannelUnBanActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, // Silver
                        1286178319,
                        1167107748, // Gold
                        1281895600
                    }
                },
                new CommandClientMessageActionAsync(IvoBotChat.WelcomeUserGold),
                new CommandClientMessageActionAsync(GetLinksFormat("Gold",IvoBotChat.GoldChannelLink,IvoBotChat.GoldChatLink,IvoBotChat.GoldBot),150),
                new CommandClientMessageActionAsync(GetLinksFormat("Silver",IvoBotChat.SilverLink,IvoBotChat.SilverChatLink,IvoBotChat.SilverBot),150),
                new CommandClientMessageActionAsync(IvoBotChat.CryptoNewsLink,150)
            });

            result.MessageActions.Add(GetTriggerAddUser("AddingUserPlatinum"), new List<FunnelAction>()
            {
                new MessageButtonAction("Adding user to Platinum channels and sending messages", new List<List<string>>()
                {
                    new List<string>()
                        {IvoBotChat.Return}
                },false,200),
                new FindChatIdFromMessageTextActionAsync(),
                new CommandChannelUnBanActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, // Silver
                        1286178319,
                        1167107748, // Gold
                        1281895600,
                        IvoBotChat.PlatinumId,
                        IvoBotChat.PlatinumChatId,
                        IvoBotChat.PlatinumCryptoCoffeId
                    }
                },
                new CommandClientMessageActionAsync(IvoBotChat.WelcomeUserPlatinum,150),
                new CommandClientMessageActionAsync(GetLinksFormat("Platinum",IvoBotChat.PlatinumLink,IvoBotChat.PlatinumChatLink, IvoBotChat.PlatinumLongLink,IvoBotChat.PlatinumBot)),
                new CommandClientMessageActionAsync(IvoBotChat.WelcomeUserPlatinumReminder,150)
            });

            /*result.MessageActions.Add(GetTriggerAddUser("AddingUserSolverOld"), new List<FunnelAction>()
            {
                new MessageButtonAction("Adding user to SolverOld channels", new List<List<string>>()
                {
                    new List<string>()
                        {IvoBotChat.Return}
                },false,200),
                new FindChatIdFromMessageTextActionAsync(),
                new CommandChannelUnBanActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1157202910, // SolverOld
                    }
                },
                new CommandClientMessageActionAsync(IvoBotChat.FreeInviteLinks,150),
                new CommandClientMessageActionAsync(IvoBotChat.SolverOldLink,150),
                new CommandClientMessageActionAsync(IvoBotChat.WelcomeUserSolverOld,150),
            });*/

            result.MessageActions.Add(GetTriggerAddUser("AddingUserSilver"), new List<FunnelAction>()
            {
                new MessageButtonAction("Adding user to Silver channels", new List<List<string>>()
                {
                    new List<string>()
                        {IvoBotChat.Return}
                },false,200),
                new FindChatIdFromMessageTextActionAsync(),
                new CommandChannelUnBanActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, // Silver
                        1286178319
                    }
                },
                new CommandClientMessageActionAsync(IvoBotChat.WelcomeUserSilver),
                new CommandClientMessageActionAsync(GetLinksFormat("Silver",IvoBotChat.SilverLink,IvoBotChat.SilverChatLink,IvoBotChat.SilverBot),150),
                new CommandClientMessageActionAsync(IvoBotChat.FreeInviteLinks,150),
            });

            result.MessageActions.Add(GetTriggerAddUser("CheckingSubscription"), new List<FunnelAction>()
            {
                new MessageButtonAction("Retrieving subscription for user", new List<List<string>>()
                {
                    new List<string>()
                        {IvoBotChat.Return}
                },false,200),
                new FindChatIdFromMessageTextActionAsync(),
                new CommandIvoGetSubscriptionAsync(),
            });

            ReturnToAdminMenu(result, "AddingUserGold");
            ReturnToAdminMenu(result, "AddingUserPlatinum");
            ReturnToAdminMenu(result, "AddingUserSolverOld");
            ReturnToAdminMenu(result, "AddingUserSilver");
            ReturnToAdminMenu(result, "CheckingSubscription");

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new PropertyUsernameIsTrigger(AdminList),
                    new MessageTrigger(IvoBotChat.ToMainMenu)
                }
            }, new List<FunnelAction>()
            {
                GetAdminMainMenu()
            });

            AddUserCommandSetState(result, IvoBotChat.AddUserGold, "AddingUserGold", "Gold");
            AddUserCommandSetState(result, IvoBotChat.AddUserPlatinum, "AddingUserPlatinum", "Platinum");
            AddUserCommandSetState(result, IvoBotChat.AddUserSolverOld, "AddingUserSolverOld", "SolverOld");
            AddUserCommandSetState(result, IvoBotChat.AddUserSilver, "AddingUserSilver", "Silver");
            GetSubscriptionForUserCommandSetState(result, IvoBotChat.CheckSubscription, "CheckingSubscription");
        }

        private static void AddUserCommandSetState(Funnel result, string command, string state,string subscription)
        {
            var trigger = new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new PropertyUsernameIsTrigger(AdminList),
                    new MessageTrigger(command),
                }
            };

            result.CallbackActions.Add(trigger, new List<FunnelAction>()
            {
                new MessageButtonAction(string.Format(IvoBotChat.AddUserText,subscription), new List<List<string>>()
                {
                    new List<string>()
                        {IvoBotChat.Return}
                }, true),
                new StateUpdateAction(state, "true")
            });
        }

        private static void GetSubscriptionForUserCommandSetState(Funnel result, string command, string state)
        {
            var trigger = new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new PropertyUsernameIsTrigger(AdminList),
                    new MessageTrigger(command),
                }
            };

            result.CallbackActions.Add(trigger, new List<FunnelAction>()
            {
                new MessageButtonAction(IvoBotChat.GetSubscriptionUserText, new List<List<string>>()
                {
                    new List<string>()
                        {IvoBotChat.Return}
                }, true),
                new StateUpdateAction(state, "true")
            });
        }

        private static void ReturnToAdminMenu(Funnel result,string state)
        {
            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new PropertyUsernameIsTrigger(AdminList),
                    new MessageTrigger(IvoBotChat.Return),
                    new StateIsValueTrigger(state, "true")
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction(state, "false"),
                GetAdminMainMenu()
            });
        }

        private static AndTrigger GetTriggerAddUser(string state)
        {
            return new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new PropertyUsernameIsTrigger(AdminList),
                    new MessageIsNotTrigger(new List<string>
                    {
                        IvoBotChat.AddUserGold
                    }),new MessageIsNotTrigger(new List<string>
                    {
                        IvoBotChat.AddUserSolverOld
                    }),new MessageIsNotTrigger(new List<string>
                    {
                        IvoBotChat.AddUserSilver
                    }),
                    new MessageIsNotTrigger(new List<string>
                    {
                        IvoBotChat.Return
                    }),
                    new StateIsValueTrigger(state,"true")
                }
            };
        }

        private static void DiscountCodes(Funnel result)
        {
            /*  TODO   AddDiscountCallback(result, IvoBotChat.Vctsovip5Gold, Gold, 950);
            var Gold = "Gold";
            var SolverOld = "SolverOld";
            var Silver = "Silver";

        
            AddDiscountCallback(result, IvoBotChat.Vctsovip5SolverOld, SolverOld, 475);
            AddDiscountCallback(result, IvoBotChat.Vctsovip5Silver, Silver, 570);
            AddDiscountCallback(result, IvoBotChat.Vctstvip5Gold, Gold, 950);
            AddDiscountCallback(result, IvoBotChat.Vctstvip5SolverOld, SolverOld, 475);
            AddDiscountCallback(result, IvoBotChat.Vctstvip5Silver, Silver, 570);

            AddDiscountCallback(result, IvoBotChat.TenOffGold, Gold, 900);
            AddDiscountCallback(result, IvoBotChat.GoldDiscount20, Gold, 800);

            AddDiscountCallback(result, IvoBotChat.SolverOldToGold, Gold, 500);
            AddDiscountCallback(result, IvoBotChat.SilverToGold, Gold, 400);
            AddDiscountCallback(result, IvoBotChat.BronzeToGold, Gold, 600);
            AddDiscountCallback(result, IvoBotChat.GoldDiscount, Gold, 800);

            AddDiscountCallback(result, IvoBotChat.SilverDiscount, Silver, 550);
            AddDiscountCallback(result, IvoBotChat.SilverRenewTrialDiscount, Silver, 570);*/
        }
        
        private static void SubscriptionExpired(Funnel result)
        {
            AddSubscriptionExpiredActions(result,"Gold");
            AddSubscriptionExpiredActions(result, "SolverOld");
            AddSubscriptionExpiredActions(result, "Silver", "Silver");
            AddSubscriptionExpiredActions(result, "Bronze");
            AddSubscriptionExpiredActions(result, "Platinum");
        }

        private static void AddSubscriptionExpiredActions(Funnel result, string subscription, string subscriptionName = "")
        {
            var subsName = subscriptionName == "" ? subscription : subscriptionName;
            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger($"SubscriptionExpired{subscription}"),
                }
            }, new List<FunnelAction>()
            {
                new SetUsernameCommandAction()
                {
                    Username = "REPLACE_WITH_YOUR_VALUE"
                },
                new CommandClientMessageActionAsync(string.Format(IvoBotChat.SubscriptionExpired, subsName),1000),
            });
        }

        private static void SubscriptionExpiredReminder(Funnel result)
        {
            AddRenewCallback(result, IvoBotChat.RenewPlatinum, "Platinum", 2299);
            AddRenewCallback(result, IvoBotChat.RenewGold, "Gold", 1299);
            AddRenewCallback(result, IvoBotChat.RenewSilver, "Silver", 999);
            AddNeedHelpCallback(result);
            //AddActivateNotificationsCallback(result);

            int status1 = 1;
            int status100 = 100;

            GetConfirmingMessage(result, status1, "Platinum");
            GetConfirmingMessage(result, status1, "Gold");
            GetConfirmingMessage(result, status1, "Silver");

            GetPaymentCompleteBehaviourPlatinum(result, status100);
            GetPaymentCompleteBehaviourGold(result, status100);
            GetPaymentCompleteBehaviourSilver(result, status100);
        }

        private static void AddActivateNotificationsCallback(Funnel result)
        {
            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(IvoBotChat.ActivateNotifications),
                }
            }, new List<FunnelAction>()
            {
                new MessageAction(IvoBotChat.ActivateNotificationsText),
                GetUserMainMenu(false)
            });
        }
        
        private static void AddNeedHelpCallback(Funnel result)
        {
            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(IvoBotChat.NeedHelp),
                }
            }, new List<FunnelAction>()
            {
                new MessageAction(IvoBotChat.NeedHelpText),
                GetUserMainMenu()
            }); 
        }

        private static void AddFAQCallback(Funnel result)
        {
            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(IvoBotChat.FAQ),
                }
            }, new List<FunnelAction>()
            {
                new MessageAction(IvoBotChat.FAQText),
                GetUserMainMenu(false)
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(IvoBotChat.FAQCommand),
                }
            }, new List<FunnelAction>()
            {
                new MessageAction(IvoBotChat.FAQText),
                GetUserMainMenu(false)
            });
        }

        private static void AddMySubscriptionCallback(Funnel result)
        {
            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(IvoBotChat.MySubscription),
                }
            }, new List<FunnelAction>()
            {
                new IvoHttpGetSubscriptionActionAsync(),
                GetUserMainMenu(false)
            });
        }

        private static void NotifyAboutToExpire(Funnel result)
        {
            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger($"SendSubscriptionAboutToExpire1")
                }
            }, new List<FunnelAction>()
            { 
                new IvoSendAboutToExpireNotificationAsync(){ ExpirationData = new List<ExpirationData>()
                {
                    new ExpirationData{Days=90,Message=IvoBotChat.SubscriptionAboutExpired,MembershipName="Gold" },
                    new ExpirationData{Days=2,Message=IvoBotChat.SubscriptionAboutExpiredTrial,MembershipName="Gold-subscription-trial-7-days" },
                    new ExpirationData{Days=2,Message=IvoBotChat.SubscriptionAboutExpiredTrial,MembershipName="Silver-subscription-trial-7-days" },
                    new ExpirationData{Days=2,Message=IvoBotChat.SubscriptionAboutExpiredTrial,MembershipName="Silver-trial" }
                } },
            });

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger($"SendSubscriptionAboutToExpire2")
                }
            }, new List<FunnelAction>()
            {
                new IvoSendAboutToExpireNotificationAsync(){ ExpirationData = new List<ExpirationData>()
                {
                    new ExpirationData{Days=90,Message=IvoBotChat.SubscriptionAboutExpired,MembershipName="SolverOld" }
                } },
            });

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger($"SendSubscriptionAboutToExpire3")
                }
            }, new List<FunnelAction>()
            {
                new IvoSendAboutToExpireNotificationAsync(){ ExpirationData = new List<ExpirationData>()
                {
                    new ExpirationData{Days=90,Message=IvoBotChat.SubscriptionAboutExpired,MembershipName="Silver" }
                } },
            });

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger($"SendSubscriptionAboutToExpire4")
                }
            }, new List<FunnelAction>()
            {
                new IvoSendAboutToExpireNotificationAsync(){ ExpirationData = new List<ExpirationData>()
                {
                    new ExpirationData{Days=90,Message=IvoBotChat.SubscriptionAboutExpired,MembershipName="Platinum" }
                } },
            });
        }

        private static void GetPaymentCompleteBehaviourGold(Funnel result, int status100)
        {
            var pCompleted = new PropertiesStartWithTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status100},
                {"ItemName", $"1 Year Gold Subscription"}
            })
            {
                Priority = 100
            };
            var actions = new List<FunnelAction>()
            {
                new MessageAction(IvoBotChat.ReceivedYourPayment),
                new IvoHttpNotificationActionAsync()
                {
                    Command = $"PaymentCompletedGold",
                    MonthsDuration = 12
                },
                new ChannelUnBanMyselfActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, // Silver
                        1286178319,
                        1167107748, // Gold
                        1281895600
                        //1157202910, // SolverOld
                    }
                },
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserGold,150),
                new TgClientMessageActionAsync(GetLinksFormat("Gold",IvoBotChat.GoldChannelLink,IvoBotChat.GoldChatLink,IvoBotChat.GoldBot),150),
                new TgClientMessageActionAsync(GetLinksFormat("Silver",IvoBotChat.SilverLink,IvoBotChat.SilverChatLink,IvoBotChat.SilverBot),150),
                new TgClientMessageActionAsync(IvoBotChat.CryptoNewsLink,150)
            };

            result.EventActions.Add(pCompleted, actions);
        }

        private static void GetPaymentCompleteBehaviourSilver(Funnel result, int status100)
        {
            var pCompleted = new PropertiesStartWithTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status100},
                {"ItemName", $"1 Year Silver Subscription"}
            })
            {
                Priority = 100
            };
            var actions = new List<FunnelAction>()
            {
                new MessageAction(IvoBotChat.ReceivedYourPayment),
                new IvoHttpNotificationActionAsync()
                {
                    Command = $"PaymentCompletedSilver",
                    MonthsDuration = 12
                },
                new ChannelUnBanMyselfActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, // Silver
                        1286178319,
                        //1157202910, // SolverOld
                    }
                },
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserSilver),
                new TgClientMessageActionAsync(IvoBotChat.FreeInviteLinks,150),
                new TgClientMessageActionAsync(GetLinksFormat("Silver",IvoBotChat.SilverLink,IvoBotChat.SilverChatLink,IvoBotChat.SilverBot),150),
            };

            result.EventActions.Add(pCompleted, actions);
        }

        private static void GetPaymentCompleteBehaviourPlatinum(Funnel result, int status100)
        {
            var pCompleted = new PropertiesStartWithTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status100},
                {"ItemName", $"1 Year Platinum Subscription"}
            })
            {
                Priority = 100
            };
            var actions = new List<FunnelAction>()
            {
                new MessageAction(IvoBotChat.ReceivedYourPayment),
                new IvoHttpNotificationActionAsync()
                {
                    Command = $"PaymentCompletedPlatinum",
                    MonthsDuration = 12
                },
                new ChannelUnBanMyselfActionAsync()
                {
                    ChannelIds = new List<int>()
                    {
                        1208625946, 
                        1286178319,
                        1167107748, 
                        1281895600,
                        IvoBotChat.PlatinumId,
                        IvoBotChat.PlatinumChatId,
                        IvoBotChat.PlatinumCryptoCoffeId,
                    }
                },
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserPlatinum),
                new TgClientMessageActionAsync(GetLinksFormat("Platinum",IvoBotChat.PlatinumLink,IvoBotChat.PlatinumChatLink, IvoBotChat.PlatinumLongLink,IvoBotChat.PlatinumBot),150),
                new TgClientMessageActionAsync(IvoBotChat.WelcomeUserPlatinumReminder)
            };

            result.EventActions.Add(pCompleted, actions);
        }

        private static void GetConfirmingMessage(Funnel result, int status1, string subscription)
        {
            var pConfirming = new PropertiesStartWithTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status1},
                {"ItemName", $"1 Year {subscription} Subscription"}
            })
            {
                Priority = 100
            };

            result.EventActions.Add(pConfirming, new List<FunnelAction>()
            {
                new MessageAction(IvoBotChat.ProccesingYourPayment)
            });
        }

        private static void AddRenewCallback(Funnel result,string message, string subscription, double cost)
        {
            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(message),
                }
            }, new List<FunnelAction>()
            {
                new CgPaymentInfoCreateActionAsync()
                {
                    MonthsDuration = 12,
                    Amount = cost,
                    CorrelationId = Guid.NewGuid(),
                    OriginalCurrency = "USD",
                    Title = $"1 Year {subscription} Subscription",
                    Description = "Subscription fee: Verified Crypto Traders"
                },
                new MessageAction(string.Format(IvoBotChat.SubscriptionCreated,cost, "dollars"),false,100)
            });
        }

        private static void AddDiscountCallback(Funnel result, string message, string subscription, double cost)
        {
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(message),
                }
            }, new List<FunnelAction>()
            {
                new MessageAction($"Discount code \"{message}\" ACTIVATED for 12 Months {subscription} subscription. \n New price is {cost} $.",false),
                new CgPaymentInfoCreateActionAsync()
                {
                    MonthsDuration = 12,
                    Amount = cost,
                    CorrelationId = Guid.NewGuid(),
                    OriginalCurrency = "USD",
                    Title = $"1 Year {subscription} Subscription. {message}",
                    Description = $"Subscription fee: Verified Crypto Traders. Discount Activated."
                },
                new MessageAction(IvoBotChat.SubscriptionCreated,false,100)
            });
        }

        private static MessageButtonAction GetAdminMainMenu(bool removePrevious = true)
        {
            return new MessageButtonAction(IvoBotChat.MainMenuTextAdmin, new List<List<string>>()
            {
                new List<string>()
                    {IvoBotChat.AddUserPlatinum},
                new List<string>()
                    {IvoBotChat.CheckSubscription}
            }, removePrevious);
        }

        private static MessageButtonAction GetUserMainMenu(bool removePrevious = true)
        {
            return new MessageButtonAction(IvoBotChat.MainMenuTextUser, new List<List<string>>()
            {
                new List<string>()
                    {IvoBotChat.MySubscription},
                new List<string>()
                    {IvoBotChat.RenewGold, IvoBotChat.RenewSilver,IvoBotChat.RenewPlatinum},
                new List<string>()
                    {IvoBotChat.NeedHelp, IvoBotChat.FAQ}
            }, false,200);
        }

        private static string GetLinksFormat(string name, string channelLink, string chatLink)
        {
            return $"{name} links: \n ✅ {name} channel:{channelLink} ✅{name} chat:{chatLink}\n";
        }

        private static string GetLinksFormat(string name, string channelLink, string chatLink, string otherLink)
        {
            return $"{name} links: \n ✅ {name} channel:{channelLink} ✅{name} chat:{chatLink} ✅{name} others:{otherLink}\n";
        }

        private static string GetLinksFormat(string name, string channelLink, string chatLink, string channelLink2, string otherLink)
        {
            return $"{name} links: \n ✅ {name} Short:{channelLink} ✅{name} Chat:{chatLink} ✅ {name} Long:{channelLink2} ✅{name} BOT:{otherLink}\n";
        }
    }
}
