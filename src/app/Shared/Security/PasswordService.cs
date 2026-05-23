namespace Backend.src.app.Shared.Security
{
    public static class PasswordService
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(storedHash);
        }

        public static (byte[] Hash, byte[] Salt) HashPassword(string password)
        {
            CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            return (hash, salt);
        }
    }
}