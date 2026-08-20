using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Domain.Enum;

namespace UtrkePasa.Server;

public class RunningRace(IServiceScopeFactory scopeFactory, MessageBus _messageBus, RaceSimulator _simulator,  
ILogger logger) 
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
        race.DogFinalePosition = _simulator.FormatFinalPositions(standings);

        context.Race.Update(race);

        var job = new JobProcessing
        {
            JobType = ProcessingJobType.RaceFinished,
            JobStatus = "Pending",
            RaceId = race.RaceId,
            winnerOfRace = race.ResultOfRace
        };

        /* var FiscalizeJob = new JobProcessing
        {
            JobType = ProcessingJobType.FiscalizeClosedRace,
            JobStatus = "Pending",
            RaceId = race.RaceId,
            winnerOfRace = race.ResultOfRace
        }; */

        await context.SaveChangesAsync();
        //await _communicationSingleton.AddJob(job);  
        //await _communicationSingleton.AddJob(FiscalizeJob);

        await _messageBus.Publish(job);


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
        logger.LogInformation("U utrci su {Dogs}", string.Join(", ", race.DogStartingPosition));
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

        var stuckRaces = _pendingRaces.Where(r => r.RaceStatus == "Processing").ToList();

        foreach(var race in stuckRaces)
        {
            await _messageBus.Publish(new JobProcessing
            {
                JobType = ProcessingJobType.RaceFinished,
                JobStatus = "Pending",
                RaceId = race.RaceId,
                winnerOfRace = race.ResultOfRace
            });

            _pendingRaces.Remove(race);
        }


        var expiredInProgress = _pendingRaces.FirstOrDefault(r => r.RaceStatus == "InProgress" && r.EndOfTheRace <= DateTimeOffset.Now);
        if (expiredInProgress != null)
        {
            expiredInProgress.RaceStatus = "Canceled";

            logger.LogWarning("Utrka {RaceId} je istekla tijekom restarta servera", expiredInProgress.RaceId);

            _pendingRaces.Remove(expiredInProgress);
        }


        var expiredOpen = _pendingRaces.FirstOrDefault(r => r.RaceStatus == "Open" && r.EndOfTheRace <= DateTimeOffset.Now);
        if (expiredOpen != null)
        {
            expiredOpen.RaceStatus = "Canceled";

            logger.LogWarning("Utrka {RaceId} je istekla tijekom restarta servera", expiredOpen.RaceId);

            _pendingRaces.Remove(expiredOpen);
        }


        var activeRace = _pendingRaces.FirstOrDefault(r => r.RaceStatus == "Open" || r.RaceStatus == "InProgress");
        if (activeRace != null)
        {
            _dogRacestates = _simulator.RestoreRaceStates(activeRace,_dogs);
        }

        await context.SaveChangesAsync();

        _started = true;
    }

    
}
