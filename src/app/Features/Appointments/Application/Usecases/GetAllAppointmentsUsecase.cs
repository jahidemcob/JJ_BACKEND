using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Application.Mappers;
using Backend.src.app.Features.Appointments.Domain.Interfaces;

namespace Backend.src.app.Features.Appointments.Application.Usecases
{
    public class GetAllAppointmentsUseCase
    {
        private readonly IAppointmentsRepository _repository;

        public GetAllAppointmentsUseCase(IAppointmentsRepository repository)
        {
            _repository = repository;
        }

        // GetAllAppointmentsUseCase.cs
        public async Task<IEnumerable<AppointmentSummaryDto>> Execute()
        {
            var results = await _repository.GetAllAppointmentsAsync();
            return results.Select(r => AppointmentMapper.ToSummaryDto(r.appointment, r.nombreCliente, r.nombreEmpleado));
        }
    }
}