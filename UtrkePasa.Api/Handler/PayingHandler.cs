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
    private readonly CurrRaceState _currRaceState;

    public PayingHandler(IValidationService validationService, IFiscalizeTicketService fiscalizeTicketService, ITicketRepository ticketRepository, CurrRaceState currRaceState)
    {
        _validationService = validationService;
        _fiscalizeTicketService = fiscalizeTicketService;
        _ticketRepository = ticketRepository;
        _currRaceState = currRaceState;
    }
   

    public async Task<TicketPurchaseResponse> HandlePaymentAsync(TicketPurchaseRequest request)
    {

        var validationResult = await _validationService.ValidateAsync(request);
        if(validationResult.IsFailed)

            return new TicketPurchaseResponse
            {
                IsFailed =true,
                ErrorCode = validationResult.ErrorCode
            };


        var race = _currRaceState.GetCurrRace();
        var raceOdds = await _ticketRepository.GetRaceOddsAsync(race.RaceId, request.oddType);

        
        var fiscalizationResult = await _fiscalizeTicketService.FiscalizationAsync(request);
        if (fiscalizationResult.IsFailed)
            return new TicketPurchaseResponse
            {
                IsFailed = true,
                ErrorCode = fiscalizationResult.ErrorCode
            };
        

        var ticket = new Ticket
        {
            UserId = request.UserId,
            RaceId = race.RaceId,
            PlacedAt = DateTimeOffset.UtcNow,
            PaidForTicket = request.PaidForTicket,
            ExpectedResult = request.ExpectedResult,
            oddType = request.oddType,
            RaceOddsId = raceOdds.RaceOddsId

        };

        await _ticketRepository.SaveTicketToDbAsync(ticket);
        Console.WriteLine("spremljeno u bazu");

        return new TicketPurchaseResponse
        {
            UserId = ticket.UserId,
            TicketId = ticket.TicketId,
            RaceId = ticket.RaceId,
            PlacedAt = DateTimeOffset.UtcNow,
            PaidForTicket = ticket.PaidForTicket,
            ExpectedResult = ticket.ExpectedResult,
            oddType = ticket.oddType
        };
    }
}

