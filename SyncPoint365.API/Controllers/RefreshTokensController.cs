using Microsoft.AspNetCore.Mvc;
using SyncPoint365.API.Config;
using SyncPoint365.API.Helpers;
using SyncPoint365.Core.DTOs.RefreshTokens;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefreshTokensController(IRefreshTokensService refreshTokensService, IUsersRepository usersRepository, IConfiguration configuration) : BaseController<RefreshTokenDTO, RefreshTokenAddDTO, RefreshTokenUpdateDTO>(refreshTokensService)
    {
        private readonly IRefreshTokensService _refreshTokensService = refreshTokensService;
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly string _jwtSecretKey = configuration["JwtSettings:Key"];

        [HttpGet("Check-Access-Token")]
        public IActionResult ValidateAccessToken(string accessToken)
        {
            try
            {
                if (IsAccessTokenValid(accessToken))
                {
                    return Ok("Access token is valid.");
                }
                return Unauthorized("Access token is invalid or expired.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
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

                if (storedRefreshToken.ExpirationDate < DateTime.UtcNow)
                {
                    await _refreshTokensService.DeleteAsync(storedRefreshToken.Id);
                    return Unauthorized("No refresh token found for this user.");
                }

                if (storedRefreshToken.Token == refreshToken)
                {
                    var user = await _usersRepository.GetByIdAsync(userId.Value);
                    var newAccessToken = Auth.GenerateAccessToken(user, new JWTSettings { Key = _jwtSecretKey });
                    return Ok(newAccessToken);
                }

                return Unauthorized("Refresh token does not match.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        private bool IsAccessTokenValid(string accessToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

                if (jsonToken?.ValidTo >= DateTime.UtcNow)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error validating access token: " + ex.Message);
            }

            return false;
        }

        private string GetUserEmailFromAccessToken(string accessToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;
                var userEmailClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "email");

                if (userEmailClaim != null)
                {
                    return userEmailClaim.Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("User Email not found. Error: " + ex.Message);
            }

            return null;
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
