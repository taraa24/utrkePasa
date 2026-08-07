using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(AppDbContext context) : base(context)
    {
    }

    public async Task SaveTicketToDbAsync(Ticket ticket)
    {
        await AddAsync(ticket);
        await SaveChangesAsync();
    }
}