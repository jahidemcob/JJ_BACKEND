using Backend.src.app.Features.Appointments.Domain.Enums;

namespace Backend.src.app.Features.Appointments.Application.DTOs
{
    public class AppointmentSummaryDto
    {
        public int IdPedido { get; set; }
        public int IdMoto { get; set; }
        public string NombreCliente { get; set; } = string.Empty;    
        public string? NombreEmpleado { get; set; }                  
        public DateOnly FechaCita { get; set; }
        public TimeOnly HoraCita { get; set; }
        public decimal Total { get; set; }
        public AppointmentState EstadoCita { get; set; }
    }
}
