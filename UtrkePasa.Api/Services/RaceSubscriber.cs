
using Microsoft.AspNetCore.SignalR.Client;
using Npgsql.Internal;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Infrastructure;

namespace UtrkePasa.Api.Services;

public class RaceSubscriber : IHostedService
{
    private  HubConnection? _connection;
    private readonly ServiceDiscovery _serviceDiscovery;
    private readonly RaceEventHandler _raceEventHandler;

    public RaceSubscriber(ServiceDiscovery serviceDiscovery, RaceEventHandler raceEventHandler)
    {
        _serviceDiscovery = serviceDiscovery;
        _raceEventHandler = raceEventHandler;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await ConnectingOnLeaderAsync(cancellationToken);
    }

    private async Task ConnectingOnLeaderAsync(CancellationToken cancellationToken)
    {
        var leader = await _serviceDiscovery.GetLeaderAsync();

        if(leader == null)
            throw new InvalidOperationException("nema leadera");

        var url = $"http://localhost:{leader.port}/racehub";

        _connection = new HubConnectionBuilder().WithUrl(url).Build();
        _connection.On<Race>("RaceUpdated",  race => _raceEventHandler.HandleAsync(race));

        _connection.Closed += OnConnectionClosed;

        await _connection.StartAsync(cancellationToken);
        
    }

    private async Task OnConnectionClosed(Exception? exception)
    {
        Console.WriteLine("konekcija sa signalR prekinuta");

        if(exception != null)
            Console.WriteLine($"greska   {exception.Message}");
        
        await ReconnectAsync();
    }

    private async Task ReconnectAsync()
    {
        while (true)
        {
            try
            {
                await Task.Delay(5000);
                var leader = await _serviceDiscovery.GetLeaderAsync();

                if(leader == null)
                {
                    await Task.Delay(5000);
                    continue;
                }

                var url = $"http://localhost:{leader.port}/racehub";

                await _connection!.DisposeAsync();

                _connection = new HubConnectionBuilder().WithUrl(url).Build();
                _connection.On<Race>("RaceUpdated",  race => _raceEventHandler.HandleAsync(race));

                _connection.Closed += OnConnectionClosed;

                await _connection.StartAsync();
                return;
                    
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                await Task.Delay(5000);
            }
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if(_connection != null)
            await _connection.DisposeAsync();
    }
}