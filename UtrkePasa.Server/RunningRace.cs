using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Domain.Repository;

namespace UtrkePasa.Server;

public class RunningRace(IServiceScopeFactory scopeFactory, RaceSimulator _simulator,  ILogger logger)
{
    private const int TickIntervalMs = 500;
    private readonly Random _random = new();

    private List<Race> _pendingRaces = new ();
    private bool _started;

    public async Task<Race> OneRaceAsync(CancellationToken stoppingToken)
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
            winner = _simulator.Racing(states, race);
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

    internal async Task CheckPendingRaces()
    {
        using var scope = scopeFactory.CreateScope();
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

    internal async Task CheckSteps()
    {
        if (_started == false)
            await LoadState();

        await CloseFinishedRaces();
        await OpenNewRaces();
        await OpenForGambling();
        await StartRace();
        await RunRace();
        await FinishRunningRace();
    }

    

    private async Task FinishRunningRace()
    {
        throw new NotImplementedException();
    }
    private async Task RunRace()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var race = await context.Race.FirstOrDefaultAsync(r => r.RaceStatus == "InProgress");

        if(race == null)return;

        Console.WriteLine("utrka je zarsila");
        if(race.EndOfTheRace <= DateTimeOffset.UtcNow) return;

        


    }

    private async Task StartRace()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var race = await context.Race.FirstOrDefaultAsync(r => r.RaceStatus == "Open");

        if(DateTimeOffset.UtcNow >= race.StartOfTheRace)
        {
            race.RaceStatus = "InProgress";

            await context.SaveChangesAsync();

            Console.WriteLine("Startt");
            logger.LogInformation("U utrci su {Dogs}", string.Join(", ", race.DogStartingPosition));
        }
    }

    private async Task OpenForGambling()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var race = await context.Race.FirstOrDefaultAsync(r => r.RaceStatus == "Created");

        if(race == null) return;

        var afterBettingRace = _simulator.OpenBetting(race);

        context.Race.Attach(afterBettingRace);

        await context.SaveChangesAsync();

        logger.LogInformation("mozemmo se kladit");

    }

    private async ValueTask OpenNewRaces()
    {

        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var dogRepository = scope.ServiceProvider.GetRequiredService<IDogRepository>();


        var allDogs = await dogRepository.GetAllDogsAsync();

        var states = _simulator.CreateStartingLineup(allDogs);
        var startingPlaces = _simulator.SetStartPlace(states);

        var race = new Race
        {
            RaceName = "test",
            DogStartingPosition = startingPlaces,
            RaceStatus = "Created"
        };
        
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
                pr.RaceStatus = "Finished";
                context.Race.Attach(pr);
            }
        }
        await context.SaveChangesAsync();
    }


    private async Task LoadState()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var raceRepository = scope.ServiceProvider.GetRequiredService<IRaceRepository>();

        var _pendingRaces = await raceRepository.GetPendingRaces();
        _started = true;
        //TODO: ucitaj sve pending utrke iz baze
    }
}
