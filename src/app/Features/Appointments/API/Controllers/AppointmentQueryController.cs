using Backend.src.app.Features.Appointments.Application.Usecases;
using Backend.src.app.Features.Appointments.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.src.app.Features.Appointments.API.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    [Authorize]
    public class AppointmentQueryController : ControllerBase
    {
        private readonly GetAllAppointmentsUseCase _getAllAppointments;
        private readonly GetAppointmentByIdUseCase _getAppointmentById;
        private readonly GetAppointmentsByStateUseCase _getAppointmentsByState;
        private readonly GetAppointmentsByUserIdUseCase _getAppointmentsByUserId;
        private readonly GetAppointmentsByEmployeeIdUseCase _getAppointmentsByEmployeeId;

        public AppointmentQueryController(
            GetAllAppointmentsUseCase getAllAppointments,
            GetAppointmentByIdUseCase getAppointmentById,
            GetAppointmentsByStateUseCase getAppointmentsByState,
            GetAppointmentsByUserIdUseCase getAppointmentsByUserId,
            GetAppointmentsByEmployeeIdUseCase getAppointmentsByEmployeeId)
        {
            _getAllAppointments = getAllAppointments;
            _getAppointmentById = getAppointmentById;
            _getAppointmentsByState = getAppointmentsByState;
            _getAppointmentsByUserId = getAppointmentsByUserId;
            _getAppointmentsByEmployeeId = getAppointmentsByEmployeeId;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _getAllAppointments.Execute();
            return Ok(list);
        }

        [HttpGet("state/{state}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetByState(AppointmentState state)
        {
            var list = await _getAppointmentsByState.Execute(state);
            return Ok(list);
        }

        [HttpGet("employee")]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> GetByEmployee()
        {
            var employeeIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(employeeIdValue, out var employeeId))
                return Unauthorized("Usuario no válido");

            var list = await _getAppointmentsByEmployeeId.Execute(employeeId);
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getAppointmentById.Execute(id);
            return Ok(result);
        }

        [HttpGet("user")]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<IActionResult> GetByUser()
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized("Usuario no válido");

            var list = await _getAppointmentsByUserId.Execute(userId);
            return Ok(list);
        }
    }
}