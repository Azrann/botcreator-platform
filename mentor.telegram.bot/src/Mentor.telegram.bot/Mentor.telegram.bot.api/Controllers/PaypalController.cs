using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Mentor.telegram.bot.models;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Newtonsoft.Json;
using Mentor.telegram.paypalpayments.Models;
using Mentor.telegram.bot.api.Services.Chats;
using System.Net;
using Microsoft.AspNetCore.Http;
using PayPal.v1.Webhooks;
using Microsoft.Extensions.Primitives;
using PayPal.v1.Payments;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Mentor.telegram.bot.api.Controllers
{
    [Route("api/[controller]")]
    public class PaypalController : Controller
    {
        private const int PaymentCompletedStatus = 100;
        private readonly IUpdateService _updateService;
        private readonly IPaymentInfoService _paymentInfoService;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(PaypalController));

        public PaypalController(IUpdateService updateService, IPaymentInfoService paymentInfoService)
        {
            _updateService = updateService;
            _paymentInfoService = paymentInfoService;
        }

        [HttpGet("{paymentInfoId}/success")]
        public async Task<IActionResult> SuccessCallback([FromRoute]string paymentInfoId, [FromQuery]string paymentId, [FromQuery]string payerId)
        {
            Guid correlationId = Guid.NewGuid();
            try
            {
                await _updateService.ProcessPaypalExecution(paymentInfoId, payerId, correlationId);
            }
            catch (Exception ex)
            {
                Log.LogErr($"Payment update called ERROR {ex.Message}", correlationId);
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Content = "<html><body> <h1>Something went wrong, view more in the logs </h1></body></html>"
                };
            }
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html><body> <h1>Payment Finished, You can close the window. </h1></body></html>"
            };
        }

        [HttpPost("{botId}/paymentinfo")]
        public async Task<IActionResult> EventHandler([FromRoute]string botId)
        {
            Guid correlationId = Guid.NewGuid();
            try
            {
                string strBody = string.Empty;
                using (StreamReader bodyReader = new StreamReader((Stream)Request.Body, Encoding.UTF8))
                {
                    strBody = await bodyReader.ReadToEndAsync();
                }
                var paypalEvent = JsonConvert.DeserializeObject<Event<Sale>>(strBody);

                var validateResult = await ValidateWebhookAsync(botId, correlationId, strBody);

                if (!validateResult)
                {
                    Log.LogErr($"Webhook validation failed for bot: {botId}", correlationId);
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }


                var eventInfo = GetEventInfo(paypalEvent);
                await _updateService.ProcessPaymentAsync(botId, eventInfo, correlationId);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogErr($"Payment update called ERROR {ex.Message}", correlationId);
                return Ok();
            }
        }

        private async Task<bool> ValidateWebhookAsync(string botId, Guid correlationId, string rawBody)
        {

            Request.Headers.TryGetValue("Paypal-Auth-Algo", out StringValues paypalAuthAlgo);
            Request.Headers.TryGetValue("Paypal-Cert-Url", out StringValues certUrl);
            Request.Headers.TryGetValue("Paypal-Transmission-Id", out StringValues paypalTransmissionId);
            Request.Headers.TryGetValue("Paypal-Transmission-Sig", out StringValues transmissionSig);
            Request.Headers.TryGetValue("Paypal-Transmission-Time", out StringValues paypalTransmissionTime);

            var signatureVerification = new VerifyWebhookSignatureRaw
            {
                AuthAlgo = paypalAuthAlgo,
                CertUrl = certUrl,
                TransmissionId = paypalTransmissionId,
                TransmissionSig = transmissionSig,
                TransmissionTime = paypalTransmissionTime,
                WebhookEvent = new JRaw(rawBody)
            };

            return await _updateService.ValidatePaypalWebhook(botId, signatureVerification, correlationId);
        }

        private EventInfo GetEventInfo(Event<Sale> paypalEvent)
        {
            var result = new EventInfo()
            {
                Command = PaypalSaleBot.PaypalPurchaseCompletedCommand,
                PaymentInfo = new PaymentInfo()
                {
                    PaymentStatus = PaymentCompletedStatus,
                    TransactionId = paypalEvent.Resource.ParentPayment,
                    ReceivedAmount = decimal.Parse(paypalEvent.Resource.Amount.Total),
                    Fee = decimal.Parse(paypalEvent.Resource.TransactionFee.Value)
                }
            };

            var paymentInfo = _paymentInfoService.GetPaymentInfoByTransactionId(paypalEvent.Resource.ParentPayment);
            result.PaymentInfo.Id = paymentInfo.Id;

            return result;
        }
    }

}
