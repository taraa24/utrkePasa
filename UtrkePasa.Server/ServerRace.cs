
using UtrkePasa.Infrastructure;

namespace UtrkePasa.Server;

public class ServerRace(IServiceScopeFactory scopeFactory, MessageBus messageBus,
    ILogger<ServerRace> logger,RacePublisher racePublisher, ServiceDiscovery serviceDiscovery) : BackgroundService
{
    private readonly ServiceDiscovery _serviceDiscovery = serviceDiscovery;

    private readonly RunningRace _runningRace = new(scopeFactory, messageBus, new RaceSimulator(), logger, racePublisher);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {        
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!_serviceDiscovery.IAmTheLeader)
            {
                await Task.Delay(1000, stoppingToken);
                return;
            }
            
            
            try 
            {
                /* await serviceDiscovery.UpdateTimestamp();
                await serviceDiscovery.UpdateLeader(); */
                await _runningRace.CheckSteps();                
            }
            catch(Exception ex)
            {
                logger.LogError(ex,
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


