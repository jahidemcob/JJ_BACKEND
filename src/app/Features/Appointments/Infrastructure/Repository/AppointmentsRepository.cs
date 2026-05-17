using Backend.src.app.Features.Appointments.Domain.Entities;
using Backend.src.app.Features.Appointments.Domain.Enums;
using Backend.src.app.Features.Appointments.Domain.Interfaces;
using Backend.src.app.Features.Appointments.Infrastructure.Context;
using Backend.src.app.Features.Users.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.app.Features.Appointments.Infrastructure.Repository
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly AppointmentsDbContext _context;
        private readonly UsersDbContext _usersContext;

        public AppointmentsRepository(
            AppointmentsDbContext context,
            UsersDbContext usersContext)
        {
            _context = context;
            _usersContext = usersContext;
        }

        // ─── Helpers ───

        private async Task<string> GetUserNameAsync(int idUsuario)
        {
            var user = await _usersContext.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
            return user?.Nombre ?? "Usuario no encontrado";
        }

        private async Task<string?> GetEmployeeNameAsync(int? idEmpleado)
        {
            if (idEmpleado == null) return null;
            var user = await _usersContext.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.IdUsuario == idEmpleado);
            return user?.Nombre;
        }

        private async Task<IEnumerable<(Appointment, string, string?)>> EnrichAppointmentsAsync(
            IEnumerable<Appointment> appointments)
        {
            var result = new List<(Appointment, string, string?)>();
            foreach (var a in appointments)
            {
                var nombreCliente = await GetUserNameAsync(a.IdUsuario);
                var nombreEmpleado = await GetEmployeeNameAsync(a.IdEmpleado);
                result.Add((a, nombreCliente, nombreEmpleado));
            }
            return result;
        }

        // ─── Admin ───

        public async Task<IEnumerable<(Appointment, string, string?)>> GetAllAppointmentsAsync()
        {
            var appointments = await _context.Pedidos
                .Include(a => a.Detalles)
                .AsNoTracking()
                .ToListAsync();
            return await EnrichAppointmentsAsync(appointments);
        }

        public async Task<(Appointment, string, string?)?> GetAppointmentByIdAsync(int idAppointment)
        {
            var appointment = await _context.Pedidos
                .Include(a => a.Detalles)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdPedido == idAppointment);

            if (appointment == null) return null;

            var nombreCliente = await GetUserNameAsync(appointment.IdUsuario);
            var nombreEmpleado = await GetEmployeeNameAsync(appointment.IdEmpleado);
            return (appointment, nombreCliente, nombreEmpleado);
        }

        public async Task<IEnumerable<(Appointment, string, string?)>> GetAppointmentsByStateAsync(AppointmentState state)
        {
            var appointments = await _context.Pedidos
                .Include(a => a.Detalles)
                .Where(a => a.EstadoCita == state)
                .AsNoTracking()
                .ToListAsync();
            return await EnrichAppointmentsAsync(appointments);
        }

        public async Task<(Appointment, string, string?)> UpdateAppointmentStateAsync(int idAppointment, AppointmentState newState)
        {
            var appointment = await _context.Pedidos
                .Include(a => a.Detalles)
                .FirstOrDefaultAsync(a => a.IdPedido == idAppointment);

            appointment!.EstadoCita = newState;
            await _context.SaveChangesAsync();

            var nombreCliente = await GetUserNameAsync(appointment.IdUsuario);
            var nombreEmpleado = await GetEmployeeNameAsync(appointment.IdEmpleado);
            return (appointment, nombreCliente, nombreEmpleado);
        }

        public async Task<(Appointment, string, string?)> AssignAppointmentToEmployeeAsync(int idAppointment, int idUsuario)
        {
            var appointment = await _context.Pedidos
                .Include(a => a.Detalles)
                .FirstOrDefaultAsync(a => a.IdPedido == idAppointment);

            appointment!.IdEmpleado = idUsuario;
            appointment!.EstadoCita = AppointmentState.Agendada;
            await _context.SaveChangesAsync();

            var nombreCliente = await GetUserNameAsync(appointment.IdUsuario);
            var nombreEmpleado = await GetEmployeeNameAsync(appointment.IdEmpleado);
            return (appointment, nombreCliente, nombreEmpleado);
        }

        // ─── Empleado ───

        public async Task<IEnumerable<(Appointment, string, string?)>> GetAppointmentsByEmployeeIdAsync(int idEmployee)
        {
            var appointments = await _context.Pedidos
                .Include(a => a.Detalles)
                .Where(a => a.IdEmpleado == idEmployee)
                .AsNoTracking()
                .ToListAsync();
            return await EnrichAppointmentsAsync(appointments);
        }

        // ─── Cliente ───

        public async Task<IEnumerable<(Appointment, string, string?)>> GetAppointmentsByUserIdAsync(int idUser)
        {
            var appointments = await _context.Pedidos
                .Include(a => a.Detalles)
                .Where(a => a.IdUsuario == idUser)
                .AsNoTracking()
                .ToListAsync();
            return await EnrichAppointmentsAsync(appointments);
        }

        public async Task<(Appointment, string, string?)> CreateAppointmentAsync(Appointment newAppointment)
        {
            _context.Pedidos.Add(newAppointment);
            await _context.SaveChangesAsync();

            var nombreCliente = await GetUserNameAsync(newAppointment.IdUsuario);
            return (newAppointment, nombreCliente, null); // empleado null al crear
        }
    }
}