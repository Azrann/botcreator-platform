using Bot.telegram.funnel;
using Bot.telegram.funnel.Actions;
using Bot.telegram.funnel.Triggers;
using System;
using System.Collections.Generic;

namespace Mentor.telegram.bot.api.Services.Chats
{
    public class PaypalSaleBot
    {
        public static string PaypalPurchaseCompletedCommand = "PaypalPurchaseCompleted";
        public static Funnel GetFunnel()
        {
            var result = new Funnel();
            result.MessageActions.Add(new MessageTrigger("BUY_SHOE"),
                new List<FunnelAction>()
                {
                    new PaypalPaymentCreateActionAsync
                    {
                        Amount = 100,
                        CorrelationId = Guid.NewGuid(),
                        Currency = "USD",
                        Description = "Buy a shoe",
                        Title = "Test buy",
                        Items = new List<paypalpayments.Models.PaypalItem>
                        {
                            new paypalpayments.Models.PaypalItem
                            {
                                Name = "Shoe",
                                Price = 100,
                                Quantity = 1,
                                Currency = "USD"
                            }
                        }
                    },
                    new MessageAction(@"Please click on the link <a>[CallbackData]</a> for checkout", false)
                });
            result.EventActions.Add(new PropertiesTrigger(new Dictionary<string, object>
            {
                { "PaymentStatus" ,100 }

            }), new List<FunnelAction>
            {
                new MessageAction("Congrats purchase was completed",false)
            });
            return result;
        }
    }
}
