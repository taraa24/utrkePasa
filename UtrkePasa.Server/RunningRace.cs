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

    /* public async Task<Race> OneRaceAsync(CancellationToken stoppingToken)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var dogRepository = scope.ServiceProvider.GetRequiredService<IDogRepository>();
        //var raceRepository = scope.ServiceProvider.GetRequiredService<IRaceRepository>();
        var raceHistoryRepository = scope.ServiceProvider.GetRequiredService<IRaceHistoryRepository>();

        var allDogs = await dogRepository.GetAllDogsAsync();

        var states = _simulator.CreateStartingLineup(allDogs);
        var startingPlaces = _simulator.SetStartPlace(states);

        var race = new Race
        {
            RaceName = "test",
            //StartOfTheRace = DateTime.UtcNow,
            DogStartingPosition = startingPlaces,
            RaceStatus = "Created"
        };

        _simulator.OpenBetting(race);
        
        await context.AddAsync(race);
        await context.SaveChangesAsync();

        logger.LogInformation("mozemmo se kladit");

        await Task.Delay(_random.Next(20000, 40000), stoppingToken);


        logger.LogInformation("U utrci su {Dogs}", string.Join(", ", states.Select(s => s.Dog!.dog_Name)));
        race.RaceStatus = "InPogress";
        await context.SaveChangesAsync();
        logger.LogInformation("Starttt");
        

        DogRaceState? winner = null;

        while (winner == null && !stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TickIntervalMs, stoppingToken);
            _simulator.Racing(race);
        }

        //race.EndOfTheRace = DateTime.UtcNow;
        race.ResultOfRace = winner?.Dog?.dog_Name;
        race.RaceStatus = "Finished";
        context.Update(race);
        

        var standings = _simulator.GetFinalStandings(states);

        var historyEntries = states.Select(state => new RaceHistory
        {
            race_Id = race.RaceId,
            dog_Id = state.Dog.DogId,
            finale_Position = state.Place,
            is_Winner = state == winner
        }).ToList();

        await raceHistoryRepository.AddRangeAsync(historyEntries);
        await context.SaveChangesAsync();

        logger.LogInformation("Finishhh");
        logger.LogInformation("Pobjednik jee {Winner}", winner?.Dog?.dog_Name);

        return race;
    }
 */
   /*  internal async Task CheckPendingRaces()
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var dogRepository = scope.ServiceProvider.GetRequiredService<IDogRepository>();
        var raceRepository = scope.ServiceProvider.GetRequiredService<IRaceRepository>();


        var currentNotFinishedRace = await raceRepository.GetCurrentActiveRaceAsync();

        if(currentNotFinishedRace.EndOfTheRace > DateAndTime.Now)
        {
            //Zatvori utrku
            Console.WriteLine("obrada je zavrsila mozemo kreniti dalje");
            return;
        }

        Console.WriteLine("prijasnja utrka je nedovrsnea");
        //var currentNotFinishedRace = //DOhvati ju;
        //TODO: if vrijeme utrke je zavrseno - obradi sve

    }
 */
    internal async Task CheckSteps()
    {
        if (_started == false)
            await LoadState();

        
        await OpenNewRaces();
        await OpenForGambling();
        await StartRace();
        RunRace();
        await FinishRunningRace();
        await CloseFinishedRaces();
        
    }

    

    private async Task FinishRunningRace()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var race = _pendingRaces.FirstOrDefault(r => r.RaceStatus == "InProgress");

        if(race == null) return;

        if(DateTimeOffset.UtcNow < race.EndOfTheRace) return;
        Console.WriteLine("jesan tuuu");
        var standings = _simulator.GetFinalStandings(_dogRacestates);

        var winner = standings.First();

        race.ResultOfRace = winner.Dog!.dog_Name;
        race.RaceStatus = "Finished";

        context.Race.Update(race);
        

        var historyEntries = new List<RaceHistory>();

        for (int i = 0; i < standings.Count; i++)
        {
            var state = standings[i];
            historyEntries.Add(new RaceHistory
            {
                race_Id = race.RaceId,
                dog_Id = state.Dog!.DogId,
                finale_Position = i + 1,
                is_Winner = i == 0
            });
        }

        await context.RaceHistory.AddRangeAsync(historyEntries);
        
        await context.SaveChangesAsync();

        _pendingRaces.Remove(race);

        _dogRacestates.Clear();

        logger.LogInformation("Finishhh");
        logger.LogInformation("Pobjednik jee {Winner}", race.ResultOfRace);


    }
    private async Task RunRace()
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

    private async ValueTask CloseFinishedRaces()
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var now = DateTimeOffset.UtcNow;
        foreach(var pr in _pendingRaces)
        {
            if(pr.EndOfTheRace < now)
            {
                context.Race.Attach(pr);
                pr.RaceStatus = "Finished";
            }
        }
        await context.SaveChangesAsync();
        _pendingRaces.RemoveAll(r => r.RaceStatus == "Finished");
    }


    private async Task LoadState()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        _pendingRaces = await context.Race.Where(r => r.RaceStatus != "Finished").ToListAsync();
        _dogs = await context.Dog.ToListAsync();


        _started = true;
    }
}
