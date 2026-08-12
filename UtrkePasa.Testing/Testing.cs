using System.Diagnostics;
using System.Net.Http.Json;
using System.Threading.Tasks.Dataflow;
using UtrkePasa.Api.Dtos;

namespace UtrkePasa.Testing;

public class Testing
{
    private static async Task<bool> PurchaseTicketAsync(HttpClient client)
    {

        var request = new TicketPurchaseRequest
        {
            UserId = 1,
            PLacedAt = DateTime.UtcNow,
            RaceOddsId = 1,
            PaidForTicket = 10
        };

        try
        {
            var response = await client.PostAsJsonAsync("api/ticketPurchase", request);

            var result = await response.Content.ReadFromJsonAsync<TicketPurchaseResponse>();
            return response.IsSuccessStatusCode && result != null && !result.IsFailed;
        }
        catch
        {
            return false;
        }
    }

    public static async Task Main(string[] args)
    {

        const int numberOfTickets = 3000;
        const int maxDegreeOfParallelism = 100;

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
