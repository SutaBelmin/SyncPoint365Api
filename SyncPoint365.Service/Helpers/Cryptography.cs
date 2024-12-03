using SyncPoint365.Core.Helpers;
using System.Security.Cryptography;
using System.Text;
﻿using System.Security.Cryptography;

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

         public static void CreatePasswordHashAndSalt(string password, out string passwordHash, out string passwordSalt, int iterations = 10000)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            passwordSalt = Convert.ToBase64String(saltBytes);


            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations, HashAlgorithmName.SHA512))
            {
                byte[] hashBytes = pbkdf2.GetBytes(64);

                passwordHash = Convert.ToBase64String(hashBytes);
            }
        }
    }
}
       
