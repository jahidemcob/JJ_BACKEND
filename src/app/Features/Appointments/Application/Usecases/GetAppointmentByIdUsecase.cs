using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Application.Exceptions;
using Backend.src.app.Features.Appointments.Application.Mappers;
using Backend.src.app.Features.Appointments.Domain.Interfaces;

namespace Backend.src.app.Features.Appointments.Application.Usecases
{
    public class GetAppointmentByIdUseCase
    {
        private readonly IAppointmentsRepository _repository;

        public GetAppointmentByIdUseCase(IAppointmentsRepository repository)
        {
            _repository = repository;
        }
        public async Task<AppointmentResponseDto> Execute(int id)
        {
            var result = await _repository.GetAppointmentByIdAsync(id)
                ?? throw new AppointmentNotFoundException(id);
            return AppointmentMapper.ToResponseDto(result.appointment, result.nombreCliente, result.nombreEmpleado);
        }
    }
}