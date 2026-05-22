using Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.DTOs;
using Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.Usecases;
using Microsoft.AspNetCore.Mvc;


namespace Backend.src.app.Integrations.ExternalAPIs.Replacements.API.Controllers
{
    [ApiController]
    [Route("api/Replacements")]
    public class ReplacementsController : ControllerBase
    {
        private readonly GetAllReplacementsUsecase _getAllReplacementsUsecase;

        public ReplacementsController(GetAllReplacementsUsecase getAllReplacementsUsecase)
        {
            _getAllReplacementsUsecase = getAllReplacementsUsecase;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllReplacementsDto>>> GetAll()
        {
            var result = await _getAllReplacementsUsecase.Execute();
            return Ok(result);
        }
    }
}
