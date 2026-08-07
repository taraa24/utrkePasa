using UtrkePasa.Api.Dtos;

namespace UtrkePasa.Api.Services;

public interface IValidationService
{
    Task ValidateAsync(TicketPurchaseRequest request);
}