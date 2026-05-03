using System.ComponentModel.DataAnnotations;

namespace Backend.src.app.Features.Services.application.DTOs
{
    public class ServiceCreateDto
    {
        public required string NombreServicio { get; set; }
        public required string Descripcion { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "El precio base debe ser un número positivo mayor a 0.")]
        public decimal PrecioBase { get; set; }
    }
}