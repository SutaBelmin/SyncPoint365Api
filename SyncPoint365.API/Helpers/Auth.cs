using Microsoft.IdentityModel.Tokens;
using SyncPoint365.API.Config;
using SyncPoint365.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public static (string JwtToken, RefreshToken RefreshToken) GenerateTokens(User user, JWTSettings jwtSettings)
        public static string GenerateAccessToken(User user, JWTSettings jwtSettings)
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
                Expires = DateTime.Now.AddSeconds(jwtSettings.Duration),
                Audience = jwtSettings.Audience,
                Issuer = jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(jwtToken);
        }

        public static RefreshToken GenerateRefreshToken(User user)
        {
            return new RefreshToken
            {
                UserId = user.Id,
                Token = GenerateSecureToken(),
                ExpirationDate = DateTime.Now.AddSeconds(35)
            };
        }

        private static string GenerateSecureToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
