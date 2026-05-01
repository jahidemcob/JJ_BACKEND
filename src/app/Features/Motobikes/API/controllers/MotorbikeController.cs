using Backend.src.app.Features.Motobikes.application.DTOs;
using Backend.src.app.Features.Motobikes.application.exceptions;
using Backend.src.app.Features.Motobikes.application.usecases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Backend.src.app.Features.Motobikes.API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorbikeController : ControllerBase
    {
        private readonly CreateMotorbikeUsecase _createMotorbike;
        private readonly GetAllMotorbikesUsecase _getAllMotorbikes;
        private readonly GetByIdMotorbikeUsecase _getByIdMotorbike;
        private readonly UpdateMotorbikeUsecase _updateMotorbike;
        private readonly UpdateStatusMotorbikeUsecase _updateStatusMotorbike;

        public MotorbikeController(
            CreateMotorbikeUsecase createMotorbike,
            GetAllMotorbikesUsecase getAllMotorbikes,
            GetByIdMotorbikeUsecase getByIdMotorbike,
            UpdateMotorbikeUsecase updateMotorbike,
            UpdateStatusMotorbikeUsecase updateStatusMotorbike
        )
        {
            _createMotorbike = createMotorbike;
            _getAllMotorbikes = getAllMotorbikes;
            _getByIdMotorbike = getByIdMotorbike;
            _updateMotorbike = updateMotorbike;
            _updateStatusMotorbike = updateStatusMotorbike;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _getAllMotorbikes.Execute();
            return Ok(list);
        }

        // GET BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var moto = await _getByIdMotorbike.Execute(id);
            return Ok(moto);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MotorbikeCreateDto dto)
        {
            var moto = await _createMotorbike.Execute(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = moto.idMoto },
                moto
            );
        }

        // UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] MotorbikeUpdateDto dto)
        {
            var updated = await _updateMotorbike.Execute(id,dto);
            return Ok(updated);
        }


        //Activar/Desactivar MOTO
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