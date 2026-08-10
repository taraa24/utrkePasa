using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public interface IDogRepository : IRepository<Dog>
{
    Task<List<Dog>> GetAllDogsAsync();
}