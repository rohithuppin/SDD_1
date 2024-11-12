using System.Security.Cryptography;

namespace SDDUserApi.Utilities
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 128 / 8; // 128 bits
        private const int HashSize = 256 / 8; // 256 bits
        private const int Iterations = 10000;

        public string HashPassword(string password)
        {
            var salt = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            var hashedPassword = Convert.ToBase64String(hashBytes);
            return hashedPassword;
        }

        public bool VerifyPasswordHash(string password, string hashedPassword)
        {

            string Upwd = HashPassword(password);

            var hashBytes = Convert.FromBase64String(hashedPassword);
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[SaltSize + i] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
