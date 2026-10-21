using System;
using System.Threading.Tasks;
using Mentor.telegram.bot.models;
using Mentor.telegram.paypalpayments.Models;
using Update = Telegram.Bot.Types.Update;

namespace Mentor.telegram.bot.api.Services
{
    public interface IUpdateService
    {
        Task ProcessAsync(string botId, Update update, Guid correlationId);

        Task ProcessPaymentAsync(string botId, EventInfo eventInfo, Guid correlationId);

        Task ProcessCoinGateAsync(string botId, EventInfo eventInfo, Guid correlationId);

        Task ProcessPaypalExecution(string paymentId, string paypalPayerId, Guid correlationId);

        Task<bool> ValidatePaypalWebhook(string botId, VerifyWebhookSignatureRaw signature, Guid correlationId);

        Task ProcessWidgetAsync(string botId, Models.WidgetUpdate update, Guid correlationId);

        Task ProcessEvent(string botId, EventInfo eventInfo, Guid correlationId);
    }
}
