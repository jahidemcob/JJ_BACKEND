using Auth.Application.DTOs;
using Auth.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUserUseCase _loginUserUseCase;
        private readonly RegisterUserUseCase _registerUserUseCase;

        public AuthController(LoginUserUseCase loginUserUseCase, RegisterUserUseCase registerUserUseCase)
        {
            _loginUserUseCase = loginUserUseCase;
            _registerUserUseCase = registerUserUseCase;
        }

        [HttpPost("login")]
        [AllowAnonymous]   
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _loginUserUseCase.LoginAsync(request);

            if (result == null)
                return Unauthorized(new { message = "Usuario o Contraseña incorrectos" });

            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]   
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _registerUserUseCase.RegisterAsync(request);

            if (result == null)
                return BadRequest(new { message = "El usuario ya existe" });

            return Ok(new { message = "Usuario registrado correctamente" });
        }
    }
}