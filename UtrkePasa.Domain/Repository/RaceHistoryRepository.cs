using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;


namespace UtrkePasa.Domain.Repository;

public class RaceHistoryRepository : Repository<RaceHistory>, IRaceHistoryRepository
{
    public RaceHistoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddRangeAsync(List<RaceHistory> history)
    {
        _dbSet.AddRange(history);
    }
}