using Microsoft.AspNetCore.SignalR.Client;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.ConsoleApp;

public class RaceSubscriber
{
    private HubConnection _connection;
    private readonly CurrRaceState _currRaceState;

    public RaceSubscriber(CurrRaceState currRaceState)
    {
        _currRaceState = currRaceState;

        _connection = new HubConnectionBuilder().WithUrl("http://localhost:5057/raceHubApi").WithAutomaticReconnect().Build();
        _connection.On<Race>("OpenForGambling", race =>
        {
            _currRaceState.SetRace(race);
            Console.WriteLine("kladenje je krenilo mozete se kladiti sve dok ne krene utrka");
            Console.WriteLine("Upisi broj tiketa koji zelis uplatit");
        } );
        _connection.On<Race>("raceStarted", race =>
        {
            _currRaceState.SetRace(race);
            Console.WriteLine("startt utrka je krenila nema vise kladenja");
            
        });
        _connection.On<Race>("raceFinished", race =>
        {
            _currRaceState.SetRace(race);
            Console.WriteLine("finishh utrka je zavrsila");
        }
        );
    }

    public async Task StartAsync()
    {
        await _connection.StartAsync();
    }

    public async Task StopAsync()
    {
        await _connection.DisposeAsync();
    }


}