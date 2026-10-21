using System.Collections.Generic;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;

namespace Mentor.telegram.bot.api.Services.Chats
{
    public class SalesBotChat
    {
        public static string Question1 = "▫ To understand your situation. How big is your team?";
        public static string Q1_Solo = "Solo Team";
        public static string Q1_2People = "2 People";
        public static string Q1_3_5People = "3-5 People";
        public static string Q1_5plus = "More than 5";
        public static string Question2 = "How many members do you have in your premium channel?";
        public static string Q2_lessThan20 = "Less than 20";
        public static string Q2_between20_50 = "20 - 50";
        public static string Q2_between50_100 = "50 - 100";
        public static string Q2_between100_200 = "100 - 200";
        public static string Q2_between200_500 = "200 - 500";
        public static string Q2_moreThan500 = "More than 500";
        public static string CleverEnough = "☝ Even if you close your conversation here and start again I am clever enough to remember your answers and won't ask you again. ☺";
        public static string Continue = "Continue...";
        public static string ShowMe = "Show me 👀";
        public static string GotIt = "🆒 Got it.";
        public static string TellMe = "Tell me";
        public static string More = "More...";
        public static string SequenceId = "Sec3SequenceId";
        public static string LeadMagnet = "Lead magnet❓";
        public static string ToSection3 = "👉 To section 3⃣";
        public static string ToMainMenu = "👉 To Main menu";
        public static string TheGift = "Open de Gift 🎁";
        public static string TheOffer = "Launching Offer 🚀";
        public static string DemoBot = "See bot 👾";
        public static string AboutMe = "About Me 🚶";
        public static string Return = "↩ RETURN";


        public static string LeadGeneration = "👉 Let's get into business \n\n" +
                                              "You already have a product or service, it is great but it is difficult getting new customers. You think people might trust more this other channels that has 10k more users than yours right? \n\n" +
                                              "What can you do? \n\n";
        
        public static string ResponseLeadGeneration =
            "👉 I will give you the response sharing this <a href='https://bit.ly/2OPSbvj'>image</a> with you \n\n";
        public static string ExplanationLeadGeneration = 
            "I totally agree with Tony and we base our strategy in this principle. So, how can you apply this?. \n\n "+
            "You can do this in 3 Steps proven formula (1 step per day): \n\n" +
            "▫ 1. Offer to potential clients a <b>Lead magnet</b> and invite them to your public channel.\n\n" +
            "▫ 2. <b>Show them</b> who you are, why you do what you, your results, testimonies etc. \n\n" +
            "▫ 3. Give them <b>more value</b> another lead magnet, something they can really apply and benefit from.\n\n" +
            "Is that all? \n\nYes, at this point your client is what we call <i>warm traffic</i>, they know you, they have had value," +
            "they trust you and it is in our nature when we receive to give back. So, don't wait, <b>present your offer</b> 📝\n\n";

        public static string WhatIsALeadMagnet =
            "A <a href='https://www.antevenio.com/wp-content/uploads/2018/07/lead-magnet.jpg'>Lead magnet</a> is an incentive that marketers offer to potential buyers in exchange for their email address, or other contact information.\n\n" +
            " Lead magnets usually offer a piece of digital, downloadable content, " +
            "such as a free PDF checklist, report, eBook, whitepaper, video, etc. In our case we use it to get buyers into our public channel and build trust 😉";

        public static string Pause =
            "Let's make a pause, you have been given the formula 💎 that will make you multiply your sales x3 💰.\n\n" +
            "Take 5 minutes and calmy think how can you implement this for your business.\n Just contact me if you have any question @REPLACE_WITH_YOUR_VALUE \n\n";

        public static string Section3Presentation =
            "July 2018 I was working with a client who is a Solo Team ☝.\n\n" +
            "He goes into the market every day ⏰ looking for the best " +
            "opportunities ⚡, he does the customer service, solving ❓ questions , he gets people into the  premium, he has to send reminders of payments and he was struggling ☔.\n\n " +
            "First thing we did was to create an onboarding process \n\n" +
            "ℹ How we did it? 👇 bellow you have the bare minimum tasks: \n" +
            "▫ Create client onboarding workflow with Draw.io.\n" + 
            "▫ Create FAQ.\n" +
            "▫ Set up Google sheet to account subscriptions and payments.\n" +
            "▫ Create a welcome message template.\n" +
            "▫ Create a payment reminder template.\n" +
            "▫ Create a service descrition document.\n\n" +
            "You can stop here and you are in a better position, but why conform to this when you can automate everything?\n\n" +
            "So we did it and now my client life is ☀☀☀\n\n" +
            "What I am going to demo you, is the final result <b>automated onboarding process for clients</b>.";

