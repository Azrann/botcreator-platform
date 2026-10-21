using System.Collections.Generic;
using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;

namespace Mentor.telegram.bot.api.Services.Chats
{
    public class TeleBinanceBotChat
    {
        public static string MainMenuText = "🔸 Please click in one of the options bellow. \n";

        public static string AddApiKeysText =
            "REPLACE_WITH_YOUR_VALUE" +
            "https://goo.gl/KtpHth \n\n"+
            "If you already have your Public key 👌 please paste it bellow.\n";
        public static string AddApiPrivateKeyText =
            "REPLACE_WITH_YOUR_VALUE";

        public static string ApiKeySetSuccessfull = "Excellent APIs keys has been set.";
        public static string SetBinanceApiKeys = "Set Binance API Keys";
        public static string Pricing = "Pricing";
        public static string About = "About";
        public static string CreateSubscription = "Create Subscription";
        public static string Return = "↩ RETURN";
        public static string MainMenu = "Main Menu 🔙";
        public static string Back = "Back ↩";
        public static string StateAddingApis = "AddingExchange";
        public static string ExchangeName = "ExchangeName";
        public static string PublicKey = "PublicKey";
        public static string PrivateKey = "REPLACE_WITH_YOUR_VALUE";
        public static string GetOpenOrders = "Open Orders";
        public static string CancelAnOrder = "Cancel an Order";
        public static string CreateOrder = "Create Order";
        public static string SendOrder = "Send Order";
        public static string MenuState = "MenuState";
        public static string BuySellText = "🔸 Which type of order would you like to create?";
        public static string Buy = "Buy";
        public static string Sell = "Sell";
        public static string OrderSide = "OrderSide";
        public static string Pair = "Pair";
        public static string SelectPairText = "🔸 Write the pair you would like to trade in this format coin/pair\n" +
            "example XRP/BTC or if it is one of the pairs bellow just press the button";
        public static string SelectAmountText = "Write the amount you would like to trade\n" +
                                              "example 0.001 or if it is one of the buttons bellow just press the button";
        public const string OrderTypeText = "🔸 Select order type:";
        public const string SelectStopPriceText = "🔸 Type the Stop Price\n example 0.982";
        public const string SelectLimitPriceText = "🔸 Type the Limit Price\n example 0.482";
        public const string Limit = "Limit";
        public const string Market = "Market";
        public const string StopLimit = "StopLimit";
        public const string LimitPrice = "LimitPrice";
        public const string StopLimitPrice = "StopLimitPrice";
        public const string OrderType = "OrderType";
        public const string OrderAmount = "OrderAmount";
        public const string OrderPrice = "OrderPrice";
        public const string CreateOrderSequence = "CreateOrderSequence";
        public static string DisplayMarketOrder = "👉 We are about to create your order please confirm the details: \n\n" +
                                                 $"<b>[OrderSide]</b> [Pair] - [OrderType] Order  \n" +
                                                 $"[OrderSide] [OrderAmount] [PairLeft] At <b>Market Price</b>\n" +
                                                 $"CurrentPrice:[CurrentPrice]\n" +
                                                 $"[BiTotal] [PairRight] \n\n";
        public static string DisplayStopOrder = "👉 We are about to create your order please confirm the details: \n\n" +
                                                $"<b>[OrderSide]</b> [Pair] - [OrderType] Order  \n" +
                                                $"[OrderSide] [OrderAmount] [PairLeft] At <b>[LimitPrice]</b>\n" +
                                                $"CurrentPrice:[CurrentPrice]\n" +
                                                $"[BiTotal] [PairRight] \n\n";
        public static string DisplayLimitOrder = "👉 We are about to create your order please confirm the details: \n\n" +
                                                 $"<b>[OrderSide]</b> [Pair] - [OrderType] Order  \n" +
                                                 $"[OrderSide] [OrderAmount] [PairLeft] At <b>[LimitPrice]</b>\n" +
                                                 $"CurrentPrice:[CurrentPrice]\n" +
                                                 $"[BiTotal] [PairRight] \n\n";
    }

    public static class TeleBinanceBot
    {
        private static readonly List<string> AdminList = new List<string>(){"REPLACE_WITH_YOUR_VALUE"};

