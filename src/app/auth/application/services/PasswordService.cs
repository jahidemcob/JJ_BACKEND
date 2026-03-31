namespace Auth.Application.Services
{
    public class PasswordService
    {
        // Por ahora simple: compara las claves tal cual
        public bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return inputPassword == storedPassword;
        }
    }
}