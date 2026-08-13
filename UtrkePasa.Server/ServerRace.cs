using UtrkePasa.Domain.Repository;
namespace UtrkePasa.Server;

public class ServerRace(IServiceScopeFactory scopeFactory,ILogger<ServerRace> logger) : BackgroundService
{
    private readonly RunningRace _runningRace = new(scopeFactory, new RaceSimulator(), logger);
    
    private bool _started = false;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {        
        while (!stoppingToken.IsCancellationRequested)
        {
            try 
            {
                await _runningRace.CheckSteps();                
            }
            catch
            {
                
            }
            finally
            {
                await Task.Delay(1000, stoppingToken);
                
            }
            /* await _runningRace.OneRaceAsync(stoppingToken);
            await Task.Delay(1000, stoppingToken); */
        }
    }
}


