using BotCreator.Core.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BotCreator.Api.Helpers
{
    public class BaseBotController : Controller
    {
        protected UserInfo LoggedInUser
        {
            get
            {
                return new UserInfo
                {
                    Id = int.Parse(User.FindFirstValue(ClaimTypes.Name)),
                    Email = User.FindFirstValue(ClaimTypes.Email),
                };

            }
        }

        protected Result<T> WrapResult<T>(T data)
        {
            return new Result<T>
            {
                Data = data,
                Error = null
            };
        }
    }
}
