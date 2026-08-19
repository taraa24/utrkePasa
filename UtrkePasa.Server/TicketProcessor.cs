using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class TicketProcessor(IServiceScopeFactory scopeFactory, ILogger<TicketProcessor> logger)
{

    internal async Task Process(JobProcessing job)
    {        
        var _unprocessedTicketsForThisRace = await LoadTickets(job);
        await ProcessTicket(_unprocessedTicketsForThisRace, job.winnerOfRace);
    }


    private async Task<List<Ticket>> LoadTickets(JobProcessing job)
    {

        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Ticket.Where(t => t.RaceId == job.RaceId).ToListAsync();
        
    }

    private async Task ProcessTicket(List<Ticket> _unprocessedTicketsForThisRace, string winner)
    {

        foreach (var ticket in _unprocessedTicketsForThisRace)
        {
            ticket.IsWinningTicket = ticket.ExpectedResult == winner;
            Console.WriteLine(
               $"Ticket {ticket.TicketId}: " +
                $"{(ticket.IsWinningTicket ? "DOBITAN" : "NIJE DOBITAN")}"
            );
        }

        Console.WriteLine("procesiranje...");

        _unprocessedTicketsForThisRace.Clear();

    }
}