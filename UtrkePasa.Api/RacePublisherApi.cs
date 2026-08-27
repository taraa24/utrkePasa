using Microsoft.AspNetCore.SignalR;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Api;

public class RcaePublisherApi 
{
  
    private readonly IHubContext<RaceHub> _hubContext;

    public RcaePublisherApi(IHubContext<RaceHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task RaceOpenFoGambling(Race race)
    {
        await _hubContext.Clients.All.SendAsync("OpenForGambling", race);
    }

    public async Task RaceStarted(Race race)
    {
        await _hubContext.Clients.All.SendAsync("raceStarted", race);
    }

    public async Task RaceFinished(Race race)
    {
        await _hubContext.Clients.All.SendAsync("raceFinished", race);
    }
}