using UtrkePasa.Api.Dtos;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.ConsoleApp;

public class TicketPurchaseClient
{
    private readonly HttpClient _httpClient;
    private readonly Random _random = new();

    private static readonly string[] DogNames =
    {
        "Flekica",
        "Bubi",
        "Lessi",
        "Mac",
        "Roni", 
        "Cheese",
        "Rea"
    };

    public TicketPurchaseClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private static string GetRandomExpectedResult()
    {
        return DogNames[Random.Shared.Next(DogNames.Length)];
    }

    public async Task SendTicketAsync(int numOfTickets)
    {
        var tickets = GetRandomTickets(numOfTickets);

        foreach(var ticket in tickets)
        {
            var response = await _httpClient.PostAsJsonAsync(
            "api/ticketpurchase",
            ticket);

            var body = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Status: {(int)response.StatusCode}");
            Console.WriteLine($"Response: {body}");
        }

        
        
    }

    public List<TicketPurchaseRequest> GetRandomTickets(int numOfTickets)
    {
        var tickets = new List<TicketPurchaseRequest>();

        for(int i = 0; i < numOfTickets; i++)
        {
            var userId = _random.Next(1,3);
            var PaidForTicket = _random.Next(10, 1001);
            var ExpectedResult = GetRandomExpectedResult();
            var oddType = _random.Next(1,6);

            tickets.Add(new TicketPurchaseRequest
            {
                UserId = userId,
                PaidForTicket = PaidForTicket,
                ExpectedResult = ExpectedResult,
                oddType = oddType.ToString()
            });
        }
        return tickets;
    }
}