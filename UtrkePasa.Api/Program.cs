using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using UtrkePasa.Api.Services;
using UtrkePasa.Domain.Repository;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Api.Middleware;
using UtrkePasa.Api.Handler;
using UtrkePasa.Api;
using UtrkePasa.Infrastructure;


Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?.Replace("{DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));  


builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IValidationService, ValidateTicketService>();
builder.Services.AddScoped<IFiscalizeTicketService, FiscalizeTicketService>();
builder.Services.AddScoped<IPayingHandler, PayingHandler>();

builder.Services.AddScoped<IDogRepository,DogRepository>();
builder.Services.AddScoped<IRaceRepository,RaceRepository>();

builder.Services.AddSingleton<ServiceDiscovery>();
builder.Services.AddSingleton<CurrRaceState>();
builder.Services.AddSingleton<RcaePublisherApi>();
builder.Services.AddSingleton<RaceEventHandler>();

builder.Services.AddSingleton<IRunningPort, ApiRunningPort>();

builder.Services.AddHostedService<RaceSubscriber>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();    
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<MyExceptionMiddleware>();
app.UseMiddleware<RequestLimitMiddleware>();
app.MapControllers();

app.MapHub<RaceHub>("/raceHubApi");

app.Run();