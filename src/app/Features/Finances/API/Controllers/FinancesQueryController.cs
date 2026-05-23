using Backend.src.app.Features.Finances.Application.UseCases;
using Backend.src.app.Features.Finances.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.app.Features.Finances.API.Controllers
{
    [ApiController]
    [Route("api/finances")]
    [Authorize(Roles = "Administrador")]
    public class FinancesQueryController : ControllerBase
    {
        private readonly GetAllMovementsUseCase _getAllMovements;
        private readonly GetMovementByIdUseCase _getMovementById;
        private readonly GetMovementsByTypeUseCase _getMovementsByType;

        public FinancesQueryController(
            GetAllMovementsUseCase getAllMovements,
            GetMovementByIdUseCase getMovementById,
            GetMovementsByTypeUseCase getMovementsByType)
        {
            _getAllMovements = getAllMovements;
            _getMovementById = getMovementById;
            _getMovementsByType = getMovementsByType;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllMovements.Execute();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getMovementById.Execute(id);
            return Ok(result);
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType(MovementType type)
        {
            var result = await _getMovementsByType.Execute(type);
            return Ok(result);
        }
    }
}