        public static string SequencePresentation =
            "Imagine you are a client and open up your bot, " +
            "the first thing you see is the <a href='https://image.ibb.co/idXyAe/01_Presentation.png'>presentation</a>.\n\n" +
            "This is your only opportunity to Grab the attention 👀 of your client.";

        public static string SequenceGetClientEmail =
            "Secondly we ask to the client for his 📧 <a href='https://image.ibb.co/ktZpGK/02_Get_Client_Email.png'>email</a>, " +
            "that later we can use for notifications, offers etc.";
        public static string SequenceShowMenu =
            "Then now we can show the <a href='https://image.ibb.co/ex9Ziz/03_Main_Menu.png'>Menu</a>, " +
            "with all the actions our client can do.";

        public static string CheckSubscriptions =
            "For example, <a href='https://preview.ibb.co/niCWVe/04_Check_My_Subscriptions.png'>My Account</a>, " +
            "is about retrieving information about your subscriptions.\n\n We don't have any, let's create" +
            "one. We press <b>CRZ Crypto Package</b>";

        public static string CheckCryptoPackage =
            "We tell our customer about our <a href='https://preview.ibb.co/kqYGwK/05_Check_Crypto_Traders_Section_And_Click_LTCT.png'>CRZ Crypto</a> service, " +
            "and as they want to buy our service 😎 they select a payment option";

        public static string SequenceSelectSubscriptionDuration =
            "Now they select 3 Months <a href='https://image.ibb.co/bEM3bK/06_Choose3_Months_Subscription.png'>subscription</a>";

        public static string PresentPaymentInformation =
            "A message shows displaying the payment details <a href='https://image.ibb.co/kjSWVe/07_Get_Payment_Details.png'>payment details</a>\n\n" +
            "▫ It is more secure as a new address is generated each time\n";

        public static string SendPayment =
            "When the client send the payment 💵 you don't need to chase or ask for transactionId.\n\n" +
            "▫ The system automatically check when payment is completed\n" +
            "▫ You receive notifications about payments at your email.\n\n";

        public static string ForAccountability =
            "You can track 💻 payments in the <a href='https://image.ibb.co/jJC5rU/Control_Panel.png'>control panel</a>.\n";
        
        public static string WhenPaymentIsCompleted =
            "When the payment has been proccess the customer receive a 🔔 <a href='https://image.ibb.co/iz3S3z/12_Received_Payment_Update2.png'>notification</a>" +
            " and gets automatically added to the premium channel.\n\n";

        public static string FAQSection =
            "Don't forget that 80% of customer support can be solved with a well written 💁 FAQ <a href='https://image.ibb.co/csEBVe/08_Check_FAQ.png'>FAQ</a>\n\n";

        public static string FAQSection2 =
            "80% of customer support can be solved with a well written 💁 FAQ <a href='https://image.ibb.co/jU6Eiz/09_Check_FAQ2.png'>FAQ</a>\n\n";

        public static string Testimonies =
            "Don't be shy and show what your customers tell about you 👌 with the <a href='https://image.ibb.co/jDzdAe/10_Check_Testimonies.png'>Testimonies section</a>\n\n";

        public static string ToSumUp =
            "To sum up 👇 bellow are <b>benefits of bot automation</b>:\n\n" +
            "✅ Reduce customer support by 80%\n" +
            "✅ Multiply your conversion rate x2 x3\n" +
            "✅ Close sales while sleeping. Always Online 24/7\n" +
            "✅ Connect with your audience, delight your customers\n" +
            "✅ Get A Head-Start Over Your Competitors\n\n";

        public static string SoWhatIsNext =
            "So what is next?:\n\n" +
            "▫ I have a reward for you. Find it at the main menu\n" +
            "▫ I hope you have lots of success with your business\n";

        public static string MainMenuText =
            "🔸 What else do you want to know? \nYou can always contact me at @REPLACE_WITH_YOUR_VALUE";

