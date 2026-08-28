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

    public async Task RaceOpenFoGambling()
    {
        await _hubContext.Clients.All.SendAsync("OpenForGambling");
        
    }

    public async Task RaceStarted()
    {
        await _hubContext.Clients.All.SendAsync("raceStarted");
        
    }

    public async Task RaceFinished()
    {
        await _hubContext.Clients.All.SendAsync("raceFinished");
        
    }
}