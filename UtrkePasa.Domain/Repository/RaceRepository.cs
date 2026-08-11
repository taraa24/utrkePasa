using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public class RaceRepository : Repository<Race>, IRaceRepository
{
    public RaceRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Race>> GetByRaceIdAsync(int raceId)
    {
        return await _dbSet.Where(r=>r.race_Id == raceId).ToListAsync();
    }

    public async Task<Race?> GetCurrentActiveRaceAsync()
    {
        return await _dbSet.Include(r => r.odds).Where(r => r.result_Of_Race == null).OrderByDescending(r => r.start_Of_The_Race).FirstOrDefaultAsync();
    }
}