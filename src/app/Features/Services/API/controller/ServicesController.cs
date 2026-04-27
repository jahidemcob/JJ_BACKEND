using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.application.usecases;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.app.Features.Services.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly CreateServiceUseCase _create;
        private readonly UpdateServiceUseCase _update;
        private readonly UpdateServiceStatusUsecase _disable;
        private readonly GetServiceByIdUseCase _getById;
        private readonly GetAllServicesUseCase _getAll;

        public ServicesController(
            CreateServiceUseCase create,
            UpdateServiceUseCase update,
            UpdateServiceStatusUsecase disable,
            GetServiceByIdUseCase getById,
            GetAllServicesUseCase getAll)
        {
            _create = create;
            _update = update;
            _disable = disable;
            _getById = getById;
            _getAll = getAll;
        }


        //Crear Servicio
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceCreateDto dto)
        {
            var service = await _create.Execute(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = service.IdServicio },
                service
            );
        }

        //Actualizar Servicio
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceUpdateDto dto)
        {
            dto.IdServicio = id;

            var updated = await _update.Execute(dto);

            return Ok(updated);
        }

        //Obtener Servicio por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _getById.Execute(id);
            return Ok(service);
        }

        //Obtener todos los Servicios
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? onlyActive)
        {
            var list = await _getAll.Execute();

            if (onlyActive == true)
                list = list.Where(s => s.Activo).ToList();

            if (onlyActive == false)
                list = list.Where(s => !s.Activo).ToList();

            return Ok(list);
        }

        //Activar/Desactivar Servicio
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