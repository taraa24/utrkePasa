using Microsoft.AspNetCore.SignalR;
using UtrkePasa.Api.Services;
using UtrkePasa.Domain.Entities;


namespace UtrkePasa.Api;

public class RaceHub : Hub
{
    private readonly RaceStateStore _raceStateStore;
    public RaceHub(RaceStateStore raceStateStore)
    {
        _raceStateStore = raceStateStore;
    }

    public async Task UpdateRace(Race race)
    {
        _raceStateStore.UpdateRace(race);
    }
}