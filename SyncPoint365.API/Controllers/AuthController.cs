using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SyncPoint365.API.Config;
using SyncPoint365.API.Helpers;
using SyncPoint365.API.RESTModels;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Helpers;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRefreshTokensRepository _refreshTokensService;
        private readonly IOptions<JWTSettings> _jwtSettings;

        public AuthController(IUsersRepository usersRepository, IConfiguration configuration, IOptions<JWTSettings> jwtSettings, IRefreshTokensRepository refreshTokensService)
        {
            _usersRepository = usersRepository;
            _jwtSettings = jwtSettings;
            _refreshTokensService = refreshTokensService;
        }

        [HttpPost]
        [Route("Login", Name = "SyncPoint365-Login")]
        public async Task<IActionResult> Login([FromBody] AuthModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _usersRepository.GetUserByEmailAsync(model.Email, cancellationToken);

                if (user == null || !Cryptography.VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return Unauthorized();
                }

                if (!user.IsActive)
                {
                    return Forbid();
                }

                return Ok(new
                {
                    User = user,
                    Token = Auth.GenerateTokens(user, _jwtSettings.Value)
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var tokenDetails = await _refreshTokensService.GetByTokenAsync(refreshToken);

            if (tokenDetails == null || tokenDetails.ExpirationDate < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token");
            }

            var user = await _usersRepository.GetByIdAsync(tokenDetails.UserId);
            var (newJwt, newRefreshToken) = Auth.GenerateTokens(user, _jwtSettings.Value);

            await _refreshTokensService.GenerateAndSaveRefreshTokenAsync(user.Id);

            return Ok(new { JwtToken = newJwt, RefreshToken = newRefreshToken.Token });
        }


    }
}
