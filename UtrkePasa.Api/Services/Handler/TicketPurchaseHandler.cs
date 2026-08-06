
namespace UtrkePasa.Api.Services.Handler;

public abstract class TicketPurchaseHandler : ITicketPurchaseHandler
{
    private ITicketPurchaseHandler? _next;    
    public ITicketPurchaseHandler SetNext(ITicketPurchaseHandler next)
    {
        _next = next;
        return next;
    }
}