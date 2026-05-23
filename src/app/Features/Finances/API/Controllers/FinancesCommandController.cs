using Backend.src.app.Features.Finances.Application.DTOs;
using Backend.src.app.Features.Finances.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.app.Features.Finances.API.Controllers
{
    [ApiController]
    [Route("api/finances")]
    [Authorize(Roles = "Administrador")]
    public class FinancesCommandController : ControllerBase
    {
        private readonly CreateMovementUseCase _createMovement;

        public FinancesCommandController(CreateMovementUseCase createMovement)
        {
            _createMovement = createMovement;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovementDto dto)
        {
            var result = await _createMovement.Execute(dto);
            return Ok(result);
        }
    }
}