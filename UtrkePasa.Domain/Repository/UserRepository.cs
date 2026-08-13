using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Domain.DataBase;

 
namespace UtrkePasa.Domain.Repository;
 
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
 
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.email == email);
    }
}
