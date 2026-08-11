using UtrkePasa.Api.Dtos;

namespace UtrkePasa.Api.Services;

public interface IValidationService
{
    Task<Result> ValidateAsync(TicketPurchaseRequest request);
}