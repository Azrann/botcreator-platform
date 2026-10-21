using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BotCreator.Core.Service;
using BotCreator.Api.ViewModels;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using BotCreator.Core.Configs;
using BotCreator.Api.Helpers;
using BotCreator.Core.DomainModels;
using BotCreator.Core.Common.Exceptions;
using System.Linq;
using System.Collections.Generic;
using BotCreator.Api.ViewModels.User;
using Microsoft.Extensions.Logging;
using BotCreator.Infrastructure.Auth.Jwt;

namespace BotCreator.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : BaseBotController
    {
        private IUserService _userService;
        private AppSettings _appSettings;
        private IMapper _mapper;
        private ITokenHandler _tokenGenerator;

        private readonly ILogger _logger;

        public UserController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            ILogger<UserController> logger,
            ITokenHandler tokenGenerator)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _tokenGenerator = tokenGenerator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<UserWithTokenDto> AuthenticateAsync([FromBody]UserLoginDto userDto)
        {
            var user = await _userService.AuthenticateAsync(userDto.Email, userDto.Password);

            if (user == null)
                throw new NotAuthenticatedException();

            return GenerateToken(user, userDto.KeepSigned);
        }



        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<UserWithoutPasswordDto> RegisterAsync([FromBody]UserDto userDto)
        {
            var user = _mapper.Map<UserModel>(userDto);
            user = await _userService.CreateAsync(user, userDto.Password);
            return _mapper.Map<UserWithoutPasswordDto>(user);
        }


        [HttpGet("loggedin/userinfo")]
        public async Task<UserWithoutPasswordDto> GetLoggedInUserInfoAsync()
        {
            var user = await _userService.GetLoggedInUserInfoAsync(LoggedInUser.Id);
            return _mapper.Map<UserWithoutPasswordDto>(user);
        }

        [HttpGet("")]
        public async Task<List<UserWithoutPasswordDto>> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return users.Select(s => _mapper.Map<UserWithoutPasswordDto>(s)).ToList();
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task ResetPasswordRequestAsync(ResetPasswordRequest email)
        {
            await _userService.ResetPasswordRequestAsync(email.Email);
        }

        [AllowAnonymous]
        [HttpPost("set-new-password")]
        public async Task SetNewPasswordAsync([FromBody]ResetTokenConfirmation confirmation)
        {
            await _userService.SetNewPasswordAsync(confirmation);
        }

        [AllowAnonymous]
        [HttpPost("refresh-access-token")]
        public async Task<UserWithTokenDto> RefreshAccessToken([FromBody]RefreshTokenDto refreshTokenDto)
        {
            if (refreshTokenDto == null || string.IsNullOrEmpty(refreshTokenDto.RefreshToken))
            {
                throw new NotAuthenticatedException();
            }
            var decodedUser = _tokenGenerator.DecodeToken(refreshTokenDto.RefreshToken);
            var user = await _userService.GetByIdAsync(decodedUser.Id);

            if (!_tokenGenerator.ValidateRefreshToken(refreshTokenDto.RefreshToken))
            {
                throw new NotAuthenticatedException();
            }

            if (await _userService.GetByIdAsync(user.Id) == null)
            {
                throw new NotAuthenticatedException();
            }

            return GenerateToken(user);
        }

        private UserWithTokenDto GenerateToken(UserModel user, bool keepSigned = true)
        {
            var accessToken = _tokenGenerator.GenerateAccessToken(user);

            var token = new UserWithTokenDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = accessToken
            };

            if (keepSigned)
            {
                var refreshToken = _tokenGenerator.GenerateRefreshToken(user);
                token.RefreshToken = refreshToken;
            }

            return token;
        }
    }
}
