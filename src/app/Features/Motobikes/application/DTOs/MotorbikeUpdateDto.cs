namespace Backend.src.app.Features.Motobikes.application.DTOs
{
    public class MotorbikeUpdateDto
    {
        public int IdMoto { get; set; }
        public string? marca { get; set; }
        public string? modelo { get; set; }
        public int? cilindraje { get; set; }
        public int? anio { get; set; }
    }
}