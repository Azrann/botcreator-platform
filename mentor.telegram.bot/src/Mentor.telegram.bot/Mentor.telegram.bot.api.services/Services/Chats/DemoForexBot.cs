using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;
using System;
using System.Collections.Generic;
using Mentor.telegram.bot.models;

namespace Mentor.telegram.bot.api.Services.Chats
{
    public static class DemoForexBotChat
    {
        public static string GetStartMessage()
        {
            return
                $"Hi, welcome to <b>CryptoTraders</b> Registration process!{Environment.NewLine} {Environment.NewLine}I am here to try to answer all your questions but first let me give you the information many people ask about.{Environment.NewLine} {Environment.NewLine}" +
                $"We give normally 2 or 3 signals a week depending on the market and you can improve your trading by asking us questions in the private group.{Environment.NewLine}{Environment.NewLine}" +
                $"Regarding exchanges you will need an account on Binance and Bitfinex.{Environment.NewLine}{Environment.NewLine}" +
                $"<b>Private members fee</b> is 0.003 BTC per month and they get access to our private channel ,all of our signals and a free book to improve your trading.{Environment.NewLine}{Environment.NewLine}" +
                $"Are you joining us?";
        }

        public static string GetDefault()
        {
            return $"Wow, I couldn't understand that {Environment.NewLine}{Environment.NewLine} for speaking with a human contact <a href='tg://user?id=REPLACE_WITH_YOUR_VALUE'>@REPLACE_WITH_YOUR_VALUE</a> ";
        }

        public static string GetJoinMessage()
        {
            return
                $"You can pay in BTC or ETH:{Environment.NewLine} {Environment.NewLine}" +
                $"Pay 0.03 BTC to the following address:{Environment.NewLine}" +
                $"<code>13ZM4sdGo7DS9fcFDcdpZvcUJx46Tj22iom223</code>{Environment.NewLine} {Environment.NewLine}" +
                $"OR if you preffer Pay 0.0021 ETH to the following address:{Environment.NewLine}" +
                $"<code>0xc0ffee254729296a45a3885639AC7E10F9d54979</code>{Environment.NewLine} {Environment.NewLine}" +
                $"Paste your transaction id here and I will get your subscription Ready {Environment.NewLine}" +
                $"((As this is a <b>demo</b> paste any word with more than 10 letters ex <i>'asdfasdf12'</i>)) {Environment.NewLine}{Environment.NewLine}";
        }

        public static string SpeakWithHuman()
        {
            return @"Please contact <a href='tg://user?id=REPLACE_WITH_YOUR_VALUE'>@REPLACE_WITH_YOUR_VALUE</a> for more information. See you soon. ";
        }

        public static string GetYouHaveJoinedMessaged()
        {
            return $"Thank you. We have received your transaction. {Environment.NewLine} {Environment.NewLine}" +
                   $"It is a pleasure to comunicate that I have already add you to our private channel <b>CryptoTraders</b> and your can start just now {Environment.NewLine} {Environment.NewLine}" +
                   $"As I promise just bellow you can find our exclusive Ebook <i>'3 Keys for Trading Signals'</i> {Environment.NewLine} ";
        }


        public static string GetChoosePaymentOptionMessage()
        {
            return
                $"If you want to join just {Environment.NewLine}" +
                $"choose your payment option below:{Environment.NewLine}" +
                $"<i>(only 'Litecoin test' accepted in this demo, accepted over 800 altcoins)</i>{Environment.NewLine}";
        }

        public static string GetYouHaveSelectedCurrency()
        {
            return
                $"You have selected [BuyerCurrency] as your currency, choose your subscription below:{Environment.NewLine}";
        }

        public static string GetProccesingYourPayment()
        {
            return
                $"📢 \n" +
                $"We are proccessing your payment for your [ItemName] soon you will be given access to our services.{Environment.NewLine}";
        }

        public static string GetReceivedYourPayment()
        {
            return
                $"📢 \n" +
                $"We have received your payment of [ReceivedAmount] [BuyerCurrency] for your [ItemName] we are giving you access to our services.{Environment.NewLine}";
        }

        public static string GetSubscriptionExpired()
        {
            return
                $"📢 \n" +
                $"Your subscription [ItemName] has expired 🙁 \n" +
                $"If you wish to continue using our services please renew it.";
        }

