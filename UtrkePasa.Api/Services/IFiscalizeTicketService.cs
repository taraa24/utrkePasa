using UtrkePasa.Api.Dtos;

namespace UtrkePasa.Api.Services;

public interface IFiscalizeTicketService
{
    Task<Result> FiscalizationAsync(TicketPurchaseRequest request);
}