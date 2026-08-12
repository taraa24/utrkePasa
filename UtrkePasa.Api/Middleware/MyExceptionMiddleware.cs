using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace UtrkePasa.Api.Middleware; //middleware se prebacujeu infrastrukturu kada budem imala vise od jednog api-a

public class MyExceptionMiddleware
{
    private readonly ILogger<MyExceptionMiddleware> _logger;
    public readonly RequestDelegate _next;

    public MyExceptionMiddleware(ILogger<MyExceptionMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, ex.Message);
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {   
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }


    private static Task HandleValidationExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var response = new { message = "An unexpected error occurred.", details = exception.Message };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new { message = "An unexpected error occurred.", details = exception.Message };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}