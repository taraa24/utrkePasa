using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api.Services;

public class RaceStateStore
{
    private Race? _activeRace;

    public Race? GetActivRace()
    {
        return _activeRace;
    }

    public void UpdateRace(Race race)
    {
        _activeRace = race;
    }
}