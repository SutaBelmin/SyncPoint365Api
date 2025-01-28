using Microsoft.IdentityModel.Tokens;
using SyncPoint365.API.Config;
using SyncPoint365.API.RESTModels;
using SyncPoint365.Core.DTOs.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SyncPoint365.API.Helpers
{
    public static class Auth
    {
        public static int GetLoggedUserId(ClaimsPrincipal user)
        {
            if (user == null || !user.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                throw new UnauthorizedAccessException("Logged user not found or does not have an ID!");

            return int.Parse(user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        public static string GetLoggedUserRole(ClaimsPrincipal user)
        {
            if (user == null || !user.HasClaim(c => c.Type == ClaimTypes.Role))
                throw new UnauthorizedAccessException("Logged user not found or does not have a role!");

            return user.Claims.First(c => c.Type == ClaimTypes.Role).Value;
        }

        public static string GenerateAccessToken(UserLoginDTO user, JWTSettings jwtSettings)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(jwtSettings.AccessTokenDuration),
                Audience = jwtSettings.Audience,
                Issuer = jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(jwtToken);
        }

        public static RefreshTokenModel GenerateRefreshToken(UserLoginDTO user, JWTSettings jwtSettings)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(jwtSettings.RefreshTokenDuration),
                Audience = jwtSettings.Audience,
                Issuer = jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            var response = new RefreshTokenModel
            {
                RefreshToken = tokenHandler.WriteToken(jwtToken),
                Expiration = tokenDescriptor.Expires.Value
            };
            return response;
        }

        public static int? GetUserIdFromRefreshToken(string refreshToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(refreshToken) as JwtSecurityToken;
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
