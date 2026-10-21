using BotCreator.Core.ServiceInterfaces;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCreator.Service.EmailProviders.Mailgun
{
    public class MailgunEmailProvider : IEmailProvider
    {        
        public Task<bool> SendResetPasswordEmailAsync(string resetToken, string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
