using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public interface ITicketRepository
{
    Task SaveTicketToDbAsync(Ticket ticket);
    Task<RaceOdds?> GetRaceOddsAsync(int raceId, string oddType);
}