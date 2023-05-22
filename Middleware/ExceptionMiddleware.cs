using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        // Initialise fields
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            // Set fields
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Try to invoke the next middleware
            try
            {
                await _next(context);
            }
            // If an exception is thrown, catch it
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, ex.Message);
                // Set the response
                context.Response.ContentType = "application/json";
                // Set the status code
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Create a variable called response
                var response = _env.IsDevelopment()
                    // If the environment is development, create a new ApiException with the status code, the message and the details
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    // If the environment is not development, create a new ApiException with the status code and the message
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                // Create a variable called options
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                // Create a variable called json
                var json = JsonSerializer.Serialize(response, options);

                // Write the json to the response
                await context.Response.WriteAsync(json);
            }
        }
    }
}