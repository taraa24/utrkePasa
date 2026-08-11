

using System.ComponentModel.DataAnnotations;
using UtrkePasa.Api.Dtos;
using UtrkePasa.Domain.Entities;
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
            ? await _raceRepository.GetByIdAsync(request.RaceId.Value)
            : await _raceRepository.GetCurrentActiveRaceAsync();

        if(race == null)
        {
            return new ValidationResult { IsFailed = true, ErrorCode = "err: race not found" };
        }else if(race.end_Of_The_Race > race.start_Of_The_Race)
        {
            return new ValidationResult { IsFailed = true, ErrorCode = "err: race finished" };
        }
        //request.RaceId = race.race_Id;
        await Task.Delay(5000);
        Console.WriteLine("validacija se obradivala 5 sek");
        return new ValidationResult { IsFailed = false, ResolvedRaceId= race.race_Id };;
        
    }
}

public class ValidationResult : Result
{
    public int? ResolvedRaceId { get; set; }
}