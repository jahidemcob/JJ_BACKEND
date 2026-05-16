using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Application.Exceptions;
using Backend.src.app.Features.Appointments.Application.Mappers;
using Backend.src.app.Features.Appointments.Domain.Enums;
using Backend.src.app.Features.Appointments.Domain.Interfaces;

namespace Backend.src.app.Features.Appointments.Application.Usecases
{
    public class UpdateAppointmentStateUseCase
    {
        private readonly IAppointmentsRepository _repository;

        public UpdateAppointmentStateUseCase(IAppointmentsRepository repository)
        {
            _repository = repository;
        }
        public async Task<AppointmentResponseDto> Execute(int id, UpdateAppointmentStateDto dto)
        {
            var current = await _repository.GetAppointmentByIdAsync(id)
                ?? throw new AppointmentNotFoundException(id);

            var validTransitions = new Dictionary<AppointmentState, List<AppointmentState>>
            {
                { AppointmentState.Pendiente,  [AppointmentState.Agendada, AppointmentState.Rechazada] },
                { AppointmentState.Agendada,   [AppointmentState.EnProceso] },
                { AppointmentState.EnProceso,  [AppointmentState.Completada] },
                { AppointmentState.Completada, [] },
                { AppointmentState.Rechazada,  [] }
            };

            if (!validTransitions[current.appointment.EstadoCita].Contains(dto.NuevoEstado))
                throw new InvalidAppointmentStateTransitionException(current.appointment.EstadoCita, dto.NuevoEstado);

            var result = await _repository.UpdateAppointmentStateAsync(id, dto.NuevoEstado);
            return AppointmentMapper.ToResponseDto(result.appointment, result.nombreCliente, result.nombreEmpleado);
        }
    }
}