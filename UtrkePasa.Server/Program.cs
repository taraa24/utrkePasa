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


/* builder.Services.AddSingleton<CommunicationSingleton>();
 */
builder.Services.AddScoped<IDogRepository, DogRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
/* builder.Services.AddSingleton<JobProcessingHandler>();
 */builder.Services.AddSingleton<TicketProcessor>();
builder.Services.AddSingleton<FiscalizeClosedRace>();
builder.Services.AddSingleton<MessageBus>();


builder.Services.AddHostedService<ServerRace>();
/* builder.Services.AddHostedService<JobProcessingServer>();
 */
var host = builder.Build();

host.Services.GetRequiredService<TicketProcessor>();
host.Services.GetRequiredService<FiscalizeClosedRace>();

host.Run();

