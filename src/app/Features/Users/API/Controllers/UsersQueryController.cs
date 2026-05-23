using Backend.src.app.Features.Users.application.usecases;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.app.Features.Users.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersQueryController : ControllerBase
    {
        private readonly UserListUsecase _userListUsecase;
        private readonly GetUserByIdUsecase _getUserByIdUsecase;

        public UsersQueryController(
            UserListUsecase userListUsecase,
            GetUserByIdUsecase getUserByIdUsecase)
        {
            _userListUsecase = userListUsecase;
            _getUserByIdUsecase = getUserByIdUsecase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userListUsecase.Execute();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var usuario = await _getUserByIdUsecase.Execute(id);
            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado" });
            return Ok(usuario);
        }
    }
}