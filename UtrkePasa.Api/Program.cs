using Microsoft.EntityFrameworkCore;
using UtrkePasa.Api.Services;
using UtrkePasa.Domain.Repository;
using UtrkePasa.Infrastructure;
using UtrkePasa.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection.")));

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