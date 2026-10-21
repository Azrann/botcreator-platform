using BotCreator.Core.Common.Exceptions;
using BotCreator.Core.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BotCreator.Api.Helpers.Middleware.Filters
{
    public class ResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result as ObjectResult;
            if (!context.ModelState.IsValid)
            {
                throw new InvalidModelException(result.Value);
            }

            if (result == null)
                return;

            result.Value = new Result
            {
                Data = result.Value,
                Error = null
            };
        }
    }
}
