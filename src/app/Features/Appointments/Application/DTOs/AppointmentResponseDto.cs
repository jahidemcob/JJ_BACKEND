using Backend.src.app.Features.Appointments.Domain.Enums;

namespace Backend.src.app.Features.Appointments.Application.DTOs
{

    public class AppointmentResponseDto
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public string NombreCliente { get; set; } = string.Empty;    
        public int? IdEmpleado { get; set; }
        public string? NombreEmpleado { get; set; }                  
        public int IdMoto { get; set; }
        public DateTime FechaCreacionCita { get; set; }
        public DateOnly FechaCita { get; set; }
        public TimeOnly HoraCita { get; set; }
        public decimal Total { get; set; }
        public AppointmentState EstadoCita { get; set; }
        public List<AppointmentDetailResponseDto> Detalles { get; set; } = new();
    }

}
