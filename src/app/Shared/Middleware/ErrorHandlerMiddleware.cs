using System.Net;
using System.Text.Json;
using Backend.src.app.auth.application.Exceptions;
using Backend.src.app.Features.Users.application.Exceptions;

namespace Backend.src.app.Shared.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = StatusCodes.Status500InternalServerError;
            var message = "Ocurrió un error inesperado.";

            // Exceptions de Auth
            if (ex is UserInactiveException)
            {
                statusCode = StatusCodes.Status403Forbidden;
                message = ex.Message;
            }
            else if (ex is InvalidCredentialsException)
            {
                statusCode = StatusCodes.Status401Unauthorized;
                message = ex.Message;
            }
            else if (ex is UserWithNoRolException)
            {
                statusCode = StatusCodes.Status500InternalServerError;
                message = ex.Message;
            }

            // Exceptions de Users
            else if (ex is EmailUsedException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = ex.Message;
            }
            else if (ex is UserNotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
                message = ex.Message;
            }
            else if (ex is UserAlreadyUsedException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = ex.Message;
            }
            else if (ex is RolNotExistException)
            {
                statusCode = StatusCodes.Status500InternalServerError;
                message = ex.Message;
            }

            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new { error = message });
            await context.Response.WriteAsync(result);
        }
    }
}