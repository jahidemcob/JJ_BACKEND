using System.ComponentModel.DataAnnotations;

namespace Backend.src.app.Features.Services.application.DTOs
{
    public class ServiceUpdateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El Id del servicio debe ser un número positivo mayor a 0.")]
        public int IdServicio { get; set; }
        public required string NombreServicio { get; set; }
        public required string Descripcion { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "El precio base debe ser un número positivo mayor a 1.")]
        public decimal PrecioBase { get; set; }
    }
}