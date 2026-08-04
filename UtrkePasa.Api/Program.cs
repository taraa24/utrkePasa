using Microsoft.EntityFrameworkCore;
using UtrkePasa.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection.")));

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