using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BotCreator.Core.Configs;
using BotCreator.Core.DomainModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace BotCreator.Infrastructure.Auth.Jwt
{
    public class TokenHandler : ITokenHandler
    {
        private AppSettings _appSettings;

        private ILogger<TokenHandler> _log;

        public TokenHandler(IOptions<AppSettings> appSettings, ILogger<TokenHandler> log)
        {
            _log = log;
            _appSettings = appSettings.Value;
        }

        public string GenerateAccessToken(UserModel user)
        {
            return GenerateToken(user, _appSettings.BearerTokenExpireMinutes, _appSettings.Secret);
        }

        public string GenerateRefreshToken(UserModel user)
        {
            return GenerateToken(user, _appSettings.RefreshTokenExpireMinutes, _appSettings.Secret);
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret)),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out _);
                return true;
            }
            catch (Exception ex)
            {
                _log.LogWarning($"Refresh token validation failed with message {ex.Message}");
                return false;
            }
        }

        public UserModel DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDecoded = tokenHandler.ReadJwtToken(token);

            var userId = tokenDecoded.Claims.First(c => c.Type == "unique_name").Value;
            var userEmail = tokenDecoded.Claims.First(c => c.Type == "email").Value;

            return new UserModel
            {
                Id = int.Parse(userId),
                Email = userEmail
            };
        }

        private string GenerateToken(UserModel user, int expireMinutes, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
