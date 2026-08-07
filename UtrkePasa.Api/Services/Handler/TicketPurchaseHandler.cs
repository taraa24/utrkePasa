
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api.Services.Handler;

public abstract class TicketPurchaseHandler : ITicketPurchaseHandler
{
    private ITicketPurchaseHandler? _next;
    
    protected abstract bool Process(Ticket ticket);

    public ITicketPurchaseHandler SetNext(ITicketPurchaseHandler next)
    {
        _next = next;
        return next;
    }

    public bool Handle(Ticket ticket)
    {

        if(Process(ticket)) return false

        return _next?.Handle(ticket) ?? true;
    }

    
}