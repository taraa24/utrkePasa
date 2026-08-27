
using Microsoft.AspNetCore.SignalR;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Server;

public class RacePublisherSErver
{

    private readonly IHubContext<RaceHub> _hubContext;

    public RacePublisherSErver(IHubContext<RaceHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task PublishRaceAsync(Race race) // signalR salje event raceupdated svim klijentima
    {
        await _hubContext.Clients.All.SendAsync("RaceUpdated",race);
    }
}