using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> User {get; set;}
}