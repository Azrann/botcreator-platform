using System.Collections.Generic;
using System.Linq;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;
using Newtonsoft.Json;
using Xunit;

namespace Bot.telegram.funnel.test
{
    public class FunnelSerializationTest
    {
        private const string StartMessage = "\\start";
        private const string WelcomeMessage = "Welcome to CryptoTraders";
        private const string WriteEmailAddress = "Please write your email:";
        private const string MainMenu = "Welcome to CryptoTraders here is your options;";
        private const string ButtonBuyCourse = "Buy Course";
        private const string ButtonGoBack = "Go Back";
        private const string CourseSelectionMessage = "Which course would you like to buy:";
        private const string ButtonBegginer = "Begginer Trading";
        private const string ButtonIntermidiate = "Intermidiate Trading";
        private const string ButtonAdvance = "Advance Trading";
        private const string ButtonPro = "Proffesional Trading";

        [Fact]
        public void ICanSerializeDeserializeAnEmptyFunnel()
        {
            IFunnel testFunnel = GetEmptyFunnel();
            var jsonFunnel = JsonConvert.SerializeObject(testFunnel);
            var funnel = JsonConvert.DeserializeObject<Funnel>(jsonFunnel);
            Assert.True(funnel.CallbackActions.Count == 0);
            Assert.True(funnel.MessageActions.Count == 0);
        }

        [Fact]
        public void ICanSerializeAnEmptyFunnelWithNullActions()
        {
            IFunnel testFunnel = new Funnel();
            var jsonFunnel = JsonConvert.SerializeObject(testFunnel);
            var funnel = JsonConvert.DeserializeObject<Funnel>(jsonFunnel);
            Assert.True(funnel.CallbackActions.Count == 0);
            Assert.True(funnel.MessageActions.Count == 0);
        }

        [Fact]
        public void ICanSerializeDeserializeWelcomeFunnel()
        {
            IFunnel testFunnel = GetWelcomeFunnel();
            var jsonFunnel = JsonConvert.SerializeObject(testFunnel);
            var funnel = JsonConvert.DeserializeObject<Funnel>(jsonFunnel);
            Assert.True(funnel.CallbackActions.Count == 0);
            Assert.True(funnel.MessageActions.Count == 1);
        }

        [Fact]
        public void ICanSerializeDeserializeSubscriptionFunnel()
        {
            IFunnel testFunnel = GetSubscriptionFunnel();
            var jsonFunnel = JsonConvert.SerializeObject(testFunnel);
            var funnel = JsonConvert.DeserializeObject<Funnel>(jsonFunnel);
            Assert.True(funnel.CallbackActions.Count == 5);
            Assert.True(funnel.MessageActions.Count == 1);
        }

        [Fact]
        public void ICanSerializeDeserializeComplexTriggerFunnel()
        {
            IFunnel testFunnel = GetComplexFunnelTrigger();
            var jsonFunnel = JsonConvert.SerializeObject(testFunnel);
            var funnel = JsonConvert.DeserializeObject<Funnel>(jsonFunnel);
            Assert.True(funnel.CallbackActions.Count == 0);
            Assert.True(funnel.MessageActions.Count == 2);
        }

        [Fact]
        public void ICanSerializeDeserializeReallyComplexTriggerFunnel()
        {
            IFunnel testFunnel = GetReallyComplexFunnelTrigger();
            var jsonFunnel = JsonConvert.SerializeObject(testFunnel);
            var funnel = JsonConvert.DeserializeObject<Funnel>(jsonFunnel);
            Assert.True(funnel.CallbackActions.Count == 0);
            Assert.True(funnel.MessageActions.Count == 1);
        }

        [Fact]
        public void ICanSerializeDeserializePriorityFunnel()
        {
            IFunnel testFunnel = GetPriorityFunnel();
            var jsonFunnel = JsonConvert.SerializeObject(testFunnel);
            var funnel = JsonConvert.DeserializeObject<Funnel>(jsonFunnel);
            Assert.True(funnel.CallbackActions.Count == 0);
            Assert.True(funnel.MessageActions.Count == 4);
            Assert.True(funnel.MessageActions.Keys.OrderByDescending(x => x.Priority).First().Priority == 100);
        }


        [Fact]
        public void ICanSerializeDeserializePriorityFunnelWithEvents()
        {
            IFunnel testFunnel = GetPriorityFunnelWithEvents();
            var jsonFunnel = JsonConvert.SerializeObject(testFunnel);
            var funnel = JsonConvert.DeserializeObject<Funnel>(jsonFunnel);
            Assert.True(funnel.CallbackActions.Count == 0);
            Assert.True(funnel.MessageActions.Count == 4);
            Assert.True(funnel.EventActions.Count == 1);
            Assert.True(funnel.MessageActions.Keys.OrderByDescending(x => x.Priority).First().Priority == 100);
        }

