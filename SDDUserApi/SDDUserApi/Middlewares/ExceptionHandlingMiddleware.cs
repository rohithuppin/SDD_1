using Newtonsoft.Json;
using System.Net;

namespace SDDUserApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);  // Proceed to the next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                // Handle the exception and return a proper response
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                Message = "An unexpected error occurred. Please try again later.",
                Detail = exception.Message,
            };

            var jsonResponse = JsonConvert.SerializeObject(errorResponse);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
