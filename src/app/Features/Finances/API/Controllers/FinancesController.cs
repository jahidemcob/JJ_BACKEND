// API/Controllers/FinancesController.cs
using Backend.src.app.Features.Finances.Application.DTOs;
using Backend.src.app.Features.Finances.Application.UseCases;
using Backend.src.app.Features.Finances.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.app.Features.Finances.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class FinancesController : ControllerBase
    {
        private readonly CreateMovementUseCase _createMovement;
        private readonly GetAllMovementsUseCase _getAllMovements;
        private readonly GetMovementByIdUseCase _getMovementById;
        private readonly GetMovementsByTypeUseCase _getMovementsByType;

        public FinancesController(
            CreateMovementUseCase createMovement,
            GetAllMovementsUseCase getAllMovements,
            GetMovementByIdUseCase getMovementById,
            GetMovementsByTypeUseCase getMovementsByType)
        {
            _createMovement = createMovement;
            _getAllMovements = getAllMovements;
            _getMovementById = getMovementById;
            _getMovementsByType = getMovementsByType;
        }

        // GET ALL + TOTAL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllMovements.Execute();
            return Ok(result);
        }

        // GET BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getMovementById.Execute(id);
            return Ok(result);
        }

        // GET BY TYPE
        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType(MovementType type)
        {
            var result = await _getMovementsByType.Execute(type);
            return Ok(result);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovementDto dto)
        {
            var result = await _createMovement.Execute(dto);
            return Ok(result);
        }
    }
}