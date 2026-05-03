using System.ComponentModel.DataAnnotations;

namespace Backend.src.app.Features.Users.application.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdUsuario debe ser un número positivo mayor a 0.")]
        public int IdUsuario { get; set; }
        public required string Nombre { get; set; }
        public required string NombreUsuario { get; set; }
        public required string Telefono { get; set; }
        public required string Correo { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El Id del rol debe ser un número positivo mayor a 0.")]
        public int IdRol { get; set; }
        public string? NuevaClave { get; set; }

    }
}