        public static Funnel GetFunnel()
        {
            var result = new Funnel();
            var CommandsList = new List<string>()
            {
                TeleBinanceBotChat.SetBinanceApiKeys, TeleBinanceBotChat.Pricing, TeleBinanceBotChat.About,
                TeleBinanceBotChat.CreateSubscription, TeleBinanceBotChat.Return
            };

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
                new StateUpdateAction(TeleBinanceBotChat.PublicKey,""),
                new StateUpdateAction(TeleBinanceBotChat.PrivateKey, ""),
                new StateUpdateAction(TeleBinanceBotChat.StateAddingApis,""),
                new StateUpdateAction(TeleBinanceBotChat.CreateOrderSequence,""),
                GetMainMenuButtons()
            });

            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new HasClientCreatedTagTrigger(),
                    new MessageTrigger("/start")
                }
            }, new List<FunnelAction>()
            {
                GetMainMenuButtons()
            });
            
            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(TeleBinanceBotChat.MainMenu)
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction(TeleBinanceBotChat.CreateOrderSequence,""),
                GetMainMenuButtons()
            });

            var createOrderSteps = new SequenceState()
            {
                Id = TeleBinanceBotChat.CreateOrderSequence,
                FirstStepCallback = TeleBinanceBotChat.CreateOrder,
                MainMenuActions = new List<FunnelAction>()
                {
                },
                Steps = new List<IStepStates>()
                {
                    new StepWithState()
                    {
                        Actions = new List<FunnelAction>()
                        {
                            GetBuySellMenuButtons()
                        }
                    },
                    new StepWithState() //IF we are in STATE 1 and we receive an event
                    {                   // Then save the value to OrderSide and show Pair menu.    
                        Actions = new List<FunnelAction>()
                        {
                            new StateUpdateActionWithData(TeleBinanceBotChat.OrderSide), 
                            GetSelectPairMenuButtons()                                
                        },
                        PreviousStepActions = new List<FunnelAction>()
                        {
                        }
                    },
                    new StepWithState() //IF we are in STATE 2 and we receive an event
                    {                   // Then save the value to Pair and show OrderType menu.    
                        Actions = new List<FunnelAction>()
                        {
                            new StateUpdateActionWithData(TeleBinanceBotChat.Pair),
                            GetOrderTypeButtons()
                        },
                        PreviousStepActions = new List<FunnelAction>()
                        {
                            GetBuySellMenuButtons()
                        }
                    },
                    new StepWithState() //IF we are in STATE 3 and we receive an event
                    {           // Then save the value to OrderType and show Amount menu.                      
                        Actions = new List<FunnelAction>()
                        {
                            new StateUpdateActionWithData(TeleBinanceBotChat.OrderType),
                            GetSelectAmountMenuButtons()
                        },
                        PreviousStepActions = new List<FunnelAction>()
                        {
                            GetSelectPairMenuButtons()
                        }
                    },
                    new StepConditionalWithState()  //IF we are in STATE 4 and we receive an event
                    {                               // And Condition apply ...
                        StateConditions = new List<StepConditionalData>()
                        { 
                            new StepConditionalData()
                            {
                                NextStep = 5,
                                PreviousStep = 3,
                                Actions = new List<FunnelAction>()
                                {
                                    new StateUpdateActionWithData(TeleBinanceBotChat.OrderAmount),
                                    GetSelectStopPriceMenuButtons()
                                },
                                PreviousStepActions =  new List<FunnelAction>()
                                {
                                    GetOrderTypeButtons()
                                },
                                StateName = TeleBinanceBotChat.OrderType,
                                StateValue = TeleBinanceBotChat.StopLimit,
                            },
                            new StepConditionalData()
                            {
                                NextStep = 6,
                                PreviousStep = 3,
                                Actions = new List<FunnelAction>()
                                {
                                    new StateUpdateActionWithData(TeleBinanceBotChat.OrderAmount),
                                    GetSelectLimitPriceMenuButtons()
                                },
                                PreviousStepActions =  new List<FunnelAction>()
                                {
                                    GetOrderTypeButtons()
                                },
                                StateName = TeleBinanceBotChat.OrderType,
                                StateValue = TeleBinanceBotChat.Limit,
                            },new StepConditionalData()
                            {
                                NextStep = 7,
                                PreviousStep = 3,
                                Actions = new List<FunnelAction>()
                                {
                                    new StateUpdateActionWithData(TeleBinanceBotChat.OrderAmount),
                                    new BiEnrichParametersAsync(),
                                    GetConfirmOrderMenuButton(TeleBinanceBotChat.Market)
                                },
                                PreviousStepActions =  new List<FunnelAction>()
                                {
                                    GetOrderTypeButtons()
                                },
                                StateName = TeleBinanceBotChat.OrderType,
                                StateValue = TeleBinanceBotChat.Market,
                            }
                        }
                    },
                    new StepConditionalWithState() //IF we are in STATE 5 and we receive an event
                    {                               // And Condition apply ...
                                                StateConditions = new List<StepConditionalData>()
                        {
                            new StepConditionalData()
                            {
                                NextStep = 6,
                                PreviousStep = 4,
                                Actions = new List<FunnelAction>()
                                {
                                    new StateUpdateActionWithData(TeleBinanceBotChat.StopLimitPrice),
                                    GetSelectLimitPriceMenuButtons()
                                },
                                PreviousStepActions =  new List<FunnelAction>()
                                {
                                    GetSelectAmountMenuButtons()
                                },
                                StateName = TeleBinanceBotChat.OrderType,
                                StateValue = TeleBinanceBotChat.StopLimit,
                            }
                        }
                    },
                    new StepConditionalWithState() //IF we are in STATE 6 and we receive an event
                    {                               // And Condition apply ...
                        StateConditions = new List<StepConditionalData>()
                        {
                            new StepConditionalData()
                            {
                                NextStep = 7,
                                PreviousStep = 5,
                                Actions = new List<FunnelAction>()
                                {
                                    new StateUpdateActionWithData(TeleBinanceBotChat.LimitPrice),
                                    new BiEnrichParametersAsync(),
                                    GetConfirmOrderMenuButton(TeleBinanceBotChat.StopLimit)
                                },
                                PreviousStepActions =  new List<FunnelAction>()
                                {
                                    GetSelectStopPriceMenuButtons()
                                },
                                StateName = TeleBinanceBotChat.OrderType,
                                StateValue = TeleBinanceBotChat.StopLimit,
                            },
                            new StepConditionalData()
                            {
                                NextStep = 7,
                                PreviousStep = 4,
                                Actions = new List<FunnelAction>()
                                {
                                    new StateUpdateActionWithData(TeleBinanceBotChat.LimitPrice),
                                    new BiEnrichParametersAsync(),
                                    GetConfirmOrderMenuButton(TeleBinanceBotChat.Limit)
                                },
                                PreviousStepActions =  new List<FunnelAction>()
                                {
                                    GetSelectAmountMenuButtons()
                                },
                                StateName = TeleBinanceBotChat.OrderType,
                                StateValue = TeleBinanceBotChat.Limit,
                            }
                        }
                    },
                    new StepConditionalWithState(true) //IF we are in STATE 7 and we receive an event
                    {                               // And Condition apply ...
                        StateConditions = new List<StepConditionalData>()
                        {
                            new StepConditionalData()
                            {
                                NextStep = 8,
                                PreviousStep = 6,
                                Actions = new List<FunnelAction>()
                                {
                                    new BiCreateOrderAsync(),
                                    new BiMessageResultAction(),
                                    GetMainMenuButtons(false)
                                    // CREATE ORDER AND RESET SEQUENCE GO TO MAIN MENU.
                                   
                                },
                                PreviousStepActions =  new List<FunnelAction>()
                                {
                                    GetSelectLimitPriceMenuButtons()
                                },
                                StateName = TeleBinanceBotChat.OrderType,
                                StateValue = TeleBinanceBotChat.StopLimit,
                            },
                            new StepConditionalData()
                            {
                                NextStep = 8,
                                PreviousStep = 6,
                                Actions = new List<FunnelAction>()
                                {
                                    // CREATE ORDER AND RESET SEQUENCE GO TO MAIN MENU.
                                    new BiCreateOrderAsync(),
                                    new BiMessageResultAction(),
                                    GetMainMenuButtons(false)
                                },
                                PreviousStepActions =  new List<FunnelAction>()
                                {
                                    GetSelectLimitPriceMenuButtons()
                                },
                                StateName = TeleBinanceBotChat.OrderType,
                                StateValue = TeleBinanceBotChat.Limit,
                            },
                            new StepConditionalData()
                            {
                                NextStep = 8,
                                PreviousStep = 4,
                                Actions = new List<FunnelAction>()
                                {
                                    // CREATE ORDER AND RESET SEQUENCE GO TO MAIN MENU.
                                    new BiCreateOrderAsync(),
                                    new BiMessageResultAction(),
                                    GetMainMenuButtons(false)
                                },
                                PreviousStepActions =  new List<FunnelAction>()
                                {
                                    GetSelectAmountMenuButtons()
                                },
                                StateName = TeleBinanceBotChat.OrderType,
                                StateValue = TeleBinanceBotChat.Market,
                            }
                        }
                    },
                }
            };

            result.AddSequenceState(createOrderSteps);
            /* TODO: Select trading pair require custom StateUpdate+checkifpair is valid.*/
           
            ShowOpenOrders(result);

            AddCancelOrder(result);

            ReturnToMenu(result);

            AddApiKeys(result, CommandsList);

            return result;
        }

        private static void ShowOpenOrders(Funnel result)
        {
            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(TeleBinanceBotChat.GetOpenOrders)
                }
            }, new List<FunnelAction>()
            {
                new BiGetOpenOrdersAsync(),
                new BiShowOpenOrdersAsync(),
                GetMainMenuButtons(false)
            });
        }

        private static void AddCancelOrder(Funnel result)
        {
            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(TeleBinanceBotChat.CancelAnOrder)
                }
            }, new List<FunnelAction>()
            {
                new BiGetOpenOrdersAsync(),
                new BiShowButtonsCancelOpenOrdersAsync(),
            });

            result.CallbackActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageStartWithTrigger("🏷")
                }
            }, new List<FunnelAction>()
            {
                new BiCancelOpenOrderAsync(),
                new BiMessageResultAction(),
                new BiGetOpenOrdersAsync(),
                new BiShowButtonsCancelOpenOrdersAsync(),
            });
        }

        private static void AddApiKeys(Funnel result, List<string> CommandsList)
        {
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(TeleBinanceBotChat.SetBinanceApiKeys)
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction(TeleBinanceBotChat.StateAddingApis, "true"),
                new StateUpdateAction(TeleBinanceBotChat.ExchangeName, "Binance"),
                new MessageMenuButtonAction(TeleBinanceBotChat.AddApiKeysText, new List<List<string>>()
                {
                    new List<string>()
                        {IvoBotChat.Return}
                }, true),
            });

            // Adding the Public Key
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageIsNotTrigger(CommandsList),
                    new StateIsNotValueEmpty(TeleBinanceBotChat.StateAddingApis),
                    new StateIsValueEmpty()
                    {
                        State = TeleBinanceBotChat.PrivateKey
                    },
                    new StateIsValueEmpty()
                    {
                        State = TeleBinanceBotChat.PublicKey
                    }
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateActionWithData(TeleBinanceBotChat.PublicKey),
                new MessageMenuButtonAction(TeleBinanceBotChat.AddApiPrivateKeyText, new List<List<string>>()
                {
                    new List<string>()
                        {TeleBinanceBotChat.Return}
                }, true),
            });

            // Adding the PrivateKey
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageIsNotTrigger(CommandsList),
                    new StateIsNotValueEmpty(TeleBinanceBotChat.StateAddingApis),
                    new StateIsValueEmpty()
                    {
                        State = TeleBinanceBotChat.PrivateKey
                    },
                    new StateIsNotValueEmpty()
                    {
                        State = TeleBinanceBotChat.PublicKey
                    }
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateActionWithData(TeleBinanceBotChat.PrivateKey),
                new ExchangeUpdateAction(),
                new MessageMenuButtonAction(TeleBinanceBotChat.ApiKeySetSuccessfull, new List<List<string>>()
                {
                    new List<string>()
                        {TeleBinanceBotChat.Return}
                }, true),
            });
        }


        private static MessageButtonAction GetMainMenuButtons(bool removePrevious = true)
        {
            return new MessageButtonAction(TeleBinanceBotChat.MainMenuText, new List<List<string>>()
            { new List<string>()
                    {TeleBinanceBotChat.Pricing,TeleBinanceBotChat.CreateSubscription},
                new List<string>()
                    {TeleBinanceBotChat.SetBinanceApiKeys, TeleBinanceBotChat.About},
                new List<string>()
                    {TeleBinanceBotChat.GetOpenOrders, TeleBinanceBotChat.CancelAnOrder},
                new List<string>()
                    {TeleBinanceBotChat.CreateOrder},

            }, removePrevious);
        }

        private static MessageButtonAction GetBuySellMenuButtons(bool removePrevious = true)
        {
            return new MessageButtonAction(TeleBinanceBotChat.BuySellText, new List<List<string>>()
            { new List<string>()
                    {TeleBinanceBotChat.Buy,TeleBinanceBotChat.Sell},
                new List<string>()
                    {TeleBinanceBotChat.MainMenu},
            }, removePrevious);
        }

        private static MessageButtonAction GetSelectPairMenuButtons(bool removePrevious = true)
        {
            return new MessageButtonAction(TeleBinanceBotChat.SelectPairText, new List<List<string>>()
            { new List<string>()
                    {"BTC/USDT","ETH/BTC","ETH/USDT"},
                new List<string>()
                    {"BCH/USDT","BCH/BTC","XRP/USDT"},
                new List<string>()
                    {"ADA/BTC","XRP/BTC","BAT/BTC"},
                new List<string>()
                    {TeleBinanceBotChat.Back, TeleBinanceBotChat.MainMenu},
            }, removePrevious);
        }

        private static MessageButtonAction GetSelectAmountMenuButtons(bool removePrevious = true)
        {
            return new MessageButtonAction(TeleBinanceBotChat.SelectAmountText, new List<List<string>>()
            { new List<string>()
                    {"0.001","0.01","0.1"},
                new List<string>()
                    {"0.002","0.02","0.2"},
                new List<string>()
                    {"0.005","0.05","0.5"},
                new List<string>()
                    {TeleBinanceBotChat.Back, TeleBinanceBotChat.MainMenu},
            }, removePrevious);
        }

        private static MessageButtonAction GetSelectStopPriceMenuButtons(bool removePrevious = true)
        {
            return new MessageButtonAction(TeleBinanceBotChat.SelectStopPriceText, new List<List<string>>()
            { 
                new List<string>()
                    {TeleBinanceBotChat.Back, TeleBinanceBotChat.MainMenu},
            }, removePrevious);
        }

        private static MessageButtonAction GetSelectLimitPriceMenuButtons(bool removePrevious = true)
        {
            return new MessageButtonAction(TeleBinanceBotChat.SelectLimitPriceText, new List<List<string>>()
            {
                new List<string>()
                    {TeleBinanceBotChat.Back, TeleBinanceBotChat.MainMenu},
            }, removePrevious);
        }
        
        private static MessageButtonAction GetOrderTypeButtons(bool removePrevious = true)
        {
            return new MessageButtonAction(TeleBinanceBotChat.OrderTypeText, new List<List<string>>()
            { new List<string>()
                    {TeleBinanceBotChat.Limit,TeleBinanceBotChat.Market},
                new List<string>()
                    {TeleBinanceBotChat.StopLimit},
                new List<string>()
                    {TeleBinanceBotChat.Back, TeleBinanceBotChat.MainMenu},
            }, removePrevious);
        }

        private static MessageButtonAction GetConfirmOrderMenuButton(string orderType, bool removePrevious = true)
        {
            string messageText="";
            switch (orderType)
            {
                case TeleBinanceBotChat.Market: messageText = TeleBinanceBotChat.DisplayMarketOrder;
                    break;
                case TeleBinanceBotChat.StopLimit:
                    messageText = TeleBinanceBotChat.DisplayStopOrder;
                    break;
                case TeleBinanceBotChat.Limit:
                    messageText = TeleBinanceBotChat.DisplayLimitOrder;
                    break;
            }

            return new MessageButtonAction(messageText, new List<List<string>>()
            { 
                new List<string>()
                    {TeleBinanceBotChat.SendOrder},
                new List<string>()
                    {TeleBinanceBotChat.Back, TeleBinanceBotChat.MainMenu},
            }, removePrevious);
        }

        private static void ReturnToMenu(Funnel result)
        {
            result.MessageActions.Add(new AndTrigger()
            {
                Priority = 100,
                Triggers = new List<ITrigger>()
                {
                    new MessageTrigger(IvoBotChat.Return)
                }
            }, new List<FunnelAction>()
            {
                new StateUpdateAction(TeleBinanceBotChat.PublicKey,""),
                new StateUpdateAction(TeleBinanceBotChat.PrivateKey, ""),
                new StateUpdateAction(TeleBinanceBotChat.ExchangeName, ""),
                new StateUpdateAction(TeleBinanceBotChat.StateAddingApis,""),
                GetMainMenuButtons()
            });
        }
    }
}
