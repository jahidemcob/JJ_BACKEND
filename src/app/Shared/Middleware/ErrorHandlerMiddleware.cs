using Backend.src.app.Features.Appointments.Application.Exceptions;
using Backend.src.app.auth.application.Exceptions;
using Backend.src.app.Features.Services.application.exceptions;
using Backend.src.app.Features.Users.application.Exceptions;
using Backend.src.app.Features.Motobikes.application.exceptions;
using Backend.src.app.Integrations.ExternalAPIs.Replacements.Application.Exceptions;
using Backend.src.app.Features.Finances.Application.Exceptions;
using Backend.src.app.Shared.exceptions;
using System.Net;
using System.Text.Json;

namespace Backend.src.app.Shared.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = ex switch
        {
            //CONFIGURATION
             ConfigurationException => (StatusCodes.Status500InternalServerError, ex.Message),

            // AUTH
            UserInactiveException => (StatusCodes.Status403Forbidden, ex.Message),
            InvalidCredentialsException => (StatusCodes.Status401Unauthorized, ex.Message),
            UserWithNoRolException => (StatusCodes.Status403Forbidden, ex.Message),
            UserOrEmailAlreadyUsedException => (StatusCodes.Status409Conflict, ex.Message),

            // USERS
            EmailUsedException => (StatusCodes.Status409Conflict, ex.Message),
            UserAlreadyUsedException => (StatusCodes.Status409Conflict, ex.Message),
            UserNotFoundException => (StatusCodes.Status404NotFound, ex.Message),
            RolNotExistException => (StatusCodes.Status400BadRequest, ex.Message),

            // SERVICES
            ServiceValidationException => (StatusCodes.Status400BadRequest, ex.Message),
            ServiceAlreadyExistsException => (StatusCodes.Status409Conflict, ex.Message),
            ServiceNotFoundException => (StatusCodes.Status404NotFound, ex.Message),
            ServiceNotUpdatedException => (StatusCodes.Status400BadRequest, ex.Message),

            // MOTORBIKES
            MotorbikeValidationException => (StatusCodes.Status400BadRequest, ex.Message),
            MotorbikeNotFoundException => (StatusCodes.Status404NotFound, ex.Message),

            // REPLACEMENTS
             ExternalServiceException => (StatusCodes.Status503ServiceUnavailable, ex.Message),
             ReplacementNotFoundException => (StatusCodes.Status404NotFound, ex.Message),

            // APPOINTMENTS
             AppointmentValidationException => (StatusCodes.Status400BadRequest, ex.Message),
             AppointmentNotFoundException => (StatusCodes.Status404NotFound, ex.Message),
             InvalidAppointmentStateTransitionException => (StatusCodes.Status409Conflict, ex.Message),
             InvalidEmployeeAssignmentException => (StatusCodes.Status400BadRequest, ex.Message),

            // FINANCES
            MovementNotFoundException => (StatusCodes.Status404NotFound, ex.Message),
            MovementValidationException => (StatusCodes.Status400BadRequest, ex.Message),

            // DEFAULT
            _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor")
        };

        context.Response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(new
        {
            status = statusCode,
            error = ex.GetType().Name,
            message,
            timestamp = DateTime.UtcNow
        });

        await context.Response.WriteAsync(result);
    }
}