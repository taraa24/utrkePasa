using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public interface IRaceHistoryRepository
{
    Task AddRangeAsync(List<RaceHistory> history);
}