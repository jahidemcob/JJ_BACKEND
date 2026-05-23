using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.application.usecases;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.app.Features.Services.API
{
    [ApiController]
    [Route("api/services")]
    public class ServicesCommandController : ControllerBase
    {
        private readonly CreateServiceUseCase _create;
        private readonly UpdateServiceUseCase _update;
        private readonly UpdateServiceStatusUsecase _disable;

        public ServicesCommandController(
            CreateServiceUseCase create,
            UpdateServiceUseCase update,
            UpdateServiceStatusUsecase disable)
        {
            _create = create;
            _update = update;
            _disable = disable;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceCreateDto dto)
        {
            var service = await _create.Execute(dto);
            return CreatedAtAction(
                nameof(ServicesQueryController.GetById),
                "ServicesQuery",
                new { id = service.IdServicio },
                service
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceUpdateDto dto)
        {
            dto.IdServicio = id;
            var updated = await _update.Execute(dto);
            return Ok(updated);
        }

        [HttpPatch("{id}/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            var status = await _disable.Execute(id);
            var message = status
                ? "Servicio activado correctamente"
                : "Servicio desactivado correctamente";
            return Ok(new { message, status });
        }
    }
}