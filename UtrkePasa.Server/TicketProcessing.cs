
using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class TicketProcessing(IServiceScopeFactory scopeFactory, ILogger logger)
{
    private ProcessingTicket? _processingTicket;  
    private List<Ticket> _unprocessedTickets = new();
    private bool _loaded = false;
    internal async Task CheckSteps()
    {
        if (!_loaded)
           await LoadDataFromDb(); 
        
        ProcessTicket();
    }


    private async Task LoadDataFromDb()
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        _processingTicket = await context.ProcessingTicket.FirstOrDefaultAsync(pt=> pt.ProcessingTicketStatus == "Pending");

        _loaded = true;
    }

    private void ProcessTicket()
    {
        throw new NotImplementedException();
    }

}