        public static string GetPaymentInforMessage()
        {
            return $"📌 To complete your subscription you need to send a payment to the following address: \n \n" +
                   $"👉 <code>[WalletAddress]</code> \n\n" +
                   $"Send <b> [BuyerAmount] [BuyerCurrency] </b> \n\n" +
                   $"We will automatically process your payment ⏰ (normally takes 10 minutes)";
        }

        public static string GetCryptoPackageInfoMessage()
        {
            return
                $"✅ Team picked Crypto Trading Signals.\n" +
                $"  🔸 We always aim to provide quality trade investments, rather than an overload of market moves.\n\n" +
                $"✅ Highly informative coin recommendations based upon and displayed as fundamental analysis, technical analysis, and market sentiment.\n" +
                $"  🔸 Plus you will receive many important market and news updates for these picks.\n\n" +
                $"✅ Short, medium, and long-term buy signals released to you hours before it is ever announced to our free channels.\n" +
                $"  🔸 This means you can get in on the price action before others and/or have greater time to evaluate the call.\n\n" +
                $"✅ Explanatory chart calls 📊.\n" +
                $"  🔸 As you follow us, you will naturally begin to understand how to analyze charts, to see the reasoning in our calls and to learn many skills such as optimal entry and exit points to maximize your profit and coin holdings\n\n" +
                $"✅ Detailed information about exciting ICOs.\n" +
                $"  🔸 Hear about interesting and quality new projects to be launched, so you can consider investing before public marketplace trading. Access to private sales.\n\n" +
                $"✅ Online training and courses. ✏\n\n" +
                $"✅ Live Trading Sessions 💻\n\n" +
                $"✅ Bitmex Signals\n";
        }

        public static string GetFaqMessage()
        {
            return $"1⃣. Why choose us?\n\n" +
                    $"CRZ Traders have been giving solid trading ideas and developed unique ranking systems in cooperation with other analysts and professional traders that will help you to figure out when is the best time to enter the market, what coins will bring the greatest profit and how long it will take with risk management in mind.\n\n" +
                    $"2⃣. How will I get trading ideas ?\n\n" +
                    $"Since we dont have any specific app, we give our trading ideas in a private telegram channels.\n\n" +
                    $"3⃣. What packages do CRZ offer?\n\n" +
                    $"CRZ offers 2 Packages :\n" +
                    $"Crypto Package : Standard Package and Advanced Package\n" +
                    $"Forex Package : Forex Package\n\n" +
                    $"4⃣. What will I receive in Crypto Package ?\n\n" +
                    $"Without restriction you will get trading tips on general analytics, daily crypto calender, News, Bitcoin and market analytics.\n\n" +
                    $"5⃣. How to apply for a package subscription?\n\n" +
                    $"Using this bot go to the main menu and click the package you are interested.\n\n" +
                    $"6⃣. What else will I receive in Crypto/Forex package ?\n\n" +
                    $"CRZ Academy program is also included .Mainly written for Crypto program as of now. It consists of Beginner, Intermediate and Advanced Courses. More info contact us on customer service.\n\n" +
                    $"7⃣. During the subscription payment I receive the message: “Invalid payment amount”\n\n" +
                    $"Write a support request for further details, by contacting @crzsupport on telegram or sending e-mail to user@example.invalid\n\n" +
                    $"8⃣. How can I pay for a subscription other than Crypto ?\n\n" +
                    $"We accept only crypto currency (BTC,ETH,LTC,BCH) as a payment solution. We will be adding fiat currency payments in the future.\n\n" +
                    $"9⃣. What signals format do you provide?\n\n" +
                    $"We have 2 sorts of formats for signals:\n\n" +
                    $"👉 Standard  – Text with coin name, duration , entry, stop loss and take profits.\n" +
                    $"👉 Advanced – Picture with the name of the coin, duration entry, stop loss and take profits.\n\n" +
                    $"1⃣0⃣. How often do the signals/trading ideas come?\n\n" +
                    $"Signals come every day, 24/7 without any days off and daily up to 5 signals.\n";
        }

