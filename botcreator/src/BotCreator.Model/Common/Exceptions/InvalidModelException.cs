using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using BotCreator.Core.Common.Models;

namespace BotCreator.Core.Common.Exceptions
{
    public class InvalidModelException : BotBaseException
    {
        private object _validationState;

        public InvalidModelException(object validationState) : base()
        {
            _validationState = validationState;
        }

        public InvalidModelException(string message) : base(message) { }

        public override HttpStatusCode ResponseStatusCode => HttpStatusCode.BadRequest;

        public override ErrorConstruct ErrorMessage => new ErrorConstruct
        {
            ErrorMessage = "Wrong input was sent",
            ErrorObj = _validationState
        };
    }
}
