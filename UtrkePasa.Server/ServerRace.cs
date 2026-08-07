namespace utrkePasa.Server;

public class ServerRace(ILogger<ServerRace> logger) : BackgroundService
{
    private static readonly TimeSpan RaceDuration = TimeSpan.FromSeconds(20);
    private static readonly TimeSpan PauseAfterFinish = TimeSpan.FromSeconds(10);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Starttt");

            await Task.Delay(RaceDuration, stoppingToken);

            logger.LogInformation("Finishhh");

            logger.LogInformation("pauza prije nove trke je {Seconds}", PauseAfterFinish.TotalSeconds);
            await Task.Delay(PauseAfterFinish, stoppingToken);
        }
    }
}
