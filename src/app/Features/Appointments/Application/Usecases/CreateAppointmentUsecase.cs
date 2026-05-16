using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Application.Exceptions;
using Backend.src.app.Features.Appointments.Application.Mappers;
using Backend.src.app.Features.Appointments.Domain.Interfaces;

namespace Backend.src.app.Features.Appointments.Application.Usecases
{
    public class CreateAppointmentUseCase
    {
        private readonly IAppointmentsRepository _repository;

        public CreateAppointmentUseCase(IAppointmentsRepository repository)
        {
            _repository = repository;
        }

        // CreateAppointmentUseCase.cs
        public async Task<AppointmentResponseDto> Execute(CreateAppointmentDto dto)
        {
            if (dto.Detalles == null || dto.Detalles.Count == 0)
                throw new AppointmentValidationException("La cita debe tener al menos un servicio.");

            var appointment = AppointmentMapper.ToEntity(dto);
            var result = await _repository.CreateAppointmentAsync(appointment);
            return AppointmentMapper.ToResponseDto(result.appointment, result.nombreCliente, result.nombreEmpleado);
        }
    }
}