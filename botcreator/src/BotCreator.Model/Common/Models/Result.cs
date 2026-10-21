using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotCreator.Core.Common.Models
{
    public class Result<T> : ErrorResult
    {
        public T Data { get; set; }
    }

    public class Result : ErrorResult
    {
        public object Data { get; set; }
    }

    public class ErrorResult
    {
        public ErrorConstruct Error { get; set; }
    } 

}
