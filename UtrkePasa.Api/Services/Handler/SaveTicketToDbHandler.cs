using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api.Services.Handler;

public class SaveTicketToDbHandler : TicketPurchaseHandler
{
    protected override bool Process(Ticket ticket)
    {
        Console.WriteLine("spremanje u bazu");
        return true;
    }
}