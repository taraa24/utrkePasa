using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace UtrkePasa.Infrastructure;

public class MyHubConnection: IHostedService
{
    public HubConnection? Connection { get;private set; }
    private readonly HubConfig _configuration;
    private readonly ServiceDiscovery _serviceDiscovery;
    public MyHubConnection(
        ServiceDiscovery serviceDiscovery,
        IOptions<HubConfig> configuration)
    {
        _serviceDiscovery = serviceDiscovery;
        _configuration = configuration.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await ConnectingOnLeaderAsync(cancellationToken);
    }

    private async Task OnConnectionClosed(Exception? exception)
    {
        Console.WriteLine("konekcija sa signalR prekinuta");

        if(exception != null)
            Console.WriteLine($"greska   {exception.Message}");
        
        await ReconnectAsync();
    }

    private async Task ConnectingOnLeaderAsync(CancellationToken cancellationToken)
    {
        var leader = await _serviceDiscovery.GetLeaderAsync(_configuration.ServerName);

        await Task.Delay(5000);
        if(leader == null)
            throw new InvalidOperationException("nema leadera");

        var url = $"http://localhost:{leader.port}{_configuration.HubPath}";

        Connection = new HubConnectionBuilder().WithUrl(url).Build();
    
        Connection.Closed += OnConnectionClosed;

        await Connection.StartAsync(cancellationToken);
        
    }

    
private async Task ReconnectAsync()
    {
        while (true)
        {
            try
            {
                await Task.Delay(5000);
                var leader = await _serviceDiscovery.GetLeaderAsync(_configuration.ServerName);

                if(leader == null)
                {
                    await Task.Delay(5000);
                    continue;
                }

                var url = $"http://localhost:{leader.port}{_configuration.HubPath}";

                Connection?.Closed -= OnConnectionClosed;
                await Connection!.DisposeAsync();

                Connection = new HubConnectionBuilder().WithUrl(url).Build();
                //_connection.On<Race>("RaceUpdated",  race => _raceEventHandler.HandleAsync(race));

                Connection.Closed += OnConnectionClosed;

                await Connection.StartAsync(cancellationToken);
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
        if(Connection != null)
            await Connection.DisposeAsync();
    }

    
}