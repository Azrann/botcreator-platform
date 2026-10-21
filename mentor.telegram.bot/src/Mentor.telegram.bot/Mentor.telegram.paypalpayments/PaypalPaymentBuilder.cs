using Mentor.telegram.paypalpayments.Models;
using PayPal.v1.Payments;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Mentor.telegram.paypalpayments
{
    public class PaypalPaymentBuilder
    {
        private const string PaypalSalePayment = "sale";
        private const string PaypalPaymentMethod = "paypal";

        private Payment _payment = new Payment();
        private PaypalPaymentInfo _paymentInfo;

        public PaypalPaymentBuilder(PaypalPaymentInfo paymentInfo)
        {
            _paymentInfo = paymentInfo;
        }

        public void BuildPaymentAsSale()
        {
            _payment.Intent = PaypalSalePayment;
            _payment.Payer = new Payer
            {
                PaymentMethod = PaypalPaymentMethod
            };
        }

        public void BuildTransaction()
        {
            _payment.Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = _paymentInfo.Amount.ToString(CultureInfo.InvariantCulture),
                            Currency = _paymentInfo.Currency,
                        },
                         Description = _paymentInfo.Description,

                         ItemList = new ItemList {
                             Items = _paymentInfo.Items?.Select(s => new Item {
                                 Name = s.Name,
                                 Price = s.Price.ToString(CultureInfo.InvariantCulture),
                                 Currency = s.Currency,
                                 Description = s.Description,
                                 Quantity = s.Quantity.ToString(CultureInfo.InvariantCulture)
                             } ).ToList() ?? new List<Item>()
                         }
                    }
                };
        }

        public void BuildCallbackUrls()
        {
            _payment.RedirectUrls = new RedirectUrls
            {
                ReturnUrl = _paymentInfo.SuccessUrl,
                CancelUrl = _paymentInfo.CancelUrl
            };
        }

        public Payment GetPayment()
        {
            return _payment;
        }
    }
}
