using System.Net;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UtrkePasa.Domain.DataBase;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.Infrastructure;

public interface IRunningPort
{
    int GetPort();
}

public class ServiceDiscovery(IServiceScopeFactory scopeFactory, IRunningPort runningPort) : BackgroundService
{
    private Guid _appGuid;

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

        _appGuid  = Guid.NewGuid();

        //var leaderExist = await context.Register.AnyAsync(r => r.isLeader && r.timestamp != null); 

        var registar = new Register
        {
            appName = applicationName,
            appGuid = _appGuid,
            ipAddr = GetLocalIpAddr(),
            timestamp = DateTimeOffset.UtcNow,
            isLeader = false,
            port = port
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
        //if i am the leader ; return;

        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var me = await context.Register.FirstOrDefaultAsync(r => r.appGuid == _appGuid);

        if(me == null || me.isLeader) return;

        var timeout = DateTimeOffset.UtcNow.AddSeconds(-10);

        var leaderExist = await context.Register.AnyAsync(leader => leader.appName == me.appName && leader.isLeader && leader.timestamp != null && leader.timestamp > timeout);

        var updated = await context.Register.Where(r => r.appGuid == me.appGuid && r.appName == me.appName &&  !leaderExist)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(r => r.isLeader, true)); //set leader = true, where heartbeat missed x2 and appname == _appname and uuid == moj uuid

        //if updatd == 1 then i am the leader!


        /* var registar = await context.Register.FirstOrDefaultAsync(r => r.appGuid == _appGuid);

        if(registar == null) return;

        var leaderExist = await context.Register.FirstOrDefaultAsync(r => r.isLeader && r.timestamp != null);

        if(leaderExist == null)
        {
            registar.isLeader = true;
            await context.SaveChangesAsync();
        } */

        
    }


    public async Task<Register?> GetLeaderAsync()
    {
        using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Register.Where(r => r.isLeader && r.timestamp != null).FirstOrDefaultAsync();
    }

}