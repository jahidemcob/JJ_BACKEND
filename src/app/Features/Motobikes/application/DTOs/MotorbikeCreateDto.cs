using System.ComponentModel.DataAnnotations;

namespace Backend.src.app.Features.Motobikes.application.DTOs
{
    public class MotorbikeCreateDto
    {
        [Required]
        public required string marca { get; set; }

        [Required]
        public required string modelo { get; set; }

        [Required]
        public required string placa { get; set; }

        [Required]
        [Range(50, 2000, ErrorMessage = "El cilindraje debe estar entre 50 y 2000 cc.")]
        public int cilindraje { get; set; }

        [Required]
        [Range(2010, 2027, ErrorMessage = "El año debe estar entre 2010 y 2027.")]
        public int anio { get; set; }
    }
}