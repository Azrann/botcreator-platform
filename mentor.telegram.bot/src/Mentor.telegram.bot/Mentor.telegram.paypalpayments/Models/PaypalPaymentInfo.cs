using System.Collections.Generic;

namespace Mentor.telegram.paypalpayments.Models
{
    public class PaypalPaymentInfo
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public string SuccessUrl { get; set; }

        public string CancelUrl { get; set; }

        public List<PaypalItem> Items { get; set; }
    }
}
