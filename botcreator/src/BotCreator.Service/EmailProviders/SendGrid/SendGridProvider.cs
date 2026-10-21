using BotCreator.Core.Configs;
using BotCreator.Core.ServiceInterfaces;
using Core.Common.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BotCreator.Service.EmailProviders.SendGrid
{
    public class SendGridProvider : IEmailProvider
    {
        private ILogger<SendGridProvider> _log;
        private IHostingEnvironment _env;
        private SendGridSettings _emailSettings;
        private AppSettings _appSettings;


        public SendGridProvider(ILogger<SendGridProvider> log,
            IHostingEnvironment env, IOptions<AppSettings> appSettings)
        {
            _log = log;
            _env = env;
            _appSettings = appSettings.Value;
            _emailSettings = appSettings.Value.EmailSettings.SendGrid;
        }

        public async Task<bool> SendResetPasswordEmailAsync(string resetToken, string userEmail)
        {
            var apiKey = _emailSettings.ApiKey;
            var subject = _emailSettings.Subject;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
            var to = new EmailAddress(userEmail);

            var emailContent = BuildEmailContent(resetToken);
            var htmlContent = emailContent;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, string.Empty, htmlContent);
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                _log.LogError($"SendGrid failed to send email to {userEmail}, response: {JsonConvertExt.SerializeObject(response)}");
                return false;
            }
            _log.LogInformation($"SendGrid completed send email to {userEmail}");
            return true;
        }

        private string BuildEmailContent(string resetToken)
        {
            var builder = new StringBuilder();
            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);
            var path = Path.Combine(directory, @"EmailProviders\Templates\ForgetPassword.html");

            using (var reader = File.OpenText(path))
            {
                builder.Append(reader.ReadToEnd());
            }
            var url = BuildResetUrl(resetToken);
            builder.Replace("{{link}}", url);

            return builder.ToString();
        }

        private string BuildResetUrl(string resetToken)
        {
            var uri = new StringBuilder(_appSettings.ResetTokenBaseUrl);
            uri.Append("?");
            uri.Append(_appSettings.ResetTokenQueryField);
            uri.Append("=");
            uri.Append(resetToken);

            return uri.ToString();
        }
    }
}
