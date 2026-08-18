using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class TicketProcessor(IServiceScopeFactory scopeFactory, ILogger<TicketProcessor> logger)
{

    internal async Task Process(JobProcessing job)
    {        
        var _unprocessedTicketsForThisRace = await LoadTickets(job);
        var raceHistory = await LoadWinner(job.RaceHistoryId);
        await ProcessTicket(_unprocessedTicketsForThisRace, raceHistory);
    }


    private async Task<List<Ticket>> LoadTickets(JobProcessing job)
    {

        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Ticket.Where(t => t.RaceId == job.RaceId).ToListAsync();
        
    }

    private async Task<RaceHistory?> LoadWinner(int raceHistoryId)
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.RaceHistory.FirstOrDefaultAsync(rh => rh.HistoryRaceId == raceHistoryId);
    }


    private async Task ProcessTicket(List<Ticket> _unprocessedTicketsForThisRace, RaceHistory raceHistory)
    {

        foreach (var ticket in _unprocessedTicketsForThisRace)
        {
            ticket.IsWinningTicket = ticket.RaceOdds!.ExpectedResult == raceHistory.Dog!.DogName;
            Console.WriteLine(
               $"Ticket {ticket.TicketId}: " +
                $"{(ticket.IsWinningTicket ? "DOBITAN" : "NIJE DOBITAN")}"
            );
        }

        Console.WriteLine("procesiranje...");

        _unprocessedTicketsForThisRace.Clear();

    }
}