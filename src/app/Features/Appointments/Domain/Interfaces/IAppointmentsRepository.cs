using Backend.src.app.Features.Appointments.Domain.Entities;
using Backend.src.app.Features.Appointments.Domain.Enums;

namespace Backend.src.app.Features.Appointments.Domain.Interfaces
{
    public interface IAppointmentsRepository
    {
        // Admin
        Task<IEnumerable<(Appointment appointment, string nombreCliente, string? nombreEmpleado)>> GetAllAppointmentsAsync();
        Task<(Appointment appointment, string nombreCliente, string? nombreEmpleado)?> GetAppointmentByIdAsync(int idAppointment);
        Task<IEnumerable<(Appointment appointment, string nombreCliente, string? nombreEmpleado)>> GetAppointmentsByStateAsync(AppointmentState state);
        Task<(Appointment appointment, string nombreCliente, string? nombreEmpleado)> UpdateAppointmentStateAsync(int idAppointment, AppointmentState newState);
        Task<(Appointment appointment, string nombreCliente, string? nombreEmpleado)> AssignAppointmentToEmployeeAsync(int idAppointment, int idUsuario);

        // Empleado
        Task<IEnumerable<(Appointment appointment, string nombreCliente, string? nombreEmpleado)>> GetAppointmentsByEmployeeIdAsync(int idEmployee);

        // Cliente
        Task<IEnumerable<(Appointment appointment, string nombreCliente, string? nombreEmpleado)>> GetAppointmentsByUserIdAsync(int idUser);
        Task<(Appointment appointment, string nombreCliente, string? nombreEmpleado)> CreateAppointmentAsync(Appointment newAppointment);
    }
}