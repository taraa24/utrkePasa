namespace UtrkePasa.Server;

public class ServerRace(IServiceScopeFactory scopeFactory ,MessageBus _messageBus, ILogger<ServerRace> logger) : BackgroundService
{
    private readonly RunningRace _runningRace = new(scopeFactory,_messageBus, new RaceSimulator(), logger);

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


