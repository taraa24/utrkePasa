using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class RaceSimulator
{
    public const int TrackLength = 100;
    private const int LineupSize = 5;
    private readonly Random _random = new();

    public List<DogRaceState> CreateStartingLineup(List<Dog> allDogs)
    {
        var selectedDogs = allDogs.OrderBy(dog => _random.Next()).Take(LineupSize).ToList();

        return selectedDogs.Select(dog => new DogRaceState
        {
            Dog = dog
        }).ToList();
    }

    public List<string> SetStartPlace(List<DogRaceState> raceDogs)
    {
        var result = new List<string>();

        for(int i = 0; i < raceDogs.Count; i++)
        {
            raceDogs[i].Place = i + 1;
            result.Add($"{raceDogs[i].Dog?.dog_Name}:{raceDogs[i].Place}");
        }
        return result;
    }

    public DogRaceState? Racing(List<DogRaceState> states, Race race)
    {
        foreach (var state in states)
        {
            state.Position += _random.Next(1,6);
        }

        DogRaceState? winner = null;

        foreach (var state in states)
        {
            if (DateTimeOffset.UtcNow > race.EndOfTheRace)
            {
                winner = states.OrderByDescending(s=>s.Position).FirstOrDefault();
            }
        }

        return winner;
    }

    public List<DogRaceState> GetFinalStandings(List<DogRaceState> states)
    {
        var standings = states.OrderByDescending(s=> s.Position).ToList();

        for (int i = 0; i < standings.Count; i++)
        {
            standings[i].Place = i + 1;
        }

        return standings;
    }

    public Race OpenBetting(Race race)
    {
        race.StartOfBetting = DateTimeOffset.UtcNow;
        race.StartOfTheRace = DateTimeOffset.UtcNow.AddSeconds(_random.Next(20, 40));
        race.EndOfTheRace = DateTimeOffset.UtcNow.AddMinutes(1.0);
        race.RaceStatus = "Open";


        

        return race;
        
    }
}