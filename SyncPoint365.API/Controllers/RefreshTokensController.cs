using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SyncPoint365.API.Config;
using SyncPoint365.API.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefreshTokensController : ControllerBase
    {
        private readonly IRefreshTokensService _refreshTokensService;
        private readonly IUsersRepository _usersRepository;
        private readonly IOptions<JWTSettings> _jwtSettings;

        public RefreshTokensController(
            IRefreshTokensService refreshTokensService, IUsersRepository usersRepository, IOptions<JWTSettings> jwtSettings)
        {
            _refreshTokensService = refreshTokensService;
            _usersRepository = usersRepository;
            _jwtSettings = jwtSettings;
        }

        [HttpGet("Compare-Tokens")]
        public async Task<IActionResult> CompareTokensAsync(string accessToken, string refreshToken)
        {
            try
            {
                var userId = GetUserIdFromAccessToken(accessToken);
                var userEmail = GetUserEmailFromAccessToken(accessToken);

                if (userId == null)
                {
                    return Unauthorized("Invalid access token.");
                }

                var storedRefreshToken = await _refreshTokensService.GetRefreshTokenByUserIdAsync(userId.Value);
                if (storedRefreshToken == null)
                {
                    return Unauthorized("No refresh token found for this user.");
                }

                if (storedRefreshToken.ExpirationDate < DateTime.Now)
                {
                    await _refreshTokensService.DeleteAsync(storedRefreshToken.Id);
                    return Unauthorized("Refresh token has expired.");
                }

                if (storedRefreshToken.Token == refreshToken)
                {
                    var user = await _usersRepository.GetUserByEmailAsync(userEmail);
                    if (user == null)
                    {
                        return Unauthorized("User not found.");
                    }

                    var newAccessToken = Auth.GenerateAccessToken(user, _jwtSettings.Value);
                    return Ok(newAccessToken);
                }

                return Unauthorized("Refresh token does not match.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        private string GetUserEmailFromAccessToken(string accessToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;
                var userEmailClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "email");

                return userEmailClaim?.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("User email not found. Error: " + ex.Message);
                return null;
            }
        }

        private int? GetUserIdFromAccessToken(string accessToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;
                var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "nameid");

                return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("User ID not found. Error: " + ex.Message);
                return null;
            }
        }
    }
}
