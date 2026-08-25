using DotNetEnv;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Server;
using Microsoft.EntityFrameworkCore;
using UtrkePasa.Domain.Repository;
using System.Net;
using UtrkePasa.Infrastructure;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Reflection.Metadata.Ecma335;

Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddSignalR();
builder.Services.AddSingleton<RacePublisher>();
builder.Services.AddSingleton<IRunningPort,RunningPort>();
builder.Services.AddHostedService<ServerRace>();

builder.Services.AddSingleton<ServiceDiscovery>();

/* builder.Services.AddHostedService<JobProcessingServer>();
 */

builder.Services.AddHostedService(sp =>
    sp.GetRequiredService<ServiceDiscovery>());

builder.Services.Configure<ServiceDiscoveryConfiguration>(builder.Configuration.GetSection("ServiceDiscoveryConfiguration"));
var app = builder.Build();
app.Services.GetRequiredService<TicketProcessor>();
app.Services.GetRequiredService<FiscalizeClosedRace>();

/* var serviceDiscovery = app.Services.GetRequiredService<ServiceDiscovery>();

await serviceDiscovery.RegisterAsync("UtrkePasa.Server");

app.Lifetime.ApplicationStopped.Register( () =>

    serviceDiscovery.ShutingDownRegisterAsync().GetAwaiter().GetResult()

); */

app.MapHub<RaceHub>("/raceHub");

app.Run();



