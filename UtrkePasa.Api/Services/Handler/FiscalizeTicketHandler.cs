using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api.Services.Handler;

public class FiscalizeTicketHandler : TicketPurchaseHandler
{
    protected override bool Process(Ticket ticket)
    {
        Console.WriteLine("fiskalizacija");
        return true;
    }
}