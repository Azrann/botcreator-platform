using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Mentor.telegram.coinpayments.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Mentor.telegram.bot.api.Controllers
{
    // USED by coinpayments.com
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IUpdateService _updateService;
        private readonly ICoinApiStorageService _coinApiStorageService;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(PaymentController));

        public PaymentController(IUpdateService updateService, ICoinApiStorageService coinApiStorageService)
        {
            _updateService = updateService;
            _coinApiStorageService = coinApiStorageService;
        }

        [HttpPost("{botId}")]
        public async Task<IActionResult> Post([FromRoute] string botId)
        {
            Guid correlationId = Guid.NewGuid();
            try
            {
                Log.LogInf($"Payment update received for id {botId}", correlationId);
                var hmac = this.Request.Headers["HMAC"];
                if (string.IsNullOrEmpty(hmac))
                {
                    return BadRequest("signature not found");
                }

                var parametersData = GetParametersData();

                var data = _coinApiStorageService.GetCoinApiDataById(botId);
                bool valid = PaymentHelper.Validate(data.IpnSecret, parametersData, hmac);

                if (!valid)
                {
                    var parametersJson = JsonConvert.SerializeObject(parametersData);
                    Log.LogErr($"Payment invalid signature [{hmac}] for parameters {parametersJson}", correlationId);
                    return BadRequest("Invalid signature");
                }

                StringValues merchant;
                StringValues status;

                if (!Request.Form.TryGetValue("status", out status))
                {
                    Log.LogErr($"Payment status not found", correlationId);
                    return BadRequest("status not found");
                }

                if (!Request.Form.TryGetValue("merchant", out merchant))
                {
                    Log.LogErr($"Payment merchant not found", correlationId);
                    return BadRequest("merchant not found");
                }

                var eventInfo = GetEventInfo(parametersData);

                await _updateService.ProcessPaymentAsync(botId,eventInfo, correlationId);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogErr($"Payment update called ERROR {ex.Message}", correlationId);
                return Ok();
            }
        }

        private EventInfo GetEventInfo(Dictionary<string, string> parametersData)
        {
            /* SEE documentation for fields https://www.coinpayments.net/merchant-tools-ipn*/
            var result = new EventInfo()
            {
                PaymentInfo = new PaymentInfo()
                {
                    Id = new Guid(parametersData.GetString("invoice")),
                    PaymentStatus = int.Parse(parametersData["status"]),
                    TransactionId = parametersData["txn_id"],
                    OriginalCurrency = parametersData["currency1"],
                    OriginalAmount = decimal.Parse(parametersData["amount1"]),
                    BuyerCurrency = parametersData["currency2"],
                    BuyerAmount = decimal.Parse(parametersData["amount2"]),
                    ReceivedAmount = parametersData.GetDecimal("received_amount"),
                    ReceivedConfirms = parametersData.GetInt("received_confirms"),
                    CryptoTxId = parametersData.GetString("send_tx"),
                    ItemName = parametersData.GetString("item_name"),
                    Fee = parametersData.GetDecimalFromDouble("fee")
                }
            };
            return result;
        }

        private Dictionary<string, string> GetParametersData()
        {
            Dictionary<string, string> parametersData = new Dictionary<string, string>();
            foreach (var formKey in Request.Form.Keys)
            {
                parametersData.Add(formKey, Request.Form[formKey]);
            }

            return parametersData;
        }
    }
}
