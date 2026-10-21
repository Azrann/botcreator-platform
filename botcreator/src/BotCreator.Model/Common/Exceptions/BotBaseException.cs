using BotCreator.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace BotCreator.Core.Common.Exceptions
{
    public class BotBaseException : Exception
    {
        public BotBaseException() : base() { }

        public BotBaseException(string message) : base(message) { }

        public virtual HttpStatusCode ResponseStatusCode => HttpStatusCode.InternalServerError;

        public virtual ErrorConstruct ErrorMessage => new ErrorConstruct
        {
            ErrorMessage = Message,
            ErrorStack = StackTrace,
            ErrorObj = this
        };
    }
}
