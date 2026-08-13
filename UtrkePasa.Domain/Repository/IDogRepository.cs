using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public interface IDogRepository
{
    Task<List<Dog>> GetAllDogsAsync();
}