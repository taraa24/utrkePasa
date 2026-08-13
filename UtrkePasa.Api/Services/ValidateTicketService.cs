

using UtrkePasa.Api.Dtos;
using UtrkePasa.Domain.Repository;

namespace UtrkePasa.Api.Services;

public class ValidateTicketService : IValidationService
{

    private readonly IRaceRepository _raceRepository;

    public ValidateTicketService(IRaceRepository raceRepository)
    {
        _raceRepository = raceRepository;
    }
    public async Task<Result> ValidateAsync(TicketPurchaseRequest request)
    {
        var race = request.RaceId.HasValue
            ? await _raceRepository.GetByRaceIdAsync(request.RaceId.Value)
            : await _raceRepository.GetCurrentActiveRaceAsync();

        
        if(race == null)
        {
            return new ValidationResult { IsFailed = true, ErrorCode = "err: race not found" };
        }else if(race.EndOfTheRace > race.StartOfTheRace)
        {
            return new ValidationResult { IsFailed = true, ErrorCode = "err: race finished" };
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