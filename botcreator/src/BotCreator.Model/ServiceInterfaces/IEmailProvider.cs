using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCreator.Core.ServiceInterfaces
{
    public interface IEmailProvider
    {
        Task<bool> SendResetPasswordEmailAsync(string resetToken, string userEmail);


    }
}
