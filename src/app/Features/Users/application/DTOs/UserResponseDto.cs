namespace Backend.src.app.Features.Users.application.DTOs
{
    public class UserResponseDto
    {
        public int IdUsuario { get; set; }
        public required string Nombre { get; set; }
        public required string NombreUsuario { get; set; }
        public required string Telefono { get; set; }
        public required string Correo { get; set; }
        public int IdRol { get; set; }
        public required string Rol { get; set; }
        public bool Activo { get; set; }
    }
}