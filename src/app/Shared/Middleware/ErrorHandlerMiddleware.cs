using Backend.src.app.auth.application.Exceptions;
using Backend.src.app.Features.Services.application.exceptions;
using Backend.src.app.Features.Users.application.Exceptions;
using Backend.src.app.Features.Motobikes.application.exceptions;
using System.Net;
using System.Text.Json;

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

            var (statusCode, message) = ex switch
            {
                // Auth
                UserInactiveException => (StatusCodes.Status403Forbidden, ex.Message),
                InvalidCredentialsException => (StatusCodes.Status401Unauthorized, ex.Message),
                UserWithNoRolException => (StatusCodes.Status403Forbidden, ex.Message),

                UserOrEmailAlreadyUsedException => (StatusCodes.Status409Conflict, ex.Message),

                // Users
                EmailUsedException => (StatusCodes.Status409Conflict, ex.Message),
                UserAlreadyUsedException => (StatusCodes.Status409Conflict, ex.Message),

                UserNotFoundException => (StatusCodes.Status404NotFound, ex.Message),
                RolNotExistException => (StatusCodes.Status400BadRequest, ex.Message),

                // Services
                ServiceValidationException => (StatusCodes.Status400BadRequest, ex.Message),
                ServiceAlreadyExistsException => (StatusCodes.Status409Conflict, ex.Message),
                ServiceNotFoundException => (StatusCodes.Status404NotFound, ex.Message),

                //Motorbike
                 MotorbikeValidationException => (StatusCodes.Status400BadRequest, ex.Message),
                 MotorbikeNotFoundException => (StatusCodes.Status404NotFound, ex.Message),


                // Default
                _ => (StatusCodes.Status500InternalServerError, ex.ToString())
            };

            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                status = statusCode,
                error = ex.GetType().Name,
                message = message,
                timestamp = DateTime.UtcNow
            });

            await context.Response.WriteAsync(result);
        }
    }
}