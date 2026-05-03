namespace Backend.src.app.Features.Services.application.DTOs
{
    public class ServiceResponseDto
    {
        public int IdServicio { get; set; }
        public required string NombreServicio { get; set; }
        public required string Descripcion { get; set; }
        public decimal PrecioBase { get; set; }
        public bool IsActive { get; set; }
    }
}