using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.Repository;
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
}