using DotNetEnv;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Server;
using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.Repository;

Env.Load("../.env");
var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?.Replace("{DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));


builder.Services.AddScoped<IDogRepository, DogRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<IRaceHistoryRepository, RaceHistoryRepository>();

builder.Services.AddHostedService<ServerRace>();

var host = builder.Build();
host.Run();

