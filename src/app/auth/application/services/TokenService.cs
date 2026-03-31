namespace Auth.Application.Services
{
    public class TokenService
    {
        // Por ahora devolvemos un token falso
        public string GenerateToken(int userId, string role)
        {
            return $"FAKE-TOKEN-{userId}-{role}";
        }
    }
}