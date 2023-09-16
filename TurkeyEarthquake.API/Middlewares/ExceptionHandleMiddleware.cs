using System.Net;
using System.Text.Json;

namespace TurkeyEarthquake.API.Middlewares;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var statusCode = exception switch
            {
                ArgumentException
                or ArgumentNullException
                => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            string message;

            if (statusCode == (int)HttpStatusCode.InternalServerError)
            {
                message = JsonSerializer.Serialize(new { error = "Server Error" });
            }
            else
            {
                message = JsonSerializer.Serialize(new { error = exception.Message });
            }
            await context.Response.WriteAsync(message);
        }
    }
}
