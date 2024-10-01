using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ExceptionHandlingDemo.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Logger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = Logger.GetInstance(); // Get the logger instance
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogException(ex);

                // Set the response status code and content
                context.Response.StatusCode = 500; // Internal Server Error
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");

                // Optionally, you can return a JSON response instead
                // context.Response.ContentType = "application/json";
                // await context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
        }
    }

}
