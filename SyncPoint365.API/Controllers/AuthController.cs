using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SyncPoint365.API.Config;
using SyncPoint365.API.RESTModels;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IOptions<JWTSettings> _jwtSettings;

        public AuthController(IUsersRepository usersRepository, IConfiguration configuration, IOptions<JWTSettings> jwtSettings)
        {
            _usersRepository = usersRepository;
            _jwtSettings = jwtSettings;
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

                return Ok(new { User = user, Token = GenerateToken(user) });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        private string GenerateToken(User user)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
               new Claim("Id",user.Id.ToString()),
               new Claim(ClaimTypes.Name, user.FirstName),
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.Role, user.Role.ToString()),
               new Claim(ClaimTypes.Email, user.Email)
           });

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Value.Key);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.Value.Duration),
                Issuer = _jwtSettings.Value.Issuer,
                Audience = _jwtSettings.Value.Audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
