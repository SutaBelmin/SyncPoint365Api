using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.RefreshTokens;
using SyncPoint365.Service.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefreshTokensController(IRefreshTokensService refreshTokensService, IConfiguration configuration) : BaseController<RefreshTokenDTO, RefreshTokenAddDTO, RefreshTokenUpdateDTO>(refreshTokensService)
    {
        private readonly IRefreshTokensService _refreshTokensService = refreshTokensService;
        private readonly string _jwtSecretKey = configuration["JwtSettings:Key"];

        [HttpGet("Compare-Token")]
        public async Task<IActionResult> CompareTokensAsync(string accessToken, string refreshToken)
        {
            try
            {
                var userId = GetUserIdFromAccessToken(accessToken);
                if (userId == null)
                {
                    return Unauthorized("Invalid access token.");
                }

                var storedRefreshToken = await _refreshTokensService.GetRefreshTokenByUserIdAsync(userId.Value);
                if (storedRefreshToken == null)
                {
                    return Unauthorized("No refresh token found for this user.");
                }

                if (storedRefreshToken.ExpirationDate < DateTime.UtcNow)
                {
                    await _refreshTokensService.DeleteAsync(storedRefreshToken.Id);
                    return Unauthorized("No refresh token found for this user.");
                }

                if (storedRefreshToken.Token == refreshToken)
                {
                    return Ok("Refresh token is a match.");
                }

                return Unauthorized("Refresh token does not match.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        private int? GetUserIdFromAccessToken(string accessToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;
                var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "nameid");

                if (userIdClaim != null)
                {
                    return int.Parse(userIdClaim.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("User ID not found. Error: " + ex.Message);
            }

            return null;
        }
    }
}
