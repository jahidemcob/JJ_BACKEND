namespace Backend.src.app.Features.Motobikes.application.DTOs
{
    public class CreateMotorbikeDTO
    {
        public int IdUsuario { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string placa { get; set; }
        public int cilindraje { get; set; }
        public int anio { get; set; }
    }
}