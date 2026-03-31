namespace Auth.Application.DTOs
{
    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
    }
}