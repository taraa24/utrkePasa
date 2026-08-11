using UtrkePasa.Api.Dtos;
using UtrkePasa.Api.Services;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Domain.Repository;

namespace UtrkePasa.Api.Handler;

public class PayingHandler : IPayingHandler
{

    private readonly IValidationService _validationService;
    private readonly IFiscalizeTicketService _fiscalizeTicketService;
    private readonly ITicketRepository _ticketRepository;

    public PayingHandler(IValidationService validationService, IFiscalizeTicketService fiscalizeTicketService, ITicketRepository ticketRepository)
    {
        _validationService = validationService;
        _fiscalizeTicketService = fiscalizeTicketService;
        _ticketRepository = ticketRepository;
    }
   

    public async Task<TicketPurchaseResponse> HandlePaymentAsync(TicketPurchaseRequest request)
    {
        await _validationService.ValidateAsync(request);
        await _fiscalizeTicketService.FiscalizationAsync(request);

        var ticket = new Ticket
        {
            user_Id = request.UserId,
            race_Id = request.RaceId,
            race_Odds_Id = request.RaceOddsId,
            paid_For_Ticket = request.PaidForTicket
        };

        await _ticketRepository.SaveTicketToDbAsync(ticket);
        Console.WriteLine("spremljeno u bazu");

        return new TicketPurchaseResponse
        {
            UserId = ticket.user_Id,
            TicketId = ticket.ticket_Id,
            RaceId = ticket.race_Id,
            PlacedAt = DateTime.UtcNow,
            PaidForTicket = ticket.paid_For_Ticket
        };
    }
}