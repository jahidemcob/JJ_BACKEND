namespace Backend.src.app.auth.application.DTOs
{
    public class LoginResponseDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool PerfilCompleto { get; set; } = true;
    }
}