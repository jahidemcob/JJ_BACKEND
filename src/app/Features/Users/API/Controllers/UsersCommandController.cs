using Backend.src.app.Features.Users.application.DTOs;
using Backend.src.app.Features.Users.application.usecases;
using Backend.src.app.Features.Users.application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.app.Features.Users.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersCommandController : ControllerBase
    {
        private readonly CreateUserUsecase _createUserUsecase;
        private readonly UpdateUserUsecase _updateUserUsecase;
        private readonly DisableUserUsecase _disableUserUsecase;

        public UsersCommandController(
            CreateUserUsecase createUserUsecase,
            UpdateUserUsecase updateUserUsecase,
            DisableUserUsecase disableUserUsecase)
        {
            _createUserUsecase = createUserUsecase;
            _updateUserUsecase = updateUserUsecase;
            _disableUserUsecase = disableUserUsecase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            var createdUser = await _createUserUsecase.Execute(dto);
            return Ok(createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
        {
            dto.IdUsuario = id;
            var updatedUser = await _updateUserUsecase.Execute(dto);
            return Ok(updatedUser);
        }

        [HttpPatch("{id}/disable")]
        public async Task<IActionResult> DisableUser(int id)
        {
            var status = await _disableUserUsecase.Execute(id);
            var message = status
                ? "Usuario activado correctamente"
                : "Usuario desactivado correctamente";
            return Ok(new { message, status });
        }
    }
}