using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Mentor.telegram.bot.api.Services;
using Mentor.telegram.bot.boundedContext.Services;
using Mentor.telegram.bot.models;
using Mentor.telegram.coinpayments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mentor.telegram.bot.api.Controllers
{
    // USED by cron services
    [Route("api/[controller]")]
    public class CoinGateController : Controller
    {
        private readonly IUpdateService _updateService;
        private readonly ICoinApiStorageService _coinApiStorageService;
        private readonly IEventService _eventService;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CronController));

        public CoinGateController(IUpdateService updateService, ICoinApiStorageService coinApiStorageService,
            IEventService eventService)
        {
            _updateService = updateService;
            _coinApiStorageService = coinApiStorageService;
            _eventService = eventService;
        }

        [HttpPost("{botId}")]
        public async Task<IActionResult> Post([FromRoute] string botId)
        {
            Guid correlationId = Guid.NewGuid();
            try
            {
                Log.LogInf($"CoinGate update received for id {botId}", correlationId);

                var myData = GetParametersData();

                var eventInfo = GetEventInfo(myData);

                var data = _coinApiStorageService.GetCoinApiDataById(botId);

                if (myData.GetString("token") != data.IpnSecret)
                {
                    Log.LogErr($"Coingate security IPN does not match", correlationId);
                    return Unauthorized();
                }

                var api = new CoinGateApi(data.PrivateKey);

                // Que diferencia hay entre TransactionId y Id? en coingate Id es el id que pasa Coingate
                // y order_id el el invoiceId que pasamos
                var order = await api.GetOrderById(eventInfo.PaymentInfo.TransactionId, correlationId);

                // update eventInfo con los valores de order.
                eventInfo.PaymentInfo.PaymentStatus = GetStatus(order.status);

                _eventService.AddEvent(GetEventItem(botId, eventInfo));
                await _updateService.ProcessCoinGateAsync(botId, eventInfo, correlationId);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogErr($"Payment update called ERROR {JsonConvertExt.SerializeObject(ex)}", correlationId);
                return Ok();
            }
        }

        private EventItem GetEventItem(string botId, EventInfo eventInfo)
        {
            return new EventItem()
            {
                BotId = botId,
                ChannelId = eventInfo.SubscriptionInfo?.ChatId.ToString(),
                Username = eventInfo.Username,
                Name = eventInfo.Command,
                DataJson = JsonConvert.SerializeObject(eventInfo),
                Id = Guid.NewGuid()
            };
        }

        private EventInfo GetEventInfo(Dictionary<string, string> parametersData)
        {
            var result = new EventInfo()
            {
                PaymentInfo = new PaymentInfo()
                {
                    Id = new Guid(parametersData.GetString("order_id")),
                    PaymentStatus = GetStatus(parametersData["status"]),
                    TransactionId = parametersData.GetString("id"),
                    OriginalCurrency = parametersData.GetString("price_currency"),
                    OriginalAmount = parametersData.GetDecimalFromDouble("price_amount"),
                    BuyerCurrency = parametersData.GetString("pay_currency"),
                    BuyerAmount = parametersData.GetDecimalFromDouble("pay_amount"),
                    ReceivedAmount = parametersData.GetDecimalFromDouble("pay_amount"), // Coingate received amount is the amount received by the merchant.
                    ReceivedConfirms = 0,
                    CryptoTxId = string.Empty,
                    Fee = 0,
                    CreatedOn = parametersData.GetDateTime("created_at") ?? DateTime.Now
                },
                Command = "CoingatePaymentUpdate"
            };
            return result;
        }

        private int GetStatus(string status)
        {
            switch (status)
            {
                case "new":
                case "pending":
                    return 0;
                case "confirming":
                    return 1;
                case "paid": return 100;
                case "invalid":
                    return -2;
                case "expired":
                    return -1;
                case "refunded":
                    return -3;
                case "canceled":
                    return -4;
                default: throw new Exception($"Status not found {status}");
            }
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
