using Backend.src.app.Features.Motobikes.application.DTOs;
using Backend.src.app.Features.Motobikes.application.usecases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.src.app.Features.Motobikes.API.controllers
{
    [ApiController]
    [Route("api/motorbike")]
    [Authorize]
    public class MotorbikeCommandController : ControllerBase
    {
        private readonly CreateMotorbikeUsecase _createMotorbike;
        private readonly UpdateMotorbikeUsecase _updateMotorbike;
        private readonly UpdateStatusMotorbikeUsecase _updateStatusMotorbike;

        public MotorbikeCommandController(
            CreateMotorbikeUsecase createMotorbike,
            UpdateMotorbikeUsecase updateMotorbike,
            UpdateStatusMotorbikeUsecase updateStatusMotorbike)
        {
            _createMotorbike = createMotorbike;
            _updateMotorbike = updateMotorbike;
            _updateStatusMotorbike = updateStatusMotorbike;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MotorbikeCreateDto dto)
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized("Usuario no válido");

            var moto = await _createMotorbike.Execute(dto, userId);
            return Ok(moto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] MotorbikeUpdateDto dto)
        {
            var updated = await _updateMotorbike.Execute(id, dto);
            return Ok(updated);
        }

        [HttpPatch("{id}/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            var status = await _updateStatusMotorbike.Execute(id);
            var message = status
                ? "Moto activada correctamente"
                : "Moto desactivada correctamente";
            return Ok(new { message, status });
        }
    }
}