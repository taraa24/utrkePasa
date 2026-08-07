namespace UtrkePasa.Api.Services.Handler;

public interface ITicketPurchaseHandler
{
    ITicketPurchaseHandler SetNext(ITicketPurchaseHandler next);
    bool Handle(Ticket ticket);
}