        public static string TheGiftText =
            "🎁 As I promised here is my gift for you: \n\n" +
            "✅ I am offering you for FREE 30 minutes of consultancy services about your premium channel.\n" +
            "✅ We can talk about marketing, lead generation, how to improve your process or any other puzzle you might have in your business\n" +
            "✅ No strings attached, totally FREE.\n" +
            "✅ Send me a message at @REPLACE_WITH_YOUR_VALUE and we can find a suitable time for both of us.\n\n";

        public static string AboutMeText =
            "I am Alberto, from Spain I have worked as Software Cosultant for more than 10 years. \n\nI have helped " +
            "big companies and small ones in banking, media and ecommerce (Just Eat, Bookatable, Dealogic ...). \n\n" +
            "2 Years ago I quit my full time job to pursue my <a href='http://www.jibit.com/content/uploads/entrepreneur-quote1.jpg'>entepreneur dream</a>," +
            " this is my second project and I " +
            "hope is NOT the last one. \n\nI wish you all the success in your business because as you I know starting " +
            "your own business is not the easy path.\n\n.";

        public static string TheOfferText =
            "I have been advised not to do this but I really wanted to start with a great launching offer\n\n" +
            "We offer 2 type of bots:\n\n" +
            "▫ <b>Normal bot</b> 🗿 the cost of building it is £999 + an average monthly fee of £24,99\n" +
            "▫ <b>Advance bot</b> 👾 the cost of building it can vary between £4999-£9999 + an average monthly fee of £49,99\n\n" +
            "The montly fee is calculated based on the number of users 👨 of your channel\n\n" +
            "For the first 5 people to contact me I want to build them the bot for FREE, so they will only pay the monthly fee. You get to choose the normal bot or the advance bot. \n\n" +
            "<b>Update:</b>At the time of writting this there are 2 slots left.I am Sorry if they all get booked, but I cannot give more, so be quick. ⏳\n\n" +
            "<b>Contact me:</b> @REPLACE_WITH_YOUR_VALUE ☎";


        public static string DemoBotText = "If you want to <b>see the demo bot</b> you can talk to @Cryptopaymetestbot 👾 . Have fun!";
    }

    public static class SalesBot
    {
        public const string StartMessage = "/start";
        public const string StateStep = "Step";
        public const string StateQ1 = "Q1";
        public const string StateQ2 = "Q2";

        public static Funnel GetFunnel()
        {
            Sequence sequenceSec3 = new Sequence()
            {
                Id = SalesBotChat.SequenceId,
                StartingButton = SalesBotChat.ToSection3,
                FinalActions = new List<FunnelAction>()
                {
                    new StateUpdateAction("Sequence","Complete")
                },
                FirstActions = new List<FunnelAction>()
                {
                    new StateUpdateAction("Step","X")
                }
            };

            var result = new Funnel();

            AddQuestion1(result);

            AddQuestion2(result);

            AddSequencePart2(result);

            sequenceSec3.Steps = new List<Step>() {
                new Step()
                {
                    Message = SalesBotChat.Section3Presentation,
                    ButtonText = SalesBotChat.Continue
                },
                new Step()
                {
                    Message = SalesBotChat.SequencePresentation,
                    ButtonText = SalesBotChat.Continue
                },
                new Step()
                {
                    Message = SalesBotChat.SequenceGetClientEmail,
                    ButtonText = SalesBotChat.Continue
                },
                new Step()
                {
                    Message = SalesBotChat.SequenceShowMenu,
                    ButtonText = SalesBotChat.Continue
                },
                new Step()
                {
                    Message = SalesBotChat.CheckSubscriptions,
                    ButtonText = SalesBotChat.GotIt
                },
                new Step()
                {
                    Message = SalesBotChat.CheckCryptoPackage,
                    ButtonText = SalesBotChat.Continue
                },
                new Step()
                {
                    Message = SalesBotChat.SequenceSelectSubscriptionDuration,
                    ButtonText = SalesBotChat.More
                },
                new Step()
                {
                    Message = SalesBotChat.PresentPaymentInformation,
                    ButtonText = SalesBotChat.Continue
                },
                new Step()
                {
                    Message = SalesBotChat.SendPayment,
                    ButtonText = SalesBotChat.Continue
                },
                new Step()
                {
                    Message = SalesBotChat.ForAccountability,
                    ButtonText = SalesBotChat.Continue
                },
                new Step()
                {
                    Message = SalesBotChat.WhenPaymentIsCompleted,
                    ButtonText = SalesBotChat.GotIt
                },
                new Step()
                {
                    Message = SalesBotChat.FAQSection,
                    ButtonText = SalesBotChat.Continue
                },
                new Step()
                {
                    Message = SalesBotChat.Testimonies,
                    ButtonText = SalesBotChat.GotIt
                },
                new Step()
                {
                    Message = SalesBotChat.ToSumUp,
                    ButtonText = SalesBotChat.Continue
                },
                new Step()
                {
                    Message = SalesBotChat.SoWhatIsNext,
                    ButtonText = SalesBotChat.ToMainMenu
                }
            };

            result.AddSequence(sequenceSec3);

            AddMainMenu(result);

            return result;
        }

