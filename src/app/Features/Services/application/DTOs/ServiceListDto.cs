namespace Backend.src.app.Features.Services.application.DTOs
{
    public class ServiceListDto
    {
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioBase { get; set; }
        public bool IsActive { get; set; }
    }
}