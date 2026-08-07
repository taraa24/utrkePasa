using UtrkePasa.Api.Dtos;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api.Services;

public class FiscalizeTicketService : IFiscalizeTicketService
{
    public async Task FiscalizationAsync(TicketPurchaseRequest request)
    {
        await Task.Delay(5000);
        Console.WriteLine("fiskalizcija se obradivala 5sek");
    }
}