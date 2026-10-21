using BotCreator.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotCreator.Infrastructure.Auth.Jwt
{
    public interface ITokenHandler
    {
        string GenerateAccessToken(UserModel user);

        string GenerateRefreshToken(UserModel user);

        bool ValidateRefreshToken(string refreshToken);

        UserModel DecodeToken(string token);
    }
}
