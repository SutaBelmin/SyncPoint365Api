using SyncPoint365.Core.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace SyncPoint365.Service.Helpers
{
    public static class Cryptography
    {
        public static string GenerateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(Constants.CryptographyParameters.Size);

            return Convert.ToBase64String(salt);
        }

        public static string GenerateHash(string password, string salt)
        {
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password: Encoding.UTF8.GetBytes(password),
                salt: Encoding.UTF8.GetBytes(salt),
                iterations: Constants.CryptographyParameters.Iterations,
                hashAlgorithm: HashAlgorithmName.SHA512,
                outputLength: Constants.CryptographyParameters.Size
            );
            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            string hash = GenerateHash(password, storedSalt);
            return hash.Equals(storedHash);
        }

        public static void CreatePasswordHashAndSalt(string password, out string passwordHash, out string passwordSalt)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            passwordSalt = Convert.ToBase64String(saltBytes);

            using (var hmac = new HMACSHA512(saltBytes))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordHash = Convert.ToBase64String(hashBytes);
            }
        }

    }
}

