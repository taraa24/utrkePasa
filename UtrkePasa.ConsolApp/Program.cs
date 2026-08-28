/* var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run(); */

using UtrkePasa.ConsoleApp;

var handler = new HandlerOnConnectionEvent();
var raceSubscriber = new RaceSubscriber(handler);

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

