using Inventory_api.src.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Inventory_api.WebAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred: {e.Message}");
                await HandleExceptionAsync(context, e);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode;
            string message;
            string error;

            switch (exception)
            {
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    error = "Bad Request";
                    break;
                case UnauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    error = "Unauthorized";
                    break;
                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    message = exception.Message;
                    error = "Conflict";
                    break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    error = "Not Found";
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = exception.Message;
                    error = "Internal Server Error";
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            var result = JsonSerializer.Serialize(new { success = false, message, error });
            return context.Response.WriteAsync(result);
        }
    }
}
