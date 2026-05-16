using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Application.Mappers;
using Backend.src.app.Features.Appointments.Domain.Interfaces;

namespace Backend.src.app.Features.Appointments.Application.Usecases
{
    public class GetAppointmentsByEmployeeIdUseCase
    {
        private readonly IAppointmentsRepository _repository;

        public GetAppointmentsByEmployeeIdUseCase(IAppointmentsRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<AppointmentSummaryDto>> Execute(int employeeId)
        {
            var results = await _repository.GetAppointmentsByEmployeeIdAsync(employeeId);
            return results.Select(r => AppointmentMapper.ToSummaryDto(r.appointment, r.nombreCliente, r.nombreEmpleado));
        }
    }
}