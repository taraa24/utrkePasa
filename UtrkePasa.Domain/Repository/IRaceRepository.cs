using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public interface IRaceRepository : IRepository<Race>
{
    Task<List<Race>> GetByRaceIdAsync(int raceId);
}