
using Microsoft.AspNetCore.SignalR.Client;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Infrastructure;

namespace UtrkePasa.Api.Services;

public class RaceSubscriber : IHostedService
{
    private  HubConnection _connection;
    private readonly CurrRaceState _currRaceState;
    private readonly ServiceDiscovery _serviceDiscovery;
    private readonly RcaePublisherApi _racePublisherApi;

    public RaceSubscriber(CurrRaceState currRaceState, ServiceDiscovery serviceDiscovery, RcaePublisherApi racePublisherApi)
    {
        _currRaceState = currRaceState;
        _serviceDiscovery = serviceDiscovery;
        _racePublisherApi = racePublisherApi;
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

        var url = $"http://localhost:{leader.port}/racehub";

        _connection = new HubConnectionBuilder().WithUrl(url).WithAutomaticReconnect().Build();

        _connection.On<Race>("RaceUpdated", async race =>
        {
            _currRaceState.SetCurrRace(race);

            if(race.RaceStatus == "Open")
                await _racePublisherApi.RaceOpenFoGambling(race);

            if(race.RaceStatus == "InProgress")
                await _racePublisherApi.RaceStarted(race);

            if(race.EndOfTheRace <= DateTimeOffset.UtcNow)
                await _racePublisherApi.RaceFinished(race);


        });
        
        await _connection.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if(_connection != null)
            await _connection.DisposeAsync();
    }
}