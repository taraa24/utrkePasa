using UtrkePasa.Api.Dtos;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api.Handler;

public interface IPayingHandler
{
    Task<TicketPurchaseResponse> HandlePaymentAsync(TicketPurchaseRequest request);
}