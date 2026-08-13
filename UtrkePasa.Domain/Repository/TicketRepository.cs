using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;
    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveTicketToDbAsync(Ticket ticket)
    {
        await _context.AddAsync(ticket);
        await _context.SaveChangesAsync();
    }


}