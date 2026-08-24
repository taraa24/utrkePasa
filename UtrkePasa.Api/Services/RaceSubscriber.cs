
using Microsoft.AspNetCore.SignalR.Client;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Infrastructure;

namespace UtrkePasa.Api.Services;

public class RaceSubscriber : IHostedService
{
    private  HubConnection _connection;
    private readonly CurrRaceState _currRaceState;
    private readonly ServiceDiscovery _serviceDiscovery;

    public RaceSubscriber(CurrRaceState currRaceState, ServiceDiscovery serviceDiscovery)
    {
        _currRaceState = currRaceState;
        _serviceDiscovery = serviceDiscovery;
/* 
        _connection = new HubConnectionBuilder().WithUrl(
            "http://localhost:5000/racehub").WithAutomaticReconnect().Build();

       _connection.On<Race>("RaceUpdated", race => _currRaceState.SetCurrRace(race)); */
      /*  _connection.Closed += OnClose(); */
       
    }

/*     private  Func<Exception?, Task> OnClose()
    {
        //Ciscenje _connection ;
        _connection.DisposeAsync().GetAwaiter().GetResult();

          _connection = new HubConnectionBuilder().WithUrl(
            "http://localhost:5000/racehub").WithAutomaticReconnect().Build();

       _connection.On<Race>("RaceUpdated", race => _currRaceState.SetCurrRace(race));
       _connection.Closed += OnClose();
    } */

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        
        var leader = await _serviceDiscovery.GetLeaderAsync();

        if(leader == null)
            throw new InvalidOperationException("nema leadera");

        var url = $"http://{leader.ipAddr}:{leader.port}/racehub";

        _connection = new HubConnectionBuilder().WithUrl(url).WithAutomaticReconnect().Build();

        _connection.On<Race>("RaceUpdated", race => _currRaceState.SetCurrRace(race));
        
        await _connection.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if(_connection != null)
            await _connection.DisposeAsync();
    }
}