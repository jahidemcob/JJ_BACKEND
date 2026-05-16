using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Application.Exceptions;
using Backend.src.app.Features.Appointments.Application.Mappers;
using Backend.src.app.Features.Appointments.Domain.Interfaces;
using Backend.src.app.Features.Users.domain.repositories;

namespace Backend.src.app.Features.Appointments.Application.Usecases
{
    public class AssignAppointmentToEmployeeUseCase
    {
        private readonly IAppointmentsRepository _repository;
        private readonly IUserManagementRepository _userRepository;

        public AssignAppointmentToEmployeeUseCase(
            IAppointmentsRepository repository,
            IUserManagementRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<AppointmentResponseDto> Execute(int idAppointment, AssignEmployeeDto dto)
        {
            // 1. Verificar que la cita existe
            var current = await _repository.GetAppointmentByIdAsync(idAppointment)
                ?? throw new AppointmentNotFoundException(idAppointment);

            // 2. Verificar que el usuario existe y es Empleado (IdRol = 2)
            var usuario = await _userRepository.GetByIdAsync(dto.IdUsuario)
                ?? throw new AppointmentValidationException($"El usuario con ID {dto.IdUsuario} no existe.");

            if (usuario.IdRol != 2)
                throw new InvalidEmployeeAssignmentException();

            // 3. Asignar
            var result = await _repository.AssignAppointmentToEmployeeAsync(idAppointment, dto.IdUsuario);
            return AppointmentMapper.ToResponseDto(result.appointment, result.nombreCliente, result.nombreEmpleado);
        }
    }
}