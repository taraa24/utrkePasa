using System.Net;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Infrastructure;

public interface IRunningPort
{
    int GetPort();
}

public class ServiceDiscovery(
    IServiceScopeFactory scopeFactory, 
    IRunningPort runningPort,
    IOptions<ServiceDiscoveryConfiguration> configuration) : BackgroundService
{
    private readonly IRunningPort _runningPort = runningPort;
    private readonly IOptions<ServiceDiscoveryConfiguration> _configuration = configuration;
    private Guid _appGuid;
    public bool IAmTheLeader { get;private set; }
    private string _appName;


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await RegisterAsync("UtrkePasa.Server");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateTimestamp();
                await UpdateLeader();

                await Task.Delay(
                    TimeSpan.FromSeconds(1),
                    stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            
        }
        finally
        {
            await ShutingDownRegisterAsync();
        }
    }


    public async Task RegisterAsync(string applicationName)
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        _appName = applicationName;
        _appGuid  = Guid.NewGuid();

        //var leaderExist = await context.Register.AnyAsync(r => r.isLeader && r.timestamp != null); 

        var registar = new Register
        {
            appName = applicationName,
            appGuid = _appGuid,
            ipAddr = GetLocalIpAddr(),
            timestamp = DateTimeOffset.UtcNow,
            isLeader = false,
            port = _runningPort.GetPort()
        };

        context.Register.Add(registar);

        await context.SaveChangesAsync();

        await UpdateLeader();
    }


    public async Task ShutingDownRegisterAsync()
    {

        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var registar = await context.Register.FirstOrDefaultAsync(r => r.appGuid == _appGuid);

        if(registar == null) return;

        registar.timestamp = null;
        registar.isLeader = false;
        await context.SaveChangesAsync();

    }

    private static string GetLocalIpAddr()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());


        var ip = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

        return ip?.ToString();
    }



    public async Task UpdateTimestamp()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var register = await context.Register.FirstOrDefaultAsync(r => r.appGuid == _appGuid);

        if(register == null) return;

        if(DateTimeOffset.UtcNow >= register?.timestamp!.Value.AddSeconds(5))
        {
            register.timestamp = DateTimeOffset.UtcNow;
        }

        await context.SaveChangesAsync();
    }

    public async Task UpdateLeader()
    {
        if (IAmTheLeader)
            return;
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();


        var timeout = DateTimeOffset.UtcNow.AddSeconds(-10);
        var leaderExist = await context.Register.SingleOrDefaultAsync(leader => leader.appName == _appName && leader.isLeader );
        if (leaderExist  == null)
        {
            await ClaimLeadership(context);
            return;
        }

        if(leaderExist.timestamp != null && leaderExist.timestamp.Value > timeout)
        {
            return;
        }

        await CleanLeadership(context, leaderExist);
        await ClaimLeadership(context);
    }

    private async Task CleanLeadership(AppDbContext context, Register leaderExist)
    {
        await context.Register
            .Where(r => r.appName == _appName && r.isLeader == true && r.appGuid == leaderExist.appGuid)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(r => r.isLeader, false));
    }

    private async Task ClaimLeadership(AppDbContext context)
    {
        try
        {
            var updated = await context.Register
                .Where(r => r.appGuid == _appGuid && r.appName == _appName)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(r => r.isLeader,
                        true)); //set leader = true, where heartbeat missed x2 and appname == _appname and uuid == moj uuid
            if (updated == 1)
                IAmTheLeader = true;
        }
        catch (Exception e)
        {
            
        }
        
    }

    public async Task<Register?> GetLeaderAsync()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Register.Where(r => r.isLeader && r.timestamp != null).FirstOrDefaultAsync();
    }
}

public class ServiceDiscoveryConfiguration
{
    public bool Singleton { get; set; }
}
