using System.ComponentModel.DataAnnotations;

namespace Backend.src.app.Features.Motobikes.application.DTOs
{
    public class MotorbikeUpdateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El IdMoto debe ser un número positivo mayor a 0.")]
        public int IdMoto { get; set; }
        public string? marca { get; set; }
        public string? modelo { get; set; }
        public int? cilindraje { get; set; }
        public int? anio { get; set; }
    }
}