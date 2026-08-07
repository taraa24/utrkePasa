

using UtrkePasa.Api.Dtos;

namespace UtrkePasa.Api.Services;

public class ValidateTicketService : IValidationService
{
    public async Task ValidateAsync(TicketPurchaseRequest request)
    {
        await Task.Delay(5000);
        Console.WriteLine("validacija se obradivala 5 sek");
    }
}