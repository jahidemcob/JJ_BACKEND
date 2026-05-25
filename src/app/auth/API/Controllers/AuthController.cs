using Backend.src.app.auth.application.UseCases;
using Backend.src.app.auth.application.DTOs;
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
        private readonly GoogleLoginUseCase _googleLoginUseCase;
        private readonly CompleteProfileUseCase _completeProfileUseCase;

        public AuthController(
            LoginUserUseCase loginUserUseCase,
            RegisterUserUseCase registerUserUseCase,
            GoogleLoginUseCase googleLoginUseCase,
            CompleteProfileUseCase completeProfileUseCase)
        {
            _loginUserUseCase = loginUserUseCase;
            _registerUserUseCase = registerUserUseCase;
            _googleLoginUseCase = googleLoginUseCase;
            _completeProfileUseCase = completeProfileUseCase;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _loginUserUseCase.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            await _registerUserUseCase.RegisterAsync(request);
            return Ok(new { message = "Usuario registrado correctamente" });
        }

        [HttpPost("google")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthRequestDto request)
        {
            var result = await _googleLoginUseCase.ExecuteAsync(request.IdToken);
            return Ok(result);
        }

        [HttpPatch("complete-profile")]
        [AllowAnonymous]
        public async Task<IActionResult> CompleteProfile([FromBody] CompleteProfileRequestDto request)
        {
            await _completeProfileUseCase.ExecuteAsync(request);
            return Ok(new { message = "Perfil completado correctamente" });
        }
    }
}