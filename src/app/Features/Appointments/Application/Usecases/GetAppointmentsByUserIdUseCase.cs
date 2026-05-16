using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Application.Mappers;
using Backend.src.app.Features.Appointments.Domain.Interfaces;

namespace Backend.src.app.Features.Appointments.Application.Usecases
{
    public class GetAppointmentsByUserIdUseCase
    {
        private readonly IAppointmentsRepository _repository;

        public GetAppointmentsByUserIdUseCase(IAppointmentsRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<AppointmentSummaryDto>> Execute(int userId)
        {
            var results = await _repository.GetAppointmentsByUserIdAsync(userId);
            return results.Select(r => AppointmentMapper.ToSummaryDto(r.appointment, r.nombreCliente, r.nombreEmpleado));
        }
    }
}