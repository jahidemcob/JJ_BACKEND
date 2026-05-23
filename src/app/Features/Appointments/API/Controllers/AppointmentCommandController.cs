using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Application.Usecases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.src.app.Features.Appointments.API.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    [Authorize]
    public class AppointmentCommandController : ControllerBase
    {
        private readonly CreateAppointmentUseCase _createAppointment;
        private readonly UpdateAppointmentStateUseCase _updateAppointmentState;
        private readonly AssignAppointmentToEmployeeUseCase _assignAppointmentToEmployee;

        public AppointmentCommandController(
            CreateAppointmentUseCase createAppointment,
            UpdateAppointmentStateUseCase updateAppointmentState,
            AssignAppointmentToEmployeeUseCase assignAppointmentToEmployee)
        {
            _createAppointment = createAppointment;
            _updateAppointmentState = updateAppointmentState;
            _assignAppointmentToEmployee = assignAppointmentToEmployee;
        }

        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized("Usuario no válido");

            dto.IdUsuario = userId;
            var result = await _createAppointment.Execute(dto);
            return Ok(result);
        }

        [HttpPatch("{id:int}/state")]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> UpdateState(int id, [FromBody] UpdateAppointmentStateDto dto)
        {
            var result = await _updateAppointmentState.Execute(id, dto);
            return Ok(result);
        }

        [HttpPatch("{id:int}/assign")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AssignEmployee(int id, [FromBody] AssignEmployeeDto dto)
        {
            var result = await _assignAppointmentToEmployee.Execute(id, dto);
            return Ok(result);
        }
    }
}