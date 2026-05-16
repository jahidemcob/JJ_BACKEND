using Backend.src.app.Features.Appointments.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend.src.app.Features.Appointments.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public int? IdEmpleado { get; set; }
        public int IdMoto { get; set; }
        public required DateTime FechaCreacionCita { get; set; }
        public required DateOnly FechaCita { get; set; }
        public required TimeOnly HoraCita { get; set; }
        public required decimal Total { get; set; }
        public required AppointmentState EstadoCita { get; set; }
        public List<AppointmentDetails> Detalles { get; set; } = new ();
    }
}
