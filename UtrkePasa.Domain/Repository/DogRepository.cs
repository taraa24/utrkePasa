using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;

public class DogRepository : IDogRepository
{

    private readonly AppDbContext _context;
    public DogRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<Dog>> GetAllDogsAsync()
    {
        return await _context.Dog.ToListAsync();    
    }
}