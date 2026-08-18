
using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class TicketProcessing(IServiceScopeFactory scopeFactory, ILogger logger)
{
    private ProcessingTicket? _processingTicket;  
    private List<Ticket> _unprocessedTicketsForThisRace = new();
    private bool _loaded = false;


    internal async Task CheckSteps()
    {
        
        await LoadDataFromDb(); 

        if (_processingTicket == null)
            return;
        
        await LoadTickets();
        await ProcessTicket();
    }


    private async Task LoadDataFromDb()
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        _processingTicket = await context.ProcessingTicket.FirstOrDefaultAsync(pt=> pt.ProcessingTicketStatus == "Pending");

    }

    private async Task LoadTickets()
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        _unprocessedTicketsForThisRace = await context.Ticket.Where(t => t.RaceId == _processingTicket.RaceId).ToListAsync();
    }

    private async Task ProcessTicket()
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        foreach (var ticket in _unprocessedTicketsForThisRace)
        {
            Console.WriteLine(
                $"Ticket ID: {ticket.TicketId}, Race ID: {ticket.RaceId}"
            );
        }

        Console.WriteLine("procesiranje...");

        _processingTicket!.ProcessingTicketStatus = "Completed";

        context.ProcessingTicket.Update(_processingTicket);

        await context.SaveChangesAsync();

        _processingTicket = null;
        _unprocessedTicketsForThisRace.Clear();

    }

}