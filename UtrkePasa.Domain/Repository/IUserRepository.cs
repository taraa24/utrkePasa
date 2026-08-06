using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;
public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}