using System;
using Mentor.telegram.bot.api.services.Services.Email;
using Xunit;

namespace Mentor.telegram.bot.api.services.test
{
    public class EmailServiceTest
    {
        [Fact]
        public async void ICanSendAnEmail()
        { 
            EmailService service = new EmailService();
            var response = await service.Send(new SendEmailEvent()
            {
                BodyHtml = "",
                CorrelationId = Guid.NewGuid(),
                EmailTo = "user@example.invalid",
                EmailCc = "user@example.invalid,user@example.invalid",
                PlainTextContent = "Hey hello world",
                Subject = "Firs test"
            });
        }
    }
}
