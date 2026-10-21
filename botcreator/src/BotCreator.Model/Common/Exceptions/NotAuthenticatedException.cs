using BotCreator.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BotCreator.Core.Common.Exceptions
{
    public class NotAuthenticatedException : BotBaseException
    {
        public NotAuthenticatedException() : base() { }

        public NotAuthenticatedException(string message) : base(message) { }

        public override HttpStatusCode ResponseStatusCode => HttpStatusCode.Unauthorized;

        public override ErrorConstruct ErrorMessage => new ErrorConstruct
        {
            ErrorMessage = "User is not authenticated",
            ErrorObj = this,
            ErrorStack = StackTrace
        };
    }
}
