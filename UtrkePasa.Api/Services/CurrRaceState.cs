using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api.Services;

public class CurrRaceState
{
    
    private Race? _currRace;

    public void SetCurrRace(Race race)
    {
        _currRace = race;
    }
    public Race? GetCurrRace()
    {
        return _currRace;
    }

    
}