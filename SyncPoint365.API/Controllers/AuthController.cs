using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SyncPoint365.API.Config;
using SyncPoint365.API.Helpers;
using SyncPoint365.API.RESTModels;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Service.Common.Interfaces;
using SyncPoint365.Service.Helpers;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IRefreshTokensService _refreshTokensService;
        private readonly IOptions<JWTSettings> _jwtSettings;
        private readonly IMapper _mapper;


        public AuthController(IUsersService usersService, IRefreshTokensService refreshTokensService, IConfiguration configuration,
            IOptions<JWTSettings> jwtSettings, IMapper mapper)
        {
            _usersService = usersService;
            _refreshTokensService = refreshTokensService;
            _jwtSettings = jwtSettings;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Login", Name = "SyncPoint365-Login")]
        public async Task<IActionResult> Login([FromBody] AuthModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _usersService.GetUserByEmailAsync(model.Email, cancellationToken);

                if (user == null || !Cryptography.VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return Unauthorized();
                }

                if (!user.IsActive)
                {
                    return Forbid();
                }

                var accessToken = Auth.GenerateAccessToken(user, _jwtSettings.Value);
                var refreshToken = Auth.GenerateRefreshToken(user, _jwtSettings.Value);

                await _refreshTokensService.ManageRefreshToken(user.Id, refreshToken.RefreshToken, refreshToken.Expiration);
                var userAuthDto = _mapper.Map<UserAuthDTO>(user);
                return Ok(new { User = userAuthDto, AccessToken = accessToken, refreshToken.RefreshToken });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpGet("Validate-Token")]
        public async Task<IActionResult> CompareTokensAsync(string refreshToken)
        {
            try
            {
                var userId = Auth.GetUserIdFromRefreshToken(refreshToken);

                if (userId == null)
                {
                    return Unauthorized("Invalid access token.");
                }

                var storedRefreshToken = await _refreshTokensService.GetRefreshTokenByUserIdAsync(userId.Value);
                if (storedRefreshToken == null)
                {
                    return Unauthorized(new { code = "TOKEN_EMPTY", message = "Refresh token empty." });
                }

                if (storedRefreshToken.ExpirationDate < DateTime.Now)
                {
                    await _refreshTokensService.DeleteAsync(storedRefreshToken.Id);
                    return Unauthorized(new { code = "TOKEN_EXPIRED", message = "Refresh token has expired." });
                }

                if (storedRefreshToken.Token == refreshToken)
                {
                    var user = await _usersService.GetByIdAsync(userId.Value);
                    var userLoginDto = _mapper.Map<UserLoginDTO>(user);
                    if (user == null)
                    {
                        return Unauthorized("User not found.");
                    }

                    var newAccessToken = Auth.GenerateAccessToken(userLoginDto, _jwtSettings.Value);
                    return Ok(newAccessToken);
                }

                return Unauthorized("Refresh token does not match.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
