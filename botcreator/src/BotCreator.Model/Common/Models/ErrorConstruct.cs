using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotCreator.Core.Common.Models
{
    public class ErrorConstruct
    {
        public string ErrorMessage { get; set; }

        public string ErrorStack { get; set; }

        public object ErrorObj { get; set; }
    }
}
