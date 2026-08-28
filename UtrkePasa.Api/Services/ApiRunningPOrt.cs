using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using UtrkePasa.Infrastructure;

namespace UtrkePasa.Api.Services;

public class ApiRunningPort(IServer server) : IRunningPort
{
    public int GetPort()
    {
        var address = server.Features
            .Get<IServerAddressesFeature>()?
            .Addresses
            .FirstOrDefault();

        return address is null ? 0 : new Uri(address).Port;
    }
}