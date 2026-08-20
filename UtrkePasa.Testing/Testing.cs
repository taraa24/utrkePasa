using System.Diagnostics;
using System.Net.Http.Json;
using System.Threading.Tasks.Dataflow;
using UtrkePasa.Api.Dtos;

namespace UtrkePasa.Testing;

public class Testing
{

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

    private static string GetRandomExpectedResult()
    {
        return DogNames[Random.Shared.Next(DogNames.Length)];
    }
    private static async Task<bool> PurchaseTicketAsync(HttpClient client)
    {

        var request = new TicketPurchaseRequest
        {
            UserId = 1,
            PlacedAt = DateTimeOffset.UtcNow,
            ExpectedResult = GetRandomExpectedResult(),
            RaceOddsId = 2,
            PaidForTicket = 10
        };

        try
        {
            var response = await client.PostAsJsonAsync("api/ticketPurchase", request);

            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(
                    $"HTTP {(int)response.StatusCode}: {body}");

                return false;
            }

            var result =
                System.Text.Json.JsonSerializer.Deserialize<TicketPurchaseResponse>(
                    body);

            return result != null && !result.IsFailed;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"EXCEPTION: {ex.Message}");
            return false;
        }
    }

    public static async Task Main(string[] args)
    {

        const int numberOfTickets = 30000;
        const int maxDegreeOfParallelism = 2;

        using var client = new HttpClient
        {
          BaseAddress = new Uri("http://localhost:5057")  
        };

        int successfulTickets = 0;

        var stopwatch = Stopwatch.StartNew();

        var block = new ActionBlock<int>(async _ =>
        {
            if(await PurchaseTicketAsync(client))
                Interlocked.Increment(ref successfulTickets);
        }, new ExecutionDataflowBlockOptions
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism
        });

         for (int i = 0; i < numberOfTickets; i++)
            block.Post(i);

        block.Complete();
        await block.Completion;

        stopwatch.Stop();

        Console.WriteLine($"Success: {successfulTickets}/{numberOfTickets}, Elapsed: {stopwatch.ElapsedMilliseconds}ms");

    }
}
