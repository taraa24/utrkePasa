using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using UtrkePasa.Infrastructure;

namespace UtrkePasa.Server;

public class RunningPort : IRunningPort
{
    private IServer _servicer;

    public RunningPort(IServer servicer)
    {
        _servicer = servicer;
    }

    public int GetPort ()
    {
        var addresses = _servicer.Features.Get<IServerAddressesFeature>();
        var first = addresses?.Addresses.FirstOrDefault();
        return int.Parse(first.Split(':')[2]);
    }
} 
