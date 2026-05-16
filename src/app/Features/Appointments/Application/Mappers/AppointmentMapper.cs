using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Domain.Entities;
using Backend.src.app.Features.Appointments.Domain.Enums; 

namespace Backend.src.app.Features.Appointments.Application.Mappers
{
    public static class AppointmentMapper
    {
        public static AppointmentResponseDto ToResponseDto(
            Appointment appointment,
            string nombreCliente,
            string? nombreEmpleado = null)
        {
            return new AppointmentResponseDto
            {
                IdPedido = appointment.IdPedido,
                IdUsuario = appointment.IdUsuario,
                NombreCliente = nombreCliente,
                IdEmpleado = appointment.IdEmpleado,
                NombreEmpleado = nombreEmpleado,
                IdMoto = appointment.IdMoto,
                FechaCreacionCita = appointment.FechaCreacionCita,
                FechaCita = appointment.FechaCita,
                HoraCita = appointment.HoraCita,
                Total = appointment.Total,
                EstadoCita = appointment.EstadoCita, 
                Detalles = appointment.Detalles
                                        .Select(ToDetailResponseDto)
                                        .ToList()
            };
        }

        public static AppointmentSummaryDto ToSummaryDto(Appointment appointment, 
            string nombreCliente, 
            string? nombreEmpleado = null)
        {
            return new AppointmentSummaryDto
            {
                IdPedido = appointment.IdPedido,
                IdMoto = appointment.IdMoto,
                NombreCliente = nombreCliente,
                NombreEmpleado = nombreEmpleado,
                FechaCita = appointment.FechaCita,
                HoraCita = appointment.HoraCita,
                Total = appointment.Total,
                EstadoCita = appointment.EstadoCita // ✅ enum a enum, sin cambios
            };
        }

        public static AppointmentDetailResponseDto ToDetailResponseDto(AppointmentDetails detail)
        {
            return new AppointmentDetailResponseDto
            {
                IdDetalle = detail.IdDetalle,
                IdServicio = detail.IdServicio,
                PrecioUnitario = detail.PrecioUnitario,
                SubTotal = detail.SubTotal
            };
        }

        public static Appointment ToEntity(CreateAppointmentDto dto)
        {
            return new Appointment
            {
                IdUsuario = dto.IdUsuario,
                IdMoto = dto.IdMoto,
                FechaCreacionCita = DateTime.UtcNow,
                FechaCita = dto.FechaCita,
                HoraCita = dto.HoraCita,
                EstadoCita = AppointmentState.Pendiente, // ← era "Pendiente" string
                Total = dto.Detalles.Sum(d => d.PrecioUnitario),
                Detalles = dto.Detalles
                                        .Select(ToDetailEntity)
                                        .ToList()
            };
        }

        private static AppointmentDetails ToDetailEntity(CreateAppointmentDetailDto dto)
        {
            return new AppointmentDetails
            {
                IdServicio = dto.IdServicio,
                PrecioUnitario = dto.PrecioUnitario,
                SubTotal = dto.PrecioUnitario
            };
        }
    }
}