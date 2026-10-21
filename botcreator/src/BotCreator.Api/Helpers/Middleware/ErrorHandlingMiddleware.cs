using BotCreator.Core.Common.Exceptions;
using BotCreator.Core.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BotCreator.Api.Helpers.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _log;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> log)
        {
            _log = log;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var appException = exception as BotBaseException;
            var errorObj = new ErrorResult();

            if (appException != null)
            {
                code = appException.ResponseStatusCode;
                errorObj.Error = appException.ErrorMessage;
            }
            else
                errorObj.Error = new ErrorConstruct
                {
                    ErrorMessage = exception.Message,
                    ErrorStack = exception.StackTrace
                };

            var result = JsonConvert.SerializeObject(errorObj, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
