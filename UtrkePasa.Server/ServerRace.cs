using UtrkePasa.Domain.Repository;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class ServerRace(ILogger<ServerRace> logger, IServiceScopeFactory scopeFactory) : BackgroundService
{

    private static readonly TimeSpan PauseAfterFinish = TimeSpan.FromSeconds(10);
    private const int TickIntervalMs = 500;

    private readonly RaceSimulator _simulator = new();
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope(); // treba ga negdi smjestit a da nije tu
            var dogRepository = scope.ServiceProvider.GetRequiredService<IDogRepository>();
            var allDogs = await dogRepository.GetAllDogsAsync();


            var states = _simulator.CreateStartingLineup(allDogs);
            logger.LogInformation("U utrci su {Dogs}", string.Join(", ", states.Select(s => s.Dog!.dog_Name)));
            logger.LogInformation("Starttt");

            DogRaceState? winner = null;

            while (winner == null && !stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TickIntervalMs, stoppingToken);
                winner = _simulator.Racing(states);
            }

            logger.LogInformation("Finishhh");
            logger.LogInformation("Pobjednik jee {Winner}", winner?.Dog.dog_Name);

            logger.LogInformation("pauza prije nove trke je {Seconds}", PauseAfterFinish.TotalSeconds);
            await Task.Delay(PauseAfterFinish, stoppingToken);
        }
    }
}


