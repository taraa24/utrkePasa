using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class TicketProcessor(IServiceScopeFactory scopeFactory, ILogger<TicketProcessor> logger)
{

    internal async Task Process(JobProcessing job)
    {        
        var _unprocessedTicketsForThisRace = await LoadTickets(job);
        await ProcessTicket(_unprocessedTicketsForThisRace, job);
    }


    private async Task<List<Ticket>> LoadTickets(JobProcessing job)
    {

        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Ticket.Where(t => t.RaceId == job.RaceId).ToListAsync();
        
    }

    private async Task ProcessTicket(List<Ticket> _unprocessedTicketsForThisRace, JobProcessing job)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        foreach (var ticket in _unprocessedTicketsForThisRace)
        {
            ticket.IsWinningTicket = ticket.ExpectedResult == job.winnerOfRace;
            Console.WriteLine(
               $"Ticket {ticket.TicketId}: " +
                $"{(ticket.IsWinningTicket ? "DOBITAN" : "NIJE DOBITAN")}"
            );
        }

        var race = await context.Race.FirstOrDefaultAsync(r => r.RaceId == job.RaceId);
        if(race == null) 
            race?.RaceStatus = "Finished";

        await context.SaveChangesAsync();

        Console.WriteLine("procesiranje...");

        _unprocessedTicketsForThisRace.Clear();

    }
}