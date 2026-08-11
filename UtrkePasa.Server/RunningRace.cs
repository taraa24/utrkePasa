using UtrkePasa.Domain.Entities;
using UtrkePasa.Domain.Repository;

namespace UtrkePasa.Server;

public class RunningRace(IServiceScopeFactory scopeFactory, RaceSimulator _simulator, RaceState raceState, ILogger logger)
{
    private const int TickIntervalMs = 500;

    public async Task<Race> OneRaceAsync(CancellationToken stoppingToken)
    {
         using var scope = scopeFactory.CreateScope(); // treba ga negdi smjestit a da nije tu
            var dogRepository = scope.ServiceProvider.GetRequiredService<IDogRepository>();
            var raceRepository = scope.ServiceProvider.GetRequiredService<IRaceRepository>();
            var raceHistoryRepository = scope.ServiceProvider.GetRequiredService<IRaceHistoryRepository>();

            var allDogs = await dogRepository.GetAllDogsAsync();

            var race = new Race
            {
                race_Name = "test",
                start_Of_The_Race = DateTime.UtcNow
            };

            await raceRepository.AddAsync(race);
            await raceRepository.SaveChangesAsync();

            var states = _simulator.CreateStartingLineup(allDogs);


            logger.LogInformation("U utrci su {Dogs}", string.Join(", ", states.Select(s => s.Dog!.dog_Name)));
            logger.LogInformation("Starttt");

            DogRaceState? winner = null;

            while (winner == null && !stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TickIntervalMs, stoppingToken);
                winner = _simulator.Racing(states);
            }

            race.end_Of_The_Race = DateTime.UtcNow;
            race.result_Of_Race = winner?.Dog?.dog_Name;
            raceRepository.Update(race);

            var standings = _simulator.GetFinalStandings(states);

            var historyEntries = states.Select(state => new RaceHistory
            {
                race_Id = race.race_Id,
                dog_Id = state.Dog.dog_Id,
                finale_Position = state.Place,
                is_Winner = state == winner
            }).ToList();

            await raceHistoryRepository.AddRangeAsync(historyEntries);
            await raceRepository.SaveChangesAsync();

            logger.LogInformation("Finishhh");
            logger.LogInformation("Pobjednik jee {Winner}", winner?.Dog?.dog_Name);

        return race;
    }
}