using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api.Services;

public class RaceEventHandler
{
    private readonly CurrRaceState _currRaceState;
    private readonly RcaePublisherApi _racePublisherApi;

    public RaceEventHandler(RcaePublisherApi racePublisherApi, CurrRaceState currRaceState)
    {
        _racePublisherApi = racePublisherApi;
        _currRaceState = currRaceState;
    }

    public async Task HandleAsync(Race race)
    {
        _currRaceState.SetCurrRace(race);

        if(race.RaceStatus == "Open")
        {
            await HandleOpenBettingAsync();
        }
        else if(race.RaceStatus == "InProgress")
        {
            await HandleStartingRace();
        }else if(race.EndOfTheRace <= DateTimeOffset.UtcNow)
        {
            await HandleFinishRace();
        }
    }

    private async Task HandleFinishRace()
    {
        await _racePublisherApi.RaceFinished();
    }

    private async Task HandleStartingRace()
    {
        await _racePublisherApi.RaceStarted();
    }

    private async Task HandleOpenBettingAsync()
    {
        await _racePublisherApi.RaceOpenFoGambling();
    }
}