        private static void AddMainMenu(Funnel result)
        {
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new HasClientCreatedTagTrigger(),
                    new StateIsValueTrigger("Sequence", "Complete"),
                    new MessageIsNotTrigger(new List<string>
                    {
                        SalesBotChat.TheGift,
                        SalesBotChat.AboutMe,
                        SalesBotChat.DemoBot,
                        SalesBotChat.TheOffer
                    })
                }
            }, new List<FunnelAction>()
            {
                GetMainMenu()
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(SalesBotChat.Return),
                    new StateIsValueTrigger("Sequence", "Complete")
                }
            }, new List<FunnelAction>()
            {
                GetMainMenu()
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(SalesBotChat.ToMainMenu),
                    new StateIsValueTrigger("Sequence", "Complete")
                }
            }, new List<FunnelAction>()
            {
                GetMainMenu()
            });

            result.AddMenuText(SalesBotChat.TheGift, SalesBotChat.TheGiftText, SalesBotChat.Return,
                new StateIsValueTrigger("Sequence", "Complete"));
            result.AddMenuText(SalesBotChat.AboutMe, SalesBotChat.AboutMeText, SalesBotChat.Return,
                new StateIsValueTrigger("Sequence", "Complete"));
            result.AddMenuText(SalesBotChat.TheOffer, SalesBotChat.TheOfferText, SalesBotChat.Return,
                new StateIsValueTrigger("Sequence", "Complete"));
            result.AddMenuText(SalesBotChat.DemoBot, SalesBotChat.DemoBotText, SalesBotChat.Return,
                new StateIsValueTrigger("Sequence", "Complete"));
        }
        
        private static void AddSequencePart2(Funnel result)
        {
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new HasClientCreatedTagTrigger(),
                    new StateIsValueTrigger(StateStep, "2")
                }
            }, new List<FunnelAction>()
            {
                GetLeadGenerationMessage(true)
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(SalesBotChat.TellMe),
                    new StateIsValueTrigger(StateStep, "2")
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction(StateStep, "3"),
                GetResponseLeadGenerationMessage()
            });

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new HasClientCreatedTagTrigger(),
                    new StateIsValueTrigger(StateStep, "3")
                }
            }, new List<FunnelAction>()
            {
                GetExplanationLeadGenerationMessage()
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(SalesBotChat.Continue),
                    new StateIsValueTrigger(StateStep, "3")
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction(StateStep, "4"),
                GetExplanationLeadGenerationMessage()
            });

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new HasClientCreatedTagTrigger(),
                    new StateIsValueTrigger(StateStep, "4")
                }
            }, new List<FunnelAction>()
            {
                WhatIsALeadMagnetMessage()
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(SalesBotChat.LeadMagnet),
                    new StateIsValueTrigger(StateStep, "4")
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction(StateStep, "4"),
                WhatIsALeadMagnetMessage()
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(SalesBotChat.Continue),
                    new StateIsValueTrigger(StateStep, "4")
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction(SalesBotChat.SequenceId, "0"),
                LetsHaveAPauseMessage()
            });
        }

        private static MessageMenuButtonAction GetMainMenu(bool removePrevious = true)
        {
            return new MessageMenuButtonAction(SalesBotChat.MainMenuText, new List<List<string>>()
            {
                new List<string>()
                    {SalesBotChat.TheGift, SalesBotChat.TheOffer},
                new List<string>()
                    {SalesBotChat.DemoBot, SalesBotChat.AboutMe}
            }, removePrevious);
        }

        private static MessageButtonAction LetsHaveAPauseMessage(bool removePrevious = true)
        {
            return new MessageButtonAction(SalesBotChat.Pause, new List<List<string>>()
            {
                new List<string>()
                    {SalesBotChat.ToSection3}
            }, removePrevious);
        }

        private static MessageButtonAction WhatIsALeadMagnetMessage(bool removePrevious = true)
        {
            return new MessageButtonAction(SalesBotChat.WhatIsALeadMagnet, new List<List<string>>()
            {
                new List<string>()
                    {SalesBotChat.Continue}
            }, removePrevious);
        }

        private static MessageButtonAction GetExplanationLeadGenerationMessage(bool removePrevious = true)
        {
            return new MessageButtonAction(SalesBotChat.ExplanationLeadGeneration, new List<List<string>>()
            {
                new List<string>()
                    {SalesBotChat.LeadMagnet, SalesBotChat.Continue}
            }, removePrevious);
        }

        private static MessageButtonAction GetResponseLeadGenerationMessage(bool removePrevious = true)
        {
            return new MessageButtonAction(SalesBotChat.ResponseLeadGeneration, new List<List<string>>()
            {
                new List<string>()
                    {SalesBotChat.Continue}
            }, removePrevious, 250);
        }

        private static void AddQuestion2(Funnel result)
        {
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new HasClientCreatedTagTrigger(),
                    new StateIsValueTrigger(StateStep,"1")
                }
            }, new List<FunnelAction>()
            {
                GetQ2Buttons()
            });

            foreach (var Q2Response in new List<string>()
            {
                SalesBotChat.Q2_lessThan20,
                SalesBotChat.Q2_between20_50,
                SalesBotChat.Q2_between50_100,
                SalesBotChat.Q2_between100_200,
                SalesBotChat.Q2_between200_500,
                SalesBotChat.Q2_moreThan500
            })
            {
                result.CallbackActions.Add(new AndTrigger()
                {
                    Priority = 100,
                    Triggers = new List<ITrigger>()
                    {
                        new MessageTrigger(Q2Response),
                        new StateIsValueTrigger(StateStep, "1")
                    }
                }, new List<FunnelAction>()
                {
                    new StateUpdateAction(StateStep, "2"),
                    new StateUpdateAction(StateQ2, Q2Response),
                    new MessageAction(SalesBotChat.CleverEnough),
                    GetLeadGenerationMessage(false)
                });
            }
        }

        private static MessageButtonAction GetLeadGenerationMessage(bool removePrevious = true)
        {
            return new MessageButtonAction(SalesBotChat.LeadGeneration,new List<List<string>>()
            {
                new List<string>()
                    {SalesBotChat.TellMe}
            }, removePrevious, 250);
        }

        private static void AddQuestion1(Funnel result)
        {
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new HasNotClientCreatedTagTrigger()
                }
            }, new List<FunnelAction>()
            {
                new ClientCreateAction(),
                GetQ1Buttons()
            });

            foreach (var Q1Response in new List<string>()
            {
                SalesBotChat.Q1_Solo,
                SalesBotChat.Q1_2People,
                SalesBotChat.Q1_3_5People,
                SalesBotChat.Q1_5plus
            })
            {
                result.CallbackActions.Add(new AndTrigger()
                {
                    Priority = 100,
                    Triggers = new List<ITrigger>()
                    {
                        new MessageTrigger(Q1Response),
                        new StateIsValueEmpty()
                        {
                            State = StateStep
                        }
                    }
                }, new List<FunnelAction>()
                {
                    new StateUpdateAction(StateStep, "1"),
                    new StateUpdateAction(StateQ1, Q1Response),
                    GetQ2Buttons()
                });
            }
        }

        private static MessageButtonAction GetQ1Buttons()
        {
            return new MessageButtonAction(SalesBotChat.Question1,
                new List<List<string>>()
                {
                    new List<string>()
                    {
                        SalesBotChat.Q1_Solo,
                        SalesBotChat.Q1_2People,
                    },
                    new List<string>()
                    {
                        SalesBotChat.Q1_3_5People,
                        SalesBotChat.Q1_5plus,
                    }
                });
        }

        private static MessageButtonAction GetQ2Buttons()
        {
            return new MessageButtonAction(SalesBotChat.Question2,
                new List<List<string>>()
                {
                    new List<string>()
                    {
                        SalesBotChat.Q2_lessThan20,
                        SalesBotChat.Q2_between20_50
                    },
                    new List<string>()
                    {
                        SalesBotChat.Q2_between50_100,
                        SalesBotChat.Q2_between100_200,
                    },
                    new List<string>()
                    {
                        SalesBotChat.Q2_between200_500,
                        SalesBotChat.Q2_moreThan500,
                    }
                });
        }
    }
}
