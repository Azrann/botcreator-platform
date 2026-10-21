using System;
using System.Collections.Generic;

namespace Mentor.telegram.coinpayments
{
    public class CreateOrder
    {
        public string order_id { get; set; }
        public double price_amount { get; set; }
        public string price_currency { get; set; }
        public string receive_currency { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string callback_url { get; set; }
        public string cancel_url { get; set; }
        public string success_url { get; set; }
        public string token { get; set; }
    }

    public class CreateOrderResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public string price_currency { get; set; }
        public double price_amount { get; set; }

        public string receive_currency { get; set; }
        public double? receive_amount { get; set; }

        public string created_at { get; set; }
        public string order_id { get; set; }
        public string payment_url { get; set; }
        public string token { get; set; }

        public List<string> errors { get; set; }
        public string message { get; set; }
        public string reason { get; set; }
    }

    public class CoinGateOrder
    {
        public string id { get; set; }
        public string status { get; set; }
        public string price_currency { get; set; }
        public double price_amount { get; set; }

        public string pay_currency { get; set; }
        public double? pay_amount { get; set; }

        public string receive_currency { get; set; }
        public double? receive_amount { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime? expire_at { get; set; }

        public string TransactionId { get; set; }
        public string payment_url { get; set; }
        public string payment_address { get; set; }
        public string token { get; set; }

        public List<string> errors { get; set; }
        public string message { get; set; }
        public string reason { get; set; }
    }
}
