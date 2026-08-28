
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Npgsql.Internal;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Infrastructure;

namespace UtrkePasa.Api.Services;

public class RaceSubscriber : IHostedService
{
    private readonly MyHubConnection _hubConnection;
    private readonly RaceEventHandler _raceEventHandler;

    public RaceSubscriber(
        MyHubConnection hubConnection, 
        RaceEventHandler raceEventHandler)
    {
        _hubConnection = hubConnection;
        _raceEventHandler = raceEventHandler;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _hubConnection?.Connection?.On<Race>("RaceUpdated", race => _raceEventHandler.HandleAsync(race));

        return Task.CompletedTask;
    }

    

    public  Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
