using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public interface IRaceHistoryRepository : IRepository<RaceHistory>
{
    Task AddRangeAsync(List<RaceHistory> history);
}