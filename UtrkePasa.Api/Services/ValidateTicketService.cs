

using UtrkePasa.Api.Dtos;
using UtrkePasa.Domain.Repository;

namespace UtrkePasa.Api.Services;

public class ValidateTicketService : IValidationService
{
    private readonly RaceStateStore _raceStateStore;

    public ValidateTicketService(RaceStateStore raceStateStore)
    {
        _raceStateStore = raceStateStore;
    }
    /* private readonly IRaceRepository _raceRepository;

    public ValidateTicketService(IRaceRepository raceRepository)
    {
        _raceRepository = raceRepository;
    } */
    public async Task<Result> ValidateAsync(TicketPurchaseRequest request)
    {

        var race = _raceStateStore.GetActivRace();

        /* var race = request.RaceId.HasValue
            ? await _raceRepository.GetByRaceIdAsync(request.RaceId.Value)
            : await _raceRepository.GetCurrentActiveRaceAsync(); */

        
        if(race == null)
        {
            return new ValidationResult { IsFailed = true, ErrorCode = "err: race not found" };
        }else if (race.EndOfTheRace <= DateTimeOffset.UtcNow)
        {
            return new ValidationResult { IsFailed = true, ErrorCode = "err: race finished" };
        }
        else if(race.StartOfTheRace <= DateTimeOffset.Now)
        {
            return new ValidationResult { IsFailed = true, ErrorCode = "err: race began, time for gambling over" };
        }
        request.RaceId = race.RaceId;
        Console.WriteLine("validacija se obradivala");
        return new ValidationResult { IsFailed = false, ResolvedRaceId= race.RaceId };;
        
    }
}

public class ValidationResult : Result
{
    public int? ResolvedRaceId { get; set; }
}