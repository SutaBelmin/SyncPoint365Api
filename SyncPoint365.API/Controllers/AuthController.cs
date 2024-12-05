using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SyncPoint365.API.RESTModels;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Repository.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IUsersRepository usersRepository, IConfiguration configuration, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Login", Name = "SyncPoint365-Login")]
        public async Task<IActionResult> Login([FromBody] AuthModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _usersRepository.GetUserByEmailAsync(model.Email, cancellationToken);

                if (user == null)
                {
                    throw new UnauthorizedAccessException("Invalid email or password!");
                }

                if (!VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new UnauthorizedAccessException("Invalid email or password!");
                }

                var userDTO = _mapper.Map<UserDTO>(user);

                var token = GenerateToken(userDTO);

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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
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

        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return storedHash == Convert.ToBase64String(hash);
            }
        }
    }
}
