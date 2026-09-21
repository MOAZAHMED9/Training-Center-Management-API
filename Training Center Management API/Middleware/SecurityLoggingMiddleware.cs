using System.Security.Claims;

namespace Training_Center_Management_API.Middleware
{
    public class SecurityLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SecurityLoggingMiddleware> _logger;

        public SecurityLoggingMiddleware(RequestDelegate next, ILogger<SecurityLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            var statusCode = context.Response.StatusCode;


            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
                var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                var path = context.Request.Path.ToString();


                _logger.LogWarning(
                    "Forbidden access. UserId={UserId}, Path={Path}, IP={IP}",
                    userId,
                    path,
                    ip
                );
            }

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
                var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                var path = context.Request.Path.ToString();


                _logger.LogWarning(
                    "Unauthorized access. UserId={UserId}, Path={Path}, IP={IP}",
                    userId,
                    path,
                    ip
                );
            }
        }
    }
}