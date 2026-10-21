using AutoMapper;
using BotCreator.Core.Common.Exceptions;
using BotCreator.Core.Configs;
using BotCreator.Core.DomainModels;
using BotCreator.Core.Service;
using BotCreator.Infrastructure.Auth;
using BotCreator.Infrastructure.Data.EfCore;
using BotCreator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BotCreator.Core.ServiceInterfaces;
using Microsoft.Extensions.Logging;

namespace BotCreator.Service.User
{
    public class UserService : IUserService
    {
        private BotManagerContext _context;
        private IMapper _mapper;
        private AppSettings _settings;
        private IEmailProvider _email;
        private ILogger _log;

        public UserService(BotManagerContext context, IMapper mapper, IOptions<AppSettings> appSettings,
            IEmailProvider email, ILogger<UserService> log)
        {
            _settings = appSettings.Value;
            _context = context;
            _mapper = mapper;
            _email = email;
            _log = log;
        }

        public async Task<UserModel> AuthenticateAsync(string email, string password)
        {
            _log.Log(LogLevel.Warning, $"Authenticatio attempt with email: {email}");
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return null;

            if (!UserManagementHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> CreateAsync(UserModel userModel, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new BotBaseException("Password is required");

            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == userModel.Email);

            if (existingUser != null)
                throw new BotBaseException("Email \"" + userModel.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            UserManagementHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<BotCreator.Infrastructure.Data.EfCore.User>(userModel);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> GetLoggedInUserInfoAsync(int userId)
        {
            return _mapper.Map<UserModel>(await _context.Users.FirstOrDefaultAsync(u => u.Id == userId));
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            return _mapper.Map<UserModel>(await _context.Users.FirstOrDefaultAsync(s => s.Id == id));
        }

        public async Task ResetPasswordRequestAsync(string email)
        {
            var resetTokenGuid = Guid.NewGuid().ToString();
            var user = await _context.Users.FirstOrDefaultAsync(s => s.Email == email);

            if (user == null)
                throw new BotBaseException("User does not exist");

            var resetToken = new ResetToken
            {
                UserId = user.Id,
                Token = resetTokenGuid,
                Issued = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(_settings.ResetTokenExpireHours)
            };

            _log.LogInformation($"Added reset token {resetTokenGuid} for user: {email}");
            _context.ResetTokens.Add(resetToken);
            await _context.SaveChangesAsync();

            await _email.SendResetPasswordEmailAsync(resetTokenGuid, user.Email);
        }

        public async Task SetNewPasswordAsync(ResetTokenConfirmation confirmation)
        {
            await CheckTokenValidityAsync(confirmation.Token);

            var userToReset = await
               (from user in _context.Users
                join resetToken in _context.ResetTokens on user.Id equals resetToken.UserId
                where resetToken.Token == confirmation.Token
                select new { User = user, Token = resetToken }).FirstOrDefaultAsync();
            if (userToReset == null)
                return;

            byte[] passwordHash, passwordSalt;
            UserManagementHelper.CreatePasswordHash(confirmation.Password, out passwordHash, out passwordSalt);
            userToReset.User.PasswordHash = passwordHash;
            userToReset.User.PasswordSalt = passwordSalt;

            _context.ResetTokens.Remove(userToReset.Token);
            await _context.SaveChangesAsync();
        }

        private async Task CheckTokenValidityAsync(string token)
        {
            var resetToken = await _context.ResetTokens.FirstOrDefaultAsync(r => r.Token == token && r.Expires >= DateTime.UtcNow);
            if (resetToken == null)
                throw new NotAuthenticatedException("Token is invalid");
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _context.Users.Select(s => _mapper.Map<UserModel>(s)).ToListAsync();
        }
    }
}
