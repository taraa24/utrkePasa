using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Domain.DataBase;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> User {get; set;}
    public DbSet<Ticket> Ticket {get;set;}
    public DbSet<Dog> Dog {get;set;}
    public DbSet<Race> Race {get;set;}
    public DbSet<RaceOdds> RaceOdds{get;set;}
    public DbSet<RaceHistory> RaceHistory{get;set;}
}