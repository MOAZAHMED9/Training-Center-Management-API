using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Training_Center_Management_API.Middleware
{
    public class ExceptionHandlingMiddleware              /////////////////////////
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)        //بيناديها مع كل HTTP request.
        {
            try
            {
                await _next(context);
            }


            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred");

                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "A database error occurred.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex,"An unexpected error occurred");

                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = (int)statusCode,
                message = message
            });
        }

    }
}