        [Fact]
        public void ICanSerializeDeserializePriorityFunnelWithState()
        {
            IFunnel testFunnel = GetPriorityFunnelWithState();
            var jsonFunnel = JsonConvert.SerializeObject(testFunnel);
            var funnel = JsonConvert.DeserializeObject<Funnel>(jsonFunnel);
            Assert.True(funnel.CallbackActions.Count == 0);
            Assert.True(funnel.MessageActions.Count == 4);
            Assert.True(funnel.EventActions.Count == 1);
            Assert.True(funnel.MessageActions.Keys.OrderByDescending(x => x.Priority).First().Priority == 100);
        }

        private IFunnel GetEmptyFunnel()
        {
            var result = new Funnel();
            return result;
        }

        private IFunnel GetWelcomeFunnel()
        {
            var result = new Funnel();
            result.MessageActions.Add(new MessageTrigger(StartMessage), new List<FunnelAction>()
            {
                new MessageAction(WelcomeMessage)
            });
            return result;
        }

        private IFunnel GetComplexFunnelTrigger()
        {
            ITrigger comlex1 = new AndTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("/start"),
                    new TagsAddedTrigger(new List<string>(){"subscribed"}),
                    new MessageTrigger("/test"),
                }
            };
            ITrigger comlex2 = new OrTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("/start"),
                    new TagsAddedTrigger(new List<string>(){"subscribed"}),
                    new MessageTrigger("/test"),
                }
            };
            var result = new Funnel();
            result.MessageActions.Add(comlex1, new List<FunnelAction>()
            {
                new MessageAction("AndTriggerExecuted")
            });
            result.MessageActions.Add(comlex2, new List<FunnelAction>()
            {
                new MessageAction("OrTriggerExecuted")
            });
            return result;
        }

        private IFunnel GetReallyComplexFunnelTrigger()
        {
            ITrigger complexChildTrigger = new AndTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("/start"),
                    new TagsAddedTrigger(new List<string>(){"subscribed"}),
                    new MessageTrigger("/test"),
                }
            };
            ITrigger complexTrigger1 = new OrTrigger()
            {
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger("/start"),
                    new TagsAddedTrigger(new List<string>(){"subscribed"}),
                    new MessageTrigger("/test"),
                    complexChildTrigger
                }
            };
            var result = new Funnel();
            
            result.MessageActions.Add(complexTrigger1, new List<FunnelAction>()
            {
                new MessageAction("OrTriggerExecuted")
            });
            return result;
        }

        private IFunnel GetSubscriptionFunnel()
        {
            var result = new Funnel();
            List<List<string>> botOptions = new List<List<string>>()
            {
                new List<string>(){ ButtonBuyCourse, ButtonGoBack},
            };
            result.MessageActions.Add(new MessageTrigger(StartMessage), new List<FunnelAction>()
            {
                new MessageButtonAction(WelcomeMessage,botOptions)
            });

            List<List<string>> courseOptions = new List<List<string>>()
            {
                new List<string>(){ButtonBegginer, ButtonIntermidiate},
                new List<string>(){ButtonAdvance, ButtonPro}
            };

            result.CallbackActions.Add(new MessageTrigger(ButtonBuyCourse), new List<FunnelAction>()
            {
                new MessageButtonAction(CourseSelectionMessage, courseOptions)
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonBegginer), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonBegginer}")
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonIntermidiate), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonIntermidiate}")
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonAdvance), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonAdvance}")
            });
            result.CallbackActions.Add(new MessageTrigger(ButtonPro), new List<FunnelAction>()
            {
                new MessageAction($"Welcome to {ButtonPro}")
            }); 
            return result;
        }

        public IFunnel GetPriorityFunnel()
        {
            var result = new Funnel();

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
                Priority = 98,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(StartMessage),
                    new HasEmailTagTrigger(),
                    new HasClientCreatedTagTrigger(),
                }
            }, new List<FunnelAction>()
            {
                new MessageInputAction(MainMenu)
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
                new MessageAction(MainMenu)
            });
            return result;
        }

        public IFunnel GetPriorityFunnelWithEvents()
        {
            var result = new Funnel();

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
                Priority = 98,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(StartMessage),
                    new HasEmailTagTrigger(),
                    new HasClientCreatedTagTrigger(),
                }
            }, new List<FunnelAction>()
            {
                new MessageInputAction(MainMenu)
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
                new MessageAction(MainMenu)
            });

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new ReplyMessageTrigger(WriteEmailAddress),
                }
            }, new List<FunnelAction>()
            {
                new EmailUpdateAction(),
                new MessageAction(MainMenu)
            });
            return result;
        }

        public IFunnel GetPriorityFunnelWithState()
        {
            var result = new Funnel();

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
                    new StateIsNotValueTrigger("test1","test2")
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction("test1","test2")
            });

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 98,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(StartMessage),
                    new StateIsValueTrigger("test1","test2"),
                    new HasClientCreatedTagTrigger(),
                }
            }, new List<FunnelAction>()
            {
                new MessageInputAction(MainMenu)
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
                new MessageAction(MainMenu)
            });

            result.EventActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new ReplyMessageTrigger(WriteEmailAddress),
                }
            }, new List<FunnelAction>()
            {
                new EmailUpdateAction(),
                new MessageAction(MainMenu)
            });
            return result;
        }
    }
}
