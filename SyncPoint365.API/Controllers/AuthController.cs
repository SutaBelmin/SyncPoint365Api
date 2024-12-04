using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SyncPoint365.API.RESTModels;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Service.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IConfiguration _configuration;

        public AuthController(IUsersService usersService, IConfiguration configuration)
        {
            _usersService = usersService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login", Name = "SyncPoint365-Login")]
        public async Task<IActionResult> Login([FromBody] AuthModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _usersService.ValidateUserAsync(model.Email, model.Password, cancellationToken);

                var token = GenerateToken(user);

                return Ok(new { Message = "User authenticated successfully", User = user, Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid email or password!");
            }
        }

        [HttpPost]
        [Route("GenerateToken")]
        public string GenerateToken(UserDTO user)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.Name, user.Email),
               new Claim(ClaimTypes.NameIdentifier, user.Email),
               new Claim(ClaimTypes.Role, user.Role.ToString())
           });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddMinutes(90),
                Issuer = _configuration["JwtSetting:Issuer"],
                Audience = _configuration["JwtSetting:Audience"],
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
