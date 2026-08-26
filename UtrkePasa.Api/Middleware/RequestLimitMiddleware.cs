using UtrkePasa.Api.Services;

namespace UtrkePasa.Api.Middleware;

public class RequestLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SemaphoreSlim _semaphore; 

    public RequestLimitMiddleware(RequestDelegate next)
    {
        _next = next;
        _semaphore = new SemaphoreSlim(10, 10); // inicijaln i maksimalan broj requestova
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        var attribute = endpoint?.Metadata.GetMetadata<RequestLimitAttribute>();

        if(attribute == null)
        {
            await _next(context);
            return;
        }

        if(!await _semaphore.WaitAsync(0))
        {
            throw new TooManyRequestsExcetions();
        }

        try
        {
            await _next(context);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}