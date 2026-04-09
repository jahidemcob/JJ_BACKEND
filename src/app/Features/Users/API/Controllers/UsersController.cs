using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.application.usecases;
using Backend.src.app.Features.Users.application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Backend.src.app.Features.Users.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly UserListUsecase _userListUsecase;
        private readonly CreateUserUsecase _createUserUsecase;
        private readonly UpdateUserUsecase _updateUserUsecase;
        private readonly DisableUserUsecase _disableUserUsecase;

        public UsersController(
            UserListUsecase userListUsecase,
            CreateUserUsecase createUserUsecase,
            UpdateUserUsecase updateUserUsecase,
            DisableUserUsecase disableUserUsecase
        )
        {
            _userListUsecase = userListUsecase;
            _createUserUsecase = createUserUsecase;
            _updateUserUsecase = updateUserUsecase;
            _disableUserUsecase = disableUserUsecase;
        }

        // 🔹 GET /users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userListUsecase.Execute();
            return Ok(result);
        }

        // 🔥 GET /users/{id} (NUEVO)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var usuarios = await _userListUsecase.Execute();

            var usuario = usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(usuario);
        }

        // 🔹 POST /users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            var createdUser = await _createUserUsecase.Execute(dto);
            return Ok(createdUser);
        }

        // 🔹 PUT /users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
        {
            dto.IdUsuario = id;

            var updatedUser = await _updateUserUsecase.Execute(dto);

            return Ok(updatedUser);
        }

        // 🔹 PATCH /users/{id}/disable
        [HttpPatch("{id}/disable")]
        public async Task<IActionResult> DisableUser(int id, [FromBody] UserDisableDto dto)
        {
            dto.IdUsuario = id;

            var success = await _disableUserUsecase.Execute(dto);

            if (!success)
                return NotFound(new { message = "Usuario no encontrado" });

            return NoContent();
        }
    }
}