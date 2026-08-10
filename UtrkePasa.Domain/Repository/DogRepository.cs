using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public class DogRepository : Repository<Dog>, IDogRepository
{
    public DogRepository(AppDbContext context) : base(context)
    {
    }


    public async Task<List<Dog>> GetAllDogsAsync()
    {
        return await _dbSet.ToListAsync();    
    }
}