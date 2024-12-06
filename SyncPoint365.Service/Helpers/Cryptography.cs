using SyncPoint365.Core.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace SyncPoint365.Service.Helpers
{
    public static class Cryptography
    {
        public static string GenerateSalt()
        {
            byte[] salt = new byte[Constants.CryptographyParameters.size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public static string GenerateHash(string password, string salt)
        {
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password: Encoding.UTF8.GetBytes(password),
                salt: Encoding.UTF8.GetBytes(salt),
                iterations: Constants.CryptographyParameters.iterations,
                hashAlgorithm: HashAlgorithmName.SHA512,
                outputLength: Constants.CryptographyParameters.size
            );

            return Convert.ToHexString(hash);

        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            string hash = GenerateHash(password, storedSalt);
            return hash.Equals(storedHash);
        }
    }
}