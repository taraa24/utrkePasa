using utrkePasa.Server;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ServerRace>();

var host = builder.Build();
host.Run();
