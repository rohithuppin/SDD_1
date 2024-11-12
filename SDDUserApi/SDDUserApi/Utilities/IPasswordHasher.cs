namespace SDDUserApi.Utilities
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);

        public bool VerifyPasswordHash(string password, string hashedPassword);
    }
}
