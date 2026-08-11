
namespace UtrkePasa.Server;

public class ServerRace(ILogger<ServerRace> logger, IServiceScopeFactory scopeFactory) : BackgroundService
{
    private static readonly TimeSpan PauseAfterFinish = TimeSpan.FromSeconds(10);
    private readonly RunningRace _runningRace = new(scopeFactory, new RaceSimulator(), logger);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await _runningRace.OneRaceAsync(stoppingToken);

            logger.LogInformation("pauza prije nove trke je {Seconds}", PauseAfterFinish.TotalSeconds);
            await Task.Delay(PauseAfterFinish, stoppingToken);
        }
    }
}


