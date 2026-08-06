using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.Entities;
using UtrkePasa.Domain.DataBase;

 
namespace UtrkePasa.Domain.Repository;
 
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
 
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.email == email);
    }
}
