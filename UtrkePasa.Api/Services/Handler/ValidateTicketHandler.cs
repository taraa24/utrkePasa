using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api.Services.Handler;

public class ValidateTicketHandler : TicketPurchaseHandler
{

    protected override bool Process(Ticket ticket)
    {
        Console.WriteLine("validacija");
        return true;
    }
}