using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Application.Mappers;
using Backend.src.app.Features.Appointments.Domain.Enums;
using Backend.src.app.Features.Appointments.Domain.Interfaces;

namespace Backend.src.app.Features.Appointments.Application.Usecases
{
    public class GetAppointmentsByStateUseCase
    {
        private readonly IAppointmentsRepository _repository;

        public GetAppointmentsByStateUseCase(IAppointmentsRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<AppointmentSummaryDto>> Execute(AppointmentState state)
        {
            var results = await _repository.GetAppointmentsByStateAsync(state);
            return results.Select(r => AppointmentMapper.ToSummaryDto(r.appointment, r.nombreCliente, r.nombreEmpleado));
        }
    }
}