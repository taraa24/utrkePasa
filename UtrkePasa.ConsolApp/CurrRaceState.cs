using UtrkePasa.Domain.Entities;

namespace UtrkePasa.ConsoleApp;

public class CurrRaceState
{
    private Race? _currRace;

    public void SetRace(Race race)
    {
        _currRace = race;
    }

    public Race? GetRace()
    {
        return _currRace;
    }
}