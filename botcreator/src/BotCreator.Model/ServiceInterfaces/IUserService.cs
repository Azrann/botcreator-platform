using BotCreator.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotCreator.Core.Service
{
    public interface IUserService
    {
        Task<UserModel> AuthenticateAsync(string email, string password);

        Task<UserModel> GetLoggedInUserInfoAsync(int userId);

        Task<UserModel> GetByIdAsync(int id);

        Task<UserModel> CreateAsync(UserModel user, string password);

        Task ResetPasswordRequestAsync(string token);

        Task SetNewPasswordAsync(ResetTokenConfirmation confirmation);

        Task<List<UserModel>> GetAllUsersAsync();
    }
}
