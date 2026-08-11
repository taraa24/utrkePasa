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
}