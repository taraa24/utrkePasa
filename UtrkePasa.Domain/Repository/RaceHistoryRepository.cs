using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;


namespace UtrkePasa.Domain.Repository;

public class RaceHistoryRepository : IRaceHistoryRepository
{
    private readonly AppDbContext _context;
    public RaceHistoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(List<RaceHistory> history)
    {
        _context.AddRange(history);
    }
}