using Backend.src.app.Features.Services.application.DTOs;
using Backend.src.app.Features.Services.application.usecases;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.app.Features.Services.API
{
    [ApiController]
    [Route("api/services")]
    public class ServicesQueryController : ControllerBase
    {
        private readonly GetServiceByIdUseCase _getById;
        private readonly GetAllServicesUseCase _getAll;

        public ServicesQueryController(
            GetServiceByIdUseCase getById,
            GetAllServicesUseCase getAll)
        {
            _getById = getById;
            _getAll = getAll;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _getById.Execute(id);
            return Ok(service);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? onlyActive)
        {
            var list = await _getAll.Execute();
            if (onlyActive == true)
                list = list.Where(s => s.IsActive).ToList();
            if (onlyActive == false)
                list = list.Where(s => !s.IsActive).ToList();
            return Ok(list);
        }
    }
}