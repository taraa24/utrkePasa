/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run(); */

using UtrkePasa.ConsoleApp;

var raceState = new CurrRaceState();
var raceSubscriber = new RaceSubscriber(raceState);

using var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5057/")
};



var ticketPurchaseClient = new TicketPurchaseClient(httpClient);

await raceSubscriber.StartAsync();

while (true)
{
    var input = Console.ReadLine();

    if (input == "q")
        break;

    var isNumeric = int.TryParse(input, out var n);
    if (isNumeric)
    {
        await ticketPurchaseClient.SendTicketAsync(n);
    }

}

await raceSubscriber.StopAsync();

