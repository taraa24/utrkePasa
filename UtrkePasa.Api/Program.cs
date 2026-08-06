using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using UtrkePasa.Api.Services;
using UtrkePasa.Domain.Repository;
using UtrkePasa.Domain.DataBase;

Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?.Replace("{DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));  

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();    
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

app.Run();