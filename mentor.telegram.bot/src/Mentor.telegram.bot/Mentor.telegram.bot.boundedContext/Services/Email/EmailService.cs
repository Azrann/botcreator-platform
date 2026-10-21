using System;
using System.Net;
using System.Threading.Tasks;
using Core.Common.Extensions;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Mentor.telegram.bot.api.services.Services.Email
{
    public class EmailService : IEmailService
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(EmailService));

        public async Task<bool> Send(SendEmailEvent message)
        {
                _logger.LogInf($"EmailService called with message {JsonConvertExt.SerializeObject(message)}",message.CorrelationId);

                var apiKey = "REPLACE_WITH_YOUR_VALUE";
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("user@example.invalid", "Support Team"),
                    Subject = message.Subject,
                    PlainTextContent = message.PlainTextContent,
                    HtmlContent = message.BodyHtml
                };

                msg.AddTo(new EmailAddress(message.EmailTo));
                if (!string.IsNullOrEmpty(message.EmailCc))
                {
                    foreach (var email in message.EmailCc.Split(','))
                    {
                        msg.AddCc(new EmailAddress(email));
                    }
                }

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    _logger.LogErr($"SendEmailEventHandler fail to send email to {message.EmailTo} {JsonConvertExt.SerializeObject(response)}", message.CorrelationId);
                    return false;
                }
                _logger.LogInf($"SendEmailEventHandler completed send email to {message.EmailTo} {JsonConvertExt.SerializeObject(response)}", message.CorrelationId);
                return true;
            }
    }

    public interface IEmailService
    {
        Task<bool> Send(SendEmailEvent message);
    }

    public class SendEmailEvent
    {
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string BodyHtml { get; set; }
        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
