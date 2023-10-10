using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace GS_Sample.Middleware
{
    public class GlobalExceptionHandling : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandling> _logger;

        public GlobalExceptionHandling(ILogger<GlobalExceptionHandling> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 
                    (int)HttpStatusCode.InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Internal Server Error",
                    Title = "Exception Occurred",
                    Detail = "Some exception occurred"
                };

                string json = JsonSerializer.Serialize(problem);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            
        }
    }
}