        public static string GetForexPackageInfoMessage()
        {
            return
                $"✅ Team picked Forex 📈 Trading Signals.\n" +
                $"  🔸 We always aim to provide quality trade investments, rather than an overload of market moves.\n\n" +
                $"✅ Highly informative companies recommendations based upon and displayed as fundamental analysis, technical analysis, and market sentiment.\n" +
                $"  🔸 Plus you will receive many important market and news updates for these picks.\n\n" +
                $"✅ Short, medium, and long-term buy signals released to you hours before it is ever announced to our free channels.\n" +
                $"  🔸 This means you can get in on the price action before others and/or have greater time to evaluate the call.\n\n" +
                $"✅ Explanatory chart calls 📊.\n" +
                $"  🔸 As you follow us, you will naturally begin to understand how to analyze charts, to see the reasoning in our calls and to learn many skills such as optimal entry and exit points to maximize your profit and coin holdings\n\n" +
                $"✅ Live webminars about FOREX startegies.\n" +
                $"  🔸 Hear about interesting and quality new projects to be launched, so you can consider investing before public marketplace trading. Access to private sales.\n\n" +
                $"✅ Live Trading Sessions\n";
        }
    }

    public static class DemoForexBot
    {
        private const string StartMessage = "/start";
        private const string WriteEmailAddress = "Please write your email:";
        private const string MainMenu = "Below you can choose from the menu <b>CRZ SERVICES</b>:";
        private const string SelectedSubscription = "SELECTED SUBSCRIPTION";
        private const string SelectedSubscriptionDuration = "SELECTED SUBSCRIPTION DURATION";
        private const string SelectedBuyerCurrency = "BuyerCurrency";

        public static Funnel GetFunnel()
        {
            var MenuButtons = new List<List<string>>()
            {
                new List<string>() {"CRZ FOREX PACKAGE", "CRZ CRYPTO PACKAGE"},
                new List<string>() {"FAQ", "FEEDBACK/RESULTS"},
                new List<string>() {"MY ACCOUNT"}
            };

            var result = new Funnel();

            AddGettingsClientDataAndShowingMenu(result, MenuButtons);

            AddMyAccountsMenu(result, MenuButtons);

            AddForexPackage(result, MenuButtons);

            AddForexPaymentComplete(result);

            AddCryptoPackage(result, MenuButtons);

            AddCryptoPaymentComplete(result);

            result.MessageActions.Add(new MessageTrigger("FAQ"), new List<FunnelAction>()
            {
                new ImageActionAsync("FAQ.jpg"),
                new MessageButtonAction()
                {
                    Message = DemoForexBotChat.GetFaqMessage(),
                    Buttons = new List<List<string>>()
                    {
                        new List<string>() {"↩ RETURN"}
                    },
                    Delay = 100,
                    RemovePreviousMessage = false
                }
            });

            result.MessageActions.Add(new MessageTrigger("FEEDBACK/RESULTS"), new List<FunnelAction>()
            {
                new ImageActionAsync("feedback.jpg"),
                new ImageActionAsync("feedback1.png", false, 100),
                new ImageActionAsync("feedback2.jpg", false, 100),
                new ImageActionAsync("feedback3.jpg", false, 100),
                new ImageActionAsync("feedback4.jpg", false, 100),
                new MessageButtonAction()
                {
                    Message = "👉 For more testimonies or results <b>contact</b> us user@example.invalid 📧",
                    Buttons = new List<List<string>>()
                    {
                        new List<string>() {"↩ RETURN"}
                    },
                    Delay = 100,
                    RemovePreviousMessage = false
                }
            });

            result.EventActions.Add(new MessageTrigger("SubscriptionExpired"),
                new List<FunnelAction>() { new MessageAction(DemoForexBotChat.GetSubscriptionExpired(), false) });


            foreach (var coin in new List<string>() { "Ethereum", "Bitcoin", "BitcoinCash", "Ripple", "Iota" })
            {
                result.CallbackActions.Add(new MessageTrigger(coin), new List<FunnelAction>()
                {
                    new MessageButtonAction()
                    {
                        Message = DemoForexBotChat.GetChoosePaymentOptionMessage(),
                        Buttons = new List<List<string>>()
                        {
                            new List<string>() {"Litecoin Test", "Ethereum"},
                            new List<string>() {"Bitcoin", "BitcoinCash"},
                            new List<string>() {"Ripple", "Iota"},
                            new List<string>(){ "↩ RETURN" }
                        }
                    }
                });
            }
            return result;
        }

        private static void AddForexPaymentComplete(Funnel result)
        {
            int status1 = 1;
            int status100 = 100;

            var properties1 = new PropertiesTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status1},
                {"ItemName", "CRZ FOREX PACKAGE SUBSCRIPTION"}
            })
            {
                Priority = 100
            };

            result.EventActions.Add(properties1, new List<FunnelAction>()
            {
                new MessageAction(DemoForexBotChat.GetProccesingYourPayment())
            });

            var received1MonthForex = new PropertiesTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status100},
                {"ReceivedAmount", 0.01},
                {"ItemName", "CRZ FOREX PACKAGE SUBSCRIPTION"}
            })
            {
                Priority = 100
            };

            result.EventActions.Add(received1MonthForex, new List<FunnelAction>()
            {
                new MessageAction(DemoForexBotChat.GetReceivedYourPayment()),
                new SubscriptionCreateAction()
                {
                    Currency = "LTCT",
                    Fee = 0.01m,
                    DaysDuration = 0,
                    MonthsDuration = 1,
                    Name = "CRZ FOREX PACKAGE"
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 1346179174,
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 1383906823,
                }
            });

            var received3MonthForex = new PropertiesTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status100},
                {"ReceivedAmount", 0.027},
                {"ItemName", "CRZ FOREX PACKAGE SUBSCRIPTION"}
            })
            {
                Priority = 100
            };

            result.EventActions.Add(received3MonthForex, new List<FunnelAction>()
            {
                new MessageAction(DemoForexBotChat.GetReceivedYourPayment()),
                new SubscriptionCreateAction()
                {
                    Currency = "LTCT",
                    Fee = 0.027m,
                    DaysDuration = 0,
                    MonthsDuration = 3,
                    Name = "CRZ FOREX PACKAGE"
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 1346179174,
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 1383906823,
                }
            });

            var received1YearForex = new PropertiesTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status100},
                {"ReceivedAmount", 0.1},
                {"ItemName", "CRZ FOREX PACKAGE SUBSCRIPTION"}
            })
            {
                Priority = 100
            };

            result.EventActions.Add(received1YearForex, new List<FunnelAction>()
            {
                new MessageAction(DemoForexBotChat.GetReceivedYourPayment()),
                new SubscriptionCreateAction()
                {
                    Currency = "LTCT",
                    Fee = 0.1m,
                    DaysDuration = 0,
                    MonthsDuration = 12,
                    Name = "CRZ FOREX PACKAGE"
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 1346179174,
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 1383906823,
                }
            });
        }

        private static void AddForexPackage(Funnel result, List<List<string>> MenuButtons)
        {
            result.MessageActions.Add(new MessageTrigger("CRZ FOREX PACKAGE"), new List<FunnelAction>()
            {
                new ImageActionAsync("Forex.jpg"),
                new MessageAction(DemoForexBotChat.GetForexPackageInfoMessage(),false,100),
                new MessageButtonAction()
                {
                    Message = DemoForexBotChat.GetChoosePaymentOptionMessage(),
                    Buttons = new List<List<string>>()
                    {
                        new List<string>() {"Litecoin Test", "Ethereum"},
                        new List<string>() {"Bitcoin", "BitcoinCash"},
                        new List<string>() {"Ripple", "Iota"},
                        new List<string>(){ "↩ RETURN" }
                    },
                    Delay = 100,
                    RemovePreviousMessage = false
                },
                new StateUpdateAction(SelectedSubscription, "CRZ FOREX PACKAGE")
            });

            var mainMenu = new List<FunnelAction>()
            {
                new ImageActionAsync("MainMenu.jpg"),
                new MessageAction("CRZ Traders ®",false,100),
                new MessageMenuButtonAction(MainMenu, MenuButtons,false,100),
                new StateUpdateAction(SelectedSubscription, ""),
                new StateUpdateAction(SelectedSubscriptionDuration, ""),
                new StateUpdateAction(SelectedBuyerCurrency, "")
            };

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 90,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("↩ RETURN"),
                }
            }, mainMenu);

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 90,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("Litecoin Test"),
                    new StateIsValueTrigger(SelectedSubscription, "CRZ FOREX PACKAGE")
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction(SelectedBuyerCurrency, "LTCT"),
                new MessageButtonAction()
                {
                    RemovePreviousMessage = true,
                    Message = DemoForexBotChat.GetYouHaveSelectedCurrency(),
                    Buttons = new List<List<string>>()
                    {
                        new List<string>() {"1 Month: 0.01 LTCT"},
                        new List<string>() {"3 Months: 0.027 LTCT (10% discount)"},
                        new List<string>() {"1 Year: 0.1 LTCT (2 months free)"},
                        new List<string>(){ "↩ RETURN" }
                    }
                }
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 90,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("1 Month: 0.01 LTCT"),
                    new StateIsValueTrigger(SelectedSubscription, "CRZ FOREX PACKAGE"),
                    new StateIsNotValueEmpty(SelectedBuyerCurrency)
                }
            }, new List<FunnelAction>()
            {
                new PaymentInfoCreateActionAsync()
                {
                    Amount = 0.01,
                    OriginalCurrency = "LTCT",
                    BuyerCurrency = "LTCT",
                    ItemName = "CRZ FOREX PACKAGE SUBSCRIPTION",
                    PaymentProvider = PaymentProvider.Coinpayments,
                    MonthsDuration = 1
                },
                new MessageAction(DemoForexBotChat.GetPaymentInforMessage()),
                new MessageMenuButtonAction(MainMenu, MenuButtons,false,100),
                new StateUpdateAction(SelectedSubscription, ""),
                new StateUpdateAction(SelectedSubscriptionDuration, ""),
                new StateUpdateAction(SelectedBuyerCurrency, "")
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 90,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("3 Months: 0.027 LTCT (10% discount)"),
                    new StateIsValueTrigger(SelectedSubscription, "CRZ FOREX PACKAGE"),
                    new StateIsNotValueEmpty(SelectedBuyerCurrency)
                }
            }, new List<FunnelAction>()
            {
                new PaymentInfoCreateActionAsync()
                {
                    Amount = 0.027,
                    OriginalCurrency = "LTCT",
                    BuyerCurrency = "LTCT",
                    ItemName = "CRZ FOREX PACKAGE SUBSCRIPTION",
                    PaymentProvider = PaymentProvider.Coinpayments,
                    MonthsDuration = 3
                },
                new MessageAction(DemoForexBotChat.GetPaymentInforMessage()),
                new MessageMenuButtonAction(MainMenu, MenuButtons,false,100),
                new StateUpdateAction(SelectedSubscription, ""),
                new StateUpdateAction(SelectedSubscriptionDuration, ""),
                new StateUpdateAction(SelectedBuyerCurrency, "")
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 90,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("1 Year: 0.1 LTCT (2 months free)"),
                    new StateIsValueTrigger(SelectedSubscription, "CRZ FOREX PACKAGE"),
                    new StateIsNotValueEmpty(SelectedBuyerCurrency)
                }
            }, new List<FunnelAction>()
            {
                new PaymentInfoCreateActionAsync()
                {
                    Amount = 0.1,
                    OriginalCurrency = "LTCT",
                    BuyerCurrency = "LTCT",
                    ItemName = "CRZ FOREX PACKAGE SUBSCRIPTION",
                    PaymentProvider = PaymentProvider.Coinpayments,
                    MonthsDuration = 12
                },
                new MessageAction(DemoForexBotChat.GetPaymentInforMessage()),
                new MessageMenuButtonAction(MainMenu, MenuButtons,false,100),
                new StateUpdateAction(SelectedSubscription, ""),
                new StateUpdateAction(SelectedSubscriptionDuration, ""),
                new StateUpdateAction(SelectedBuyerCurrency, "")
            });
        }

        private static void AddCryptoPackage(Funnel result, List<List<string>> MenuButtons)
        {
            result.MessageActions.Add(new MessageTrigger("CRZ CRYPTO PACKAGE"), new List<FunnelAction>()
            {
                new ImageActionAsync("Crypto.jpg"),
                new MessageAction(DemoForexBotChat.GetCryptoPackageInfoMessage(),false,100),
                new MessageButtonAction()
                {
                    Message = DemoForexBotChat.GetChoosePaymentOptionMessage(),
                    Buttons = new List<List<string>>()
                    {
                        new List<string>() {"Litecoin Test", "Ethereum"},
                        new List<string>() {"Bitcoin", "BitcoinCash"},
                        new List<string>() {"Ripple", "Iota"},
                        new List<string>(){ "↩ RETURN" }
                    },
                    Delay = 100,
                    RemovePreviousMessage = false
                },
                new StateUpdateAction(SelectedSubscription, "CRZ CRYPTO PACKAGE")
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 90,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("Litecoin Test"),
                    new StateIsValueTrigger(SelectedSubscription, "CRZ CRYPTO PACKAGE")
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction(SelectedBuyerCurrency, "LTCT"),
                new MessageButtonAction()
                {
                    Message = DemoForexBotChat.GetYouHaveSelectedCurrency(),
                    Buttons = new List<List<string>>()
                    {
                        new List<string>() {"1 Month: 0.015 LTCT"},
                        new List<string>() {"3 Months: 0.04 LTCT (10% discount)"},
                        new List<string>() {"1 Year: 0.15 LTCT (2 months free)"},
                        new List<string>(){ "↩ RETURN" }
                    }
                }
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 90,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("1 Month: 0.015 LTCT"),
                    new StateIsValueTrigger(SelectedSubscription, "CRZ CRYPTO PACKAGE"),
                    new StateIsNotValueEmpty(SelectedBuyerCurrency)
                }
            }, new List<FunnelAction>()
            {
                new PaymentInfoCreateActionAsync()
                {
                    Amount = 0.015,
                    OriginalCurrency = "LTCT",
                    BuyerCurrency = "LTCT",
                    ItemName = "CRZ CRYPTO PACKAGE SUBSCRIPTION",
                    PaymentProvider = PaymentProvider.Coinpayments,
                    MonthsDuration = 1
                },
                new MessageAction(DemoForexBotChat.GetPaymentInforMessage()),
                new MessageMenuButtonAction(MainMenu, MenuButtons,false,100),
                new StateUpdateAction(SelectedSubscription, ""),
                new StateUpdateAction(SelectedSubscriptionDuration, ""),
                new StateUpdateAction(SelectedBuyerCurrency, "")
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 90,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("3 Months: 0.04 LTCT (10% discount)"),
                    new StateIsValueTrigger(SelectedSubscription, "CRZ CRYPTO PACKAGE"),
                    new StateIsNotValueEmpty(SelectedBuyerCurrency)
                }
            }, new List<FunnelAction>()
            {
                new PaymentInfoCreateActionAsync()
                {
                    Amount = 0.04,
                    OriginalCurrency = "LTCT",
                    BuyerCurrency = "LTCT",
                    ItemName = "CRZ CRYPTO PACKAGE SUBSCRIPTION",
                    PaymentProvider = PaymentProvider.Coinpayments,
                    MonthsDuration = 3
                },
                new MessageAction(DemoForexBotChat.GetPaymentInforMessage()),
                new MessageMenuButtonAction(MainMenu, MenuButtons,false,100),
                new StateUpdateAction(SelectedSubscription, ""),
                new StateUpdateAction(SelectedSubscriptionDuration, ""),
                new StateUpdateAction(SelectedBuyerCurrency, "")
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 90,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("1 Year: 0.15 LTCT (2 months free)"),
                    new StateIsValueTrigger(SelectedSubscription, "CRZ CRYPTO PACKAGE"),
                    new StateIsNotValueEmpty(SelectedBuyerCurrency)
                }
            }, new List<FunnelAction>()
            {
                new PaymentInfoCreateActionAsync()
                {
                    Amount = 0.15,
                    OriginalCurrency = "LTCT",
                    BuyerCurrency = "LTCT",
                    ItemName = "CRZ CRYPTO PACKAGE SUBSCRIPTION",
                    PaymentProvider = PaymentProvider.Coinpayments,
                    MonthsDuration = 12
                },
                new MessageAction(DemoForexBotChat.GetPaymentInforMessage()),
                new MessageMenuButtonAction(MainMenu, MenuButtons,false,100),
                new StateUpdateAction(SelectedSubscription, ""),
                new StateUpdateAction(SelectedSubscriptionDuration, ""),
                new StateUpdateAction(SelectedBuyerCurrency, "")
            });
        }

        private static void AddCryptoPaymentComplete(Funnel result)
        {
            int status1 = 1;
            int status100 = 100;

            var properties1 = new PropertiesTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status1},
                {"ItemName", "CRZ CRYPTO PACKAGE SUBSCRIPTION"}
            })
            {
                Priority = 100
            };

            result.EventActions.Add(properties1, new List<FunnelAction>()
            {
                new MessageAction(DemoForexBotChat.GetProccesingYourPayment())
            });

            var received1MonthCRYPTO = new PropertiesTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status100},
                {"ReceivedAmount", 0.015},
                {"ItemName", "CRZ CRYPTO PACKAGE SUBSCRIPTION"}
            })
            {
                Priority = 100
            };

            result.EventActions.Add(received1MonthCRYPTO, new List<FunnelAction>()
            {
                new MessageAction(DemoForexBotChat.GetReceivedYourPayment()),
                new SubscriptionCreateAction()
                {
                    Currency = "LTCT",
                    Fee = 0.015m,
                    DaysDuration = 0,
                    MonthsDuration = 1,
                    Name = "CRZ CRYPTO PACKAGE"
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 307005531,
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 1327897835,
                }
            });

            var received3MonthCRYPTO = new PropertiesTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status100},
                {"ReceivedAmount", 0.04},
                {"ItemName", "CRZ CRYPTO PACKAGE SUBSCRIPTION"}
            })
            {
                Priority = 100
            };

            result.EventActions.Add(received3MonthCRYPTO, new List<FunnelAction>()
            {
                new MessageAction(DemoForexBotChat.GetReceivedYourPayment()),
                new SubscriptionCreateAction()
                {
                    Currency = "LTCT",
                    Fee = 0.04m,
                    DaysDuration = 0,
                    MonthsDuration = 3,
                    Name = "CRZ CRYPTO PACKAGE"
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 307005531,
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 1327897835,
                }
            });

            var received1YearCRYPTO = new PropertiesTrigger(new Dictionary<string, object>()
            {
                {"PaymentStatus", status100},
                {"ReceivedAmount", 0.15},
                {"ItemName", "CRZ CRYPTO PACKAGE SUBSCRIPTION"}
            })
            {
                Priority = 100
            };

            result.EventActions.Add(received1YearCRYPTO, new List<FunnelAction>()
            {
                new MessageAction(DemoForexBotChat.GetReceivedYourPayment()),
                new SubscriptionCreateAction()
                {
                    Currency = "LTCT",
                    Fee = 0.15m,
                    DaysDuration = 0,
                    MonthsDuration = 12,
                    Name = "CRZ CRYPTO PACKAGE"
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 307005531,
                },
                new ChannelAddMyselfActionAsync()
                {
                    ChannelId = 1327897835,
                }
            });
        }

        private static void AddMyAccountsMenu(Funnel result, List<List<string>> MenuButtons)
        {
            result.MessageActions.Add(new MessageTrigger("RETURN"), new List<FunnelAction>()
            {
                new MessageMenuButtonAction(MainMenu, MenuButtons),
                new StateUpdateAction(SelectedSubscription, ""),
                new StateUpdateAction(SelectedSubscriptionDuration, ""),
                new StateUpdateAction(SelectedBuyerCurrency, "")
            });

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 98,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("MY ACCOUNT"),
                    new HasNotSubscriptionTagTrigger()
                }
            }, new List<FunnelAction>()
            {
                new ImageActionAsync("Account.jpg"),
                new MessageAction("You have no subscriptions."),
                new MessageMenuButtonAction(MainMenu, MenuButtons)
            });

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 98,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("MY ACCOUNT"),
                    new HasSubscriptionTagTrigger()
                }
            }, new List<FunnelAction>()
            {
                new ImageActionAsync("Account.jpg"),
                new MessageShowSubscriptionsAction(),
                new MessageMenuButtonAction(MainMenu, MenuButtons)
            });
        }

        private static void AddGettingsClientDataAndShowingMenu(Funnel result, List<List<string>> MenuButtons)
        {
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(StartMessage),
                    new HasNotClientCreatedTagTrigger()
                }
            }, new List<FunnelAction>()
            {
                new ClientCreateAction()
            });

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 99,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(StartMessage),
                    new HasNotEmailTagTrigger()
                }
            }, new List<FunnelAction>()
            {
                new MessageInputAction(WriteEmailAddress)
            });

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new ReplyMessageTrigger(WriteEmailAddress),
                }
            }, new List<FunnelAction>()
            {
                new EmailUpdateAction(),
                new MessageMenuButtonAction(MainMenu, MenuButtons)
            });

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 98,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(StartMessage),
                    new HasClientCreatedTagTrigger(),
                    new HasEmailTagTrigger(),
                }
            }, new List<FunnelAction>()
            {
                new ImageActionAsync("MainMenu.jpg"),
                new MessageAction("CRZ Traders ®",false,100),
                new MessageMenuButtonAction(MainMenu, MenuButtons,false,100),
            });
        }

    }
}
