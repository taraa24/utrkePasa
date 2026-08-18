using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Domain.Repository;

namespace UtrkePasa.Server;

public class RunningRace(IServiceScopeFactory scopeFactory, RaceSimulator _simulator,  ILogger logger)
{
    private List<Race> _pendingRaces = new ();
    private List<Dog> _dogs = new();
    private List<DogRaceState> _dogRacestates = new();
    private bool _started;

    internal async Task CheckSteps()
    {
        if (_started == false)
            await LoadState();

        
        await OpenNewRaces();
        await OpenForGambling();
        await StartRace();
        RunRace();
        await FinishRunningRace();
        
    }

    
    private async Task FinishRunningRace()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var race = _pendingRaces.FirstOrDefault(r => r.RaceStatus == "InProgress");

        if(race == null) return;

        if(DateTimeOffset.UtcNow < race.EndOfTheRace) return;
        var standings = _simulator.GetFinalStandings(_dogRacestates);

        var winner = standings.First();

        race.ResultOfRace = winner.Dog!.DogName;
        race.RaceStatus = "Processing";

        context.Race.Update(race);
        

        var historyEntries = new List<RaceHistory>();

        for (int i = 0; i < standings.Count; i++)
        {
            var state = standings[i];
            historyEntries.Add(new RaceHistory
            {
                RaceId = race.RaceId,
                DogId = state.Dog!.DogId,
                FinalePosition = i + 1,
                IsWinner = i == 0
            });
        }

        await context.RaceHistory.AddRangeAsync(historyEntries);
        
        await context.SaveChangesAsync();

        var winnerHistory = historyEntries.First(h => h.IsWinner);
        context.ProcessingTicket.Add(new ProcessingTicket
        {
            RaceId = race.RaceId,
            RaceHistoryId = winnerHistory.HistoryRaceId,
            ProcessingTicketStatus = "Pending"
        });

        await context.SaveChangesAsync();


        _pendingRaces.Remove(race);

        _dogRacestates.Clear();

        logger.LogInformation("Finishhh");
        logger.LogInformation("Pobjednik jee {Winner}", race.ResultOfRace);


    }
    private void RunRace()
    {

        var race = _pendingRaces.FirstOrDefault(r => r.RaceStatus == "InProgress");

        if(race == null)return;

        if(race.EndOfTheRace <= DateTimeOffset.UtcNow) return;
        
        _simulator.Racing(_dogRacestates);

    }

    private async Task StartRace()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var race = _pendingRaces.FirstOrDefault(r => r.RaceStatus == "Open");

        if(race == null) return;

        if(DateTimeOffset.UtcNow < race.StartOfTheRace)
        {
            return;
        }

        race.RaceStatus = "InProgress";

        context.Race.Update(race);
        await context.SaveChangesAsync();

        Console.WriteLine("Startt");
        logger.LogInformation("U utrci su {Dogs}", string.Join(", ", race.DogStartingPosition));
    }

    private async Task OpenForGambling()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var race = _pendingRaces.FirstOrDefault(r => r.RaceStatus == "Created");

        if(race == null) return;

        _simulator.OpenBetting(race);

        context.Race.Update(race);

        await context.SaveChangesAsync();

        logger.LogInformation("mozemmo se kladit");

    }

    private async ValueTask OpenNewRaces()
    {
        if(_pendingRaces.Any(r => r.RaceStatus == "Created" || r.RaceStatus == "Open" || r.RaceStatus == "InProgress"))
        {
            return;
        }

        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();


        _dogRacestates = _simulator.CreateStartingLineup(_dogs);
        var startingPlaces = _simulator.SetStartPlace(_dogRacestates);


        var race = new Race
        {
            RaceName = "test",
            DogStartingPosition = startingPlaces,
            RaceStatus = "Created"
        };

        _pendingRaces.Add(race);
        context.Race.Add(race);
        await context.SaveChangesAsync();

    }


    private async Task LoadState()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        _pendingRaces = await context.Race.Where(r => r.RaceStatus != "Finished").ToListAsync();
        _dogs = await context.Dog.ToListAsync();

        var interruptedRace = _pendingRaces.FirstOrDefault(r => r.RaceStatus == "InProgress" && r.EndOfTheRace <= DateTimeOffset.Now);
    
        if (interruptedRace != null)
        {
            interruptedRace.RaceStatus = "Canceled";

            await context.SaveChangesAsync();

            _pendingRaces.Remove(interruptedRace);
        }

        var expiredOpen = _pendingRaces
            .FirstOrDefault(r =>
                r.RaceStatus == "Open" &&
                r.EndOfTheRace <= DateTimeOffset.Now);

        if (expiredOpen != null)
        {
            expiredOpen.RaceStatus = "Canceled";

            logger.LogWarning(
                "Open utrka {RaceId} je istekla tijekom restarta servera. Otkazana.",
                expiredOpen.RaceId);

            _pendingRaces.Remove(expiredOpen);
        }

        _started = true;
    }
}
