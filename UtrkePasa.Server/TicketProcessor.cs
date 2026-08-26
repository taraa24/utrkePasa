using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Enum;
using UtrkePasa.Infrastructure;

namespace UtrkePasa.Server;

public class TicketProcessor : ISubsriber
{

    private readonly IServiceScopeFactory _scopeFactory;

    public TicketProcessor(IServiceScopeFactory scopeFactory, MessageBus messageBus)
    {
        _scopeFactory = scopeFactory;

        messageBus.AddSubscriber(this);

    }


    public async Task recieveJob(JobProcessing job)
    {
        if (job.JobType != ProcessingJobType.RaceFinished)
            return;

        await ProcessTicket(job);
    }

    private async Task ProcessTicket(JobProcessing job, int BatchSize = 500)
    {

        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        while (true)
        {
            var _unprocessedTicketsForThisRace = await context.Ticket.Where(t => t.RaceId == job.RaceId && t.IsWinningTicket == null)
                                            .OrderBy(t => t.TicketId).Take(BatchSize).ToListAsync();


            if (_unprocessedTicketsForThisRace.Count == 0)
                break;

            var finalPositions = job.dogFinalePositions.Select(x => x.Split(':')).Where(x => x.Length == 2 && int.TryParse(x[1], out _))
                                                        .ToDictionary(x => x[0], x => int.Parse(x[1]));


            foreach (var ticket in _unprocessedTicketsForThisRace)
            {

                var expectedPositions = ticket.oddType.Select(c => int.Parse(c.ToString())).ToList();

                ticket.IsWinningTicket = finalPositions.TryGetValue(ticket.ExpectedResult,out var actualPosition)&& expectedPositions.Contains(actualPosition);

                Console.WriteLine(
                $"Ticket {ticket.TicketId}: " +
                    $"{((bool)ticket.IsWinningTicket ? "DOBITAN" : "NIJE DOBITAN")}"
                );
            }   

            await context.SaveChangesAsync();

            context.ChangeTracker.Clear();// za brisanje tiketa iz memorije
            
        }


        var race = await context.Race.FirstOrDefaultAsync(r => r.RaceId == job.RaceId);
        if(race != null) 
            race?.RaceStatus = "Finished";
            await context.SaveChangesAsync();

        
        Console.WriteLine("procesiranje...");

    }
}