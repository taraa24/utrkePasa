
using Microsoft.AspNetCore.SignalR.Client;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api.Services;

public class RaceSubscriber : IHostedService
{
    private readonly HubConnection _connection;
    private readonly CurrRaceState _currRaceState;

    public RaceSubscriber(CurrRaceState currRaceState)
    {
        _currRaceState = currRaceState;

        _connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/racehub").WithAutomaticReconnect().Build();

        _connection.On<Race>("RaceUpdated", race => _currRaceState.SetCurrRace(race));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _connection.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _connection.DisposeAsync();
    }
}