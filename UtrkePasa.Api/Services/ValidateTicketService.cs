

using UtrkePasa.Api.Dtos;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Domain.Repository;

namespace UtrkePasa.Api.Services;

public class ValidateTicketService : IValidationService
{

    private readonly IRaceRepository _raceRepository;
    private readonly IUserRepository _userRepository;

    public ValidateTicketService(IRaceRepository raceRepository, IUserRepository userRepository)
    {
        _raceRepository = raceRepository;
        _userRepository = userRepository;
    }
    public async Task ValidateAsync(TicketPurchaseRequest request)
    {
        var race = await _raceRepository.GetByIdAsync(request.RaceId);
        if(request.RaceId == race.race_Id && race.end_Of_The_Race > race.start_Of_The_Race)
        {
            Console.WriteLine("ne mozete se kladiti na ovu utrku");
        }
        else
        {
            await Task.Delay(5000);
            Console.WriteLine("validacija se obradivala 5 sek");
        }
        
    }
}