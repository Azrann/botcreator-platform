using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Functions.Services
{
    public class JwtTokenService
    {
        public bool Validate(string token)
        {
            var key = Encoding.ASCII.GetBytes("REPLACE_WITH_YOUR_VALUE");
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            try
            {
                tokenHandler.ValidateToken(CleanToken(token), validationParameters, out validatedToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            return validatedToken != null;
        }

        public TokenInfo Read(string token)
        {
            var stream = CleanToken(token);
            var handler = new JwtSecurityTokenHandler();
            var tokens = handler.ReadToken(stream) as JwtSecurityToken;

            var email = tokens.Claims.First(claim => claim.Type == "email").Value;
            var userId = tokens.Claims.First(claim => claim.Type == "unique_name").Value;

            return new TokenInfo
            {
                Email = email,
                UserId = userId
            };
        }

        private string CleanToken(string token)
        {
            var tokenWithoutSpaces = token.Replace(" ", "");
            if (tokenWithoutSpaces.ToLower().StartsWith("bearer"))
            {
                return tokenWithoutSpaces.Substring(6, tokenWithoutSpaces.Length - 6);
            }

            return tokenWithoutSpaces;
        }
    }
}
