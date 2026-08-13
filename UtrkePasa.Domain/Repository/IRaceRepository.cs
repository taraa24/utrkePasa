using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public interface IRaceRepository 
{
    Task<Race?> GetByRaceIdAsync(int raceId);
    Task<Race?> GetCurrentActiveRaceAsync();
    Task<List<Race>> GetPendingRaces();
}