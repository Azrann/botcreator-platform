using Mentor.telegram.paypalpayments.Models;
using System.Collections.Generic;

namespace Bot.telegram.funnel.Actions
{
    public class PaypalPaymentCreateActionAsync : FunnelAction
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<PaypalItem> Items { get; set; }
    }
}
