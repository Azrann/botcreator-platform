using System.Security.Claims;
using Functions.Services;
using Microsoft.AspNetCore.Http;

namespace Functions.Token
{
    public interface IAccessTokenProvider
    {
        /// <summary>
        /// Validate the access token, returning the security principal in a result.
        /// </summary>
        /// <param name="request">The HTTP request containing the access token.</param>
        /// <returns>A result that contains the security principal.</returns>
        AccessTokenResult ValidateToken(HttpRequest request);
    }
}