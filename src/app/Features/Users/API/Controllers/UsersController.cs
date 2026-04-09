using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.application.usecases;
using Backend.src.app.Features.Users.application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.app.Features.Users.API.Controllers
{
    [ApiController]
    [Route("users")]
    //[Authorize(Roles = "Administrador")]
    public class UsersController : ControllerBase
    {
        private readonly UserListUsecase _userListUsecase;
        private readonly GetUserByIdUsecase _getUserByIdUsecase;
        private readonly CreateUserUsecase _createUserUsecase;
        private readonly UpdateUserUsecase _updateUserUsecase;
        private readonly DisableUserUsecase _disableUserUsecase;

        public UsersController(
            UserListUsecase userListUsecase,
            GetUserByIdUsecase getUserByIdUsecase,
            CreateUserUsecase createUserUsecase,
            UpdateUserUsecase updateUserUsecase,
            DisableUserUsecase disableUserUsecase
        )
        {
            _userListUsecase = userListUsecase;
            _getUserByIdUsecase = getUserByIdUsecase;
            _createUserUsecase = createUserUsecase;
            _updateUserUsecase = updateUserUsecase;
            _disableUserUsecase = disableUserUsecase;
        }

        // GET /users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userListUsecase.Execute();
            return Ok(result);
        }

        // GET /users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var usuario = await _getUserByIdUsecase.Execute(id);

            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(usuario);
        }

        // POST /users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            var createdUser = await _createUserUsecase.Execute(dto);
            return Ok(createdUser);
        }

        // PUT /users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
        {
            dto.IdUsuario = id;

            var updatedUser = await _updateUserUsecase.Execute(dto);

            return Ok(updatedUser);
        }

        // PATCH /users/{id}/disable
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