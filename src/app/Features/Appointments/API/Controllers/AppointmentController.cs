using Backend.src.app.Features.Appointments.Application.DTOs;
using Backend.src.app.Features.Appointments.Application.Usecases;
using Backend.src.app.Features.Appointments.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.src.app.Features.Appointments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly CreateAppointmentUseCase _createAppointment;
        private readonly GetAllAppointmentsUseCase _getAllAppointments;
        private readonly GetAppointmentByIdUseCase _getAppointmentById;
        private readonly GetAppointmentsByStateUseCase _getAppointmentsByState;
        private readonly GetAppointmentsByUserIdUseCase _getAppointmentsByUserId;
        private readonly GetAppointmentsByEmployeeIdUseCase _getAppointmentsByEmployeeId;
        private readonly UpdateAppointmentStateUseCase _updateAppointmentState;
        private readonly AssignAppointmentToEmployeeUseCase _assignAppointmentToEmployee;

        public AppointmentController(
            CreateAppointmentUseCase createAppointment,
            GetAllAppointmentsUseCase getAllAppointments,
            GetAppointmentByIdUseCase getAppointmentById,
            GetAppointmentsByStateUseCase getAppointmentsByState,
            GetAppointmentsByUserIdUseCase getAppointmentsByUserId,
            GetAppointmentsByEmployeeIdUseCase getAppointmentsByEmployeeId,
            UpdateAppointmentStateUseCase updateAppointmentState,
            AssignAppointmentToEmployeeUseCase assignAppointmentToEmployee)
        {
            _createAppointment = createAppointment;
            _getAllAppointments = getAllAppointments;
            _getAppointmentById = getAppointmentById;
            _getAppointmentsByState = getAppointmentsByState;
            _getAppointmentsByUserId = getAppointmentsByUserId;
            _getAppointmentsByEmployeeId = getAppointmentsByEmployeeId;
            _updateAppointmentState = updateAppointmentState;
            _assignAppointmentToEmployee = assignAppointmentToEmployee;
        }

        // ─── Admin ───

        // GET ALL
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _getAllAppointments.Execute();
            return Ok(list);
        }

        // GET BY STATE
        [HttpGet("state/{state}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetByState(AppointmentState state)
        {
            var list = await _getAppointmentsByState.Execute(state);
            return Ok(list);
        }

        // ASSIGN EMPLOYEE
        [HttpPatch("{id:int}/assign")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AssignEmployee(int id, [FromBody] AssignEmployeeDto dto)
        {
            var result = await _assignAppointmentToEmployee.Execute(id, dto);
            return Ok(result);
        }

        // ─── Admin + Empleado ───

        // UPDATE STATE
        [HttpPatch("{id:int}/state")]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> UpdateState(int id, [FromBody] UpdateAppointmentStateDto dto)
        {
            var result = await _updateAppointmentState.Execute(id, dto);
            return Ok(result);
        }

        // GET BY EMPLOYEE
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

        // ─── Admin + Cliente ───

        // GET BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getAppointmentById.Execute(id);
            return Ok(result);
        }

        // GET BY USER
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

        // ─── Cliente ───

        // CREATE
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
    }
}