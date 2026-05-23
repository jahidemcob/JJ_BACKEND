using Backend.src.app.Features.Motobikes.application.usecases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.src.app.Features.Motobikes.API.controllers
{
    [ApiController]
    [Route("api/motorbike")]
    [Authorize]
    public class MotorbikeQueryController : ControllerBase
    {
        private readonly GetAllMotorbikesUsecase _getAllMotorbikes;
        private readonly GetByIdMotorbikeUsecase _getByIdMotorbike;

        public MotorbikeQueryController(
            GetAllMotorbikesUsecase getAllMotorbikes,
            GetByIdMotorbikeUsecase getByIdMotorbike)
        {
            _getAllMotorbikes = getAllMotorbikes;
            _getByIdMotorbike = getByIdMotorbike;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userIdValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized("Usuario no válido");

            var list = await _getAllMotorbikes.Execute(userId);
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var moto = await _getByIdMotorbike.Execute(id);
            return Ok(moto);
        }
    }
}