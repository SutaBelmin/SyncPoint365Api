using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.RefreshTokens;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefreshTokensController : BaseController<RefreshTokenDTO, RefreshTokenAddDTO, RefreshTokenUpdateDTO>
    {
        private readonly IRefreshTokensService _refreshTokensService;

        public RefreshTokensController(IRefreshTokensService refreshTokensService) : base(refreshTokensService)
        {
            _refreshTokensService = refreshTokensService;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetByTokenAsync(string token)
        {
            var refreshToken = await _refreshTokensService.GetByTokenAsync(token);
            if (refreshToken == null)
            {
                return NotFound();
            }
            return Ok(refreshToken);
        }

        [HttpPost("Generate")]
        public async Task<IActionResult> GenerateRefreshTokenAsync([FromQuery] int userId)
        {
            var refreshToken = await _refreshTokensService.GenerateAndSaveRefreshTokenAsync(userId);
            await _refreshTokensService.RemoveExpiredTokensAsync();
            return Ok(refreshToken);
        }
    }
}
