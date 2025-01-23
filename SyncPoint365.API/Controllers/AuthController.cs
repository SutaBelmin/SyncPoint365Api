using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SyncPoint365.API.Config;
using SyncPoint365.API.Helpers;
using SyncPoint365.API.RESTModels;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using SyncPoint365.Service.Helpers;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRefreshTokensService _refreshTokensService;
        private readonly IOptions<JWTSettings> _jwtSettings;

        public AuthController(IUsersRepository usersRepository, IConfiguration configuration, IOptions<JWTSettings> jwtSettings, IRefreshTokensService refreshTokensService)
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

                var accessToken = Auth.GenerateAccessToken(user, _jwtSettings.Value);
                var refreshToken = Auth.GenerateRefreshToken(user);

                await _refreshTokensService.SaveRefreshTokenAsync(refreshToken);

                return Ok(new { User = user, AccessToken = accessToken, RefreshToken = refreshToken.Token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }


        //[HttpPost]
        //[Route("validate-refresh-token")]
        //public async Task<IActionResult> ValidateRefreshToken([FromBody] AuthTokenValidationModel model)
        //{
        //    try
        //    {
        //        var refreshToken = await _refreshTokensRepository.GetRefreshTokenByUserIdAsync(model.UserId);

        //        if (refreshToken == null || refreshToken.ExpirationDate < DateTime.UtcNow)
        //        {
        //            return Unauthorized("Refresh token is inactive or expired.");
        //        }

        //        var user = await _usersRepository.GetByIdAsync(model.UserId);
        //        var accessToken = Auth.GenerateAccessToken(user, _jwtSettings.Value);

        //        return Ok(new { JwtToken = accessToken, RefreshToken = refreshToken.Token });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal Server Error: VALIDACIJA" + ex.Message);
        //    }
        //}

    }
}
