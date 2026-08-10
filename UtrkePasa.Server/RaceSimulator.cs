using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class RaceSimulator
{
    public const int TrackLength = 600;
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

    public DogRaceState? Racing(List<DogRaceState> states)
    {
        foreach (var state in states)
        {
            state.Position += _random.Next(1,6);
        }

        DogRaceState? winner = null;

        foreach (var state in states)
        {
            if (state.Position >= TrackLength)
            {
                if (winner == null || state.Position > winner.Position)
                {
                    winner = state;
                }
            }
        }

        return winner;
    }
}