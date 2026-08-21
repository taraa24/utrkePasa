
namespace UtrkePasa.Server;

public class ServerRace(IServiceScopeFactory scopeFactory, MessageBus messageBus,
    ILogger<ServerRace> logger,RacePublisher racePublisher) : BackgroundService
{

    private readonly RunningRace _runningRace = new(scopeFactory, messageBus, new RaceSimulator(), logger, racePublisher);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {        

        while (!stoppingToken.IsCancellationRequested)
        {
            try 
            {
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


