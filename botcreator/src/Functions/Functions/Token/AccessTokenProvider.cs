using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Functions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Functions.Token
{
    /// <summary>
    /// Validates a incoming request and extracts any <see cref="ClaimsPrincipal"/> contained within the bearer token.
    /// </summary>
    public class AccessTokenProvider : IAccessTokenProvider
    {
        private const string AUTH_HEADER_NAME = "Authorization";
        private const string BEARER_PREFIX = "bearer ";
        private readonly string _issuerToken;

        public AccessTokenProvider(string issuerToken)
        {
            _issuerToken = issuerToken;
        }

        public AccessTokenResult ValidateToken(HttpRequest request)
        {
            try
            {
                // Get the token from the header
                if (request != null &&
                    request.Headers.ContainsKey(AUTH_HEADER_NAME) &&
                    request.Headers[AUTH_HEADER_NAME].ToString().ToLower().StartsWith(BEARER_PREFIX))
                {
                    var token = request.Headers[AUTH_HEADER_NAME].ToString().Substring(BEARER_PREFIX.Length);

                    // Create the parameters
                    var tokenParams = new TokenValidationParameters()
                    {
                        RequireSignedTokens = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes((_issuerToken)))
                    };

                    // Validate the token
                    var handler = new JwtSecurityTokenHandler();
                    var result = handler.ValidateToken(token, tokenParams, out var securityToken);
                    return AccessTokenResult.Success(result);
                }
                else
                {
                    return AccessTokenResult.NoToken();
                }
            }
            catch (SecurityTokenExpiredException)
            {
                return AccessTokenResult.Expired();
            }
            catch (Exception ex)
            {
                return AccessTokenResult.Error(ex);
            }
        }
    }
}