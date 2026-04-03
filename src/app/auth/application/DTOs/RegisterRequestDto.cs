namespace Auth.Application.DTOs
{
    public class RegisterRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}