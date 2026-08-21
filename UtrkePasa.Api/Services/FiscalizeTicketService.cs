using UtrkePasa.Api.Dtos;

namespace UtrkePasa.Api.Services;

public class FiscalizeTicketService : IFiscalizeTicketService
{
    public async Task<Result> FiscalizationAsync(TicketPurchaseRequest request)
    {
        //await Task.Delay(1000);
        Console.WriteLine("fiskalizcija se obradivala 5sek");
        return Result.Success();
    }
}