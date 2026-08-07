
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using UtrkePasa.Api.Services;

namespace UtrkePasa.Api.Middleware; //middleware se prebacujeu infrastrukturu kada budem imala vise od jednog api-a

public class MyExceptionMiddleware
{

    private readonly ITicketService _ticketService;


    public readonly RequestDelegate _next;

    public MyExceptionMiddleware(RequestDelegate next)
    {
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
            await HandleExceptionAsync(context, ex);

        }/* catch (FiscalizationException ex)
        {
            await HandleExceptionAsync(context,ex);

        }catch(SaveToDbException ex)
        {
            await HandleExceptionAsync(context,ex);
        } */
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
       {
           context.Response.ContentType = "application/json";
           context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

           var response = new { message = "An unexpected error occurred.", details = exception.Message };
           return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
       }
}