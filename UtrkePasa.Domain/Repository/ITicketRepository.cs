using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public interface ITicketRepository : IRepository<Ticket>
{
    Task SaveTicketToDbAsync(Ticket ticket);
}