using Microsoft.AspNetCore.SignalR.Client;

namespace UtrkePasa.Server;

public class ServerRace : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly MessageBus _messageBus;
    private readonly ILogger<ServerRace> _logger;
    private readonly HubConnection _connection;

    private readonly RunningRace _runningRace;

    public ServerRace(IServiceScopeFactory scopeFactory, MessageBus messageBus, ILogger<ServerRace> logger)
    {
        _scopeFactory = scopeFactory;
        _messageBus = messageBus;
        _logger = logger;

        _connection = new HubConnectionBuilder().WithUrl("http://localhost:5057/raceHub").WithAutomaticReconnect().Build();

        _runningRace = new RunningRace(scopeFactory,messageBus, new RaceSimulator(), logger, _connection);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {        

        try
        {
            await _connection.StartAsync(stoppingToken);

            _logger.LogInformation("Spojenooo");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Nije se moguće spojiti na SignalR API");
            
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            try 
            {
                await _runningRace.CheckSteps();                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,
                    "Greška u ServerRace"
                );
            }
            finally
            {
                await Task.Delay(1000, stoppingToken);
                
            }
        }
    }
}


