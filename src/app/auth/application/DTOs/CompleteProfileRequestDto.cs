namespace Backend.src.app.auth.application.DTOs
{
    public class CompleteProfileRequestDto
    {
        public required int IdUsuario { get; set; }
        public required string NombreUsuario { get; set; } = string.Empty;
        public required string Telefono { get; set; } = string.Empty;
        public required string Clave { get; set; } = string.Empty;
    }
}