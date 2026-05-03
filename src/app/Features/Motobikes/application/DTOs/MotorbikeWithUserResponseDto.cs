namespace Backend.src.app.Features.Motobikes.application.DTOs
{
    public class MotorbikeWithUserResponseDto
    {
        public int idMoto { get; set; }
        public int idUsuario { get; set; }

        public required string marca { get; set; }
        public required string modelo { get; set; }
        public required string placa { get; set; }

        public int cilindraje { get; set; }
        public int anio { get; set; }
        public bool Activo { get; set; }

        public required string nombreUsuario { get; set; }
    }
}
