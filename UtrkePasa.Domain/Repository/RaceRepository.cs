using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public class RaceRepository : IRaceRepository
{
    private readonly AppDbContext _context;

    public RaceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Race?> GetByRaceIdAsync(int raceId)
    {
        return await _context.Race.FirstOrDefaultAsync(r => r.RaceId == raceId);
    }

    public async Task<Race?> GetCurrentActiveRaceAsync()
    {
        return await _context.Race.Where(r => r.ResultOfRace == null).Where(s => s.RaceStatus == "Open" || s.RaceStatus == "InProgress").OrderByDescending(r => r.StartOfTheRace).FirstOrDefaultAsync();
    }

    public async Task<List<Race>> GetPendingRaces()
    {
        return await _context.Race.Where(r => r.ResultOfRace == null).Where(s => s.RaceStatus == "InProgress").OrderByDescending(r => r.StartOfTheRace).ToListAsync();
    }
}