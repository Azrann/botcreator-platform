using BraintreeHttp;
using Mentor.telegram.paypalpayments.Models;
using PayPal.Core;
using PayPal.v1.Payments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Bot.Configuration;
using Core.Common.Extensions;
using Mentor.telegram.bot.models.Paypal;
using Mentor.telegram.paypalpayments.WebhookPatch;

namespace Mentor.telegram.paypalpayments
{
    public class PaypalApi
    {
        private const string Redirect = "redirect";
        private const string ApprovalUrl = "approval_url";
        private const string VerificationSuccess = "SUCCESS";

        private readonly string _botId;
        private readonly Guid _correlationId;
        private readonly PayPalEnvironment _paypalEnvironment;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(PaypalApi));

        public PaypalApi(PayPalEnvironment paypalEnvironment, string botId, Guid correlationId)
        {
            _botId = botId;
            _correlationId = correlationId;
            _paypalEnvironment = paypalEnvironment;
        }

        public async Task<bool> ExecutePayment(string paypalPayerId, string paypalPaymentId)
        {
            var paymentExecution = new PaymentExecution
            {
                PayerId = paypalPayerId
            };
            var executeRequest = new PaymentExecuteRequest(paypalPaymentId);
            executeRequest.RequestBody(paymentExecution);
            var client = new PayPalHttpClient(_paypalEnvironment);

            try
            {
                var response = await client.Execute(executeRequest);
                var statusCode = response.StatusCode;

                var result = response.Result<Payment>();
                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                Log.LogErr($"Paypal execution failed with status code {statusCode}, failure reson: {result?.FailureReason}. For bot {_botId}", _correlationId);
            }
            catch (HttpException httpException)
            {
                Log.LogErr($"Paypal execute request throws exception: {JsonConvertExt.SerializeObject(httpException)}", _correlationId);
            }

            return false;
        }

        public async Task<PaypalPaymentRequestResult> CreatePaymentAsync(PaypalPaymentInfo paymentInfo)
        {
            var payment = BuildPayment(paymentInfo);

            var request = new PaymentCreateRequest();
            request.RequestBody(payment);
            var client = new PayPalHttpClient(_paypalEnvironment);

            try
            {
                HttpResponse response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                if (statusCode == HttpStatusCode.Created)
                    return ConstructPaymentResult(result);
                else
                {
                    Log.LogErr($"Could not create Paypal Payment for bot {_botId}", _correlationId);
                    Log.LogErr($"Paypal create payment request returned: status code {statusCode}, failure reson: {result?.FailureReason}. For bot {_botId}", _correlationId);
                }
            }
            catch (HttpException httpException)
            {
                Log.LogErr($"Paypal create payment request throws exception: {JsonConvertExt.SerializeObject(httpException)}", _correlationId);
            }

            return null;
        }

        public static PayPalEnvironment CreatePaypalEnvironment(IConfigurationCommon config, PaypalApiData credentials)
        {
            if (config.IsDevelopment())
            {
                return new SandboxEnvironment(credentials.ClientId, credentials.SecretId);
            }

            return new LiveEnvironment(credentials.ClientId, credentials.SecretId);
        }

        private PaypalPaymentRequestResult ConstructPaymentResult(Payment payment)
        {
            var result = new PaypalPaymentRequestResult
            {
                Id = payment.Id
            };
            result.CheckoutLink = FindCheckoutLink(payment.Links);

            return result;
        }

        private string FindCheckoutLink(List<LinkDescriptionObject> links)
        {
            if (links == null || links.Count == 0)
                return string.Empty;

            return links.FirstOrDefault(s => s.Method.ToLowerInvariant() == Redirect &&
                            s.Rel.ToLowerInvariant() == ApprovalUrl)?.Href;
        }

        private Payment BuildPayment(PaypalPaymentInfo paymentInfo)
        {
            var builder = new PaypalPaymentBuilder(paymentInfo);
            builder.BuildPaymentAsSale();
            builder.BuildTransaction();
            builder.BuildCallbackUrls();

            return builder.GetPayment();
        }

        public async Task<bool> ValidateWebhookAsync(VerifyWebhookSignatureRaw rawSignature, string botId, Guid correlationId)
        {
            var executeRequest = new WebhookVerifySignatureRawRequest();
            executeRequest.RequestBody(rawSignature);
            var client = new PaypalRawHttpClient(_paypalEnvironment);

            try
            {
                var response = await client.Execute(executeRequest);
                var statusCode = response.StatusCode;

                var result = response.Result<PayPal.v1.Webhooks.VerifyWebhookSignatureResponse>();
                if (result.VerificationStatus == VerificationSuccess)
                {
                    return true;
                }
                Log.LogErr($"Paypal webhook verify request failed with verification status: {result?.VerificationStatus}. For bot {_botId}", _correlationId);
            }
            catch (HttpException httpException)
            {
                Log.LogErr($"Paypal webhook verify request throws exception: {JsonConvertExt.SerializeObject(httpException)}", _correlationId);
            }

            return false;
        }
    }
}
