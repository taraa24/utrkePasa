using System.Reflection.Metadata;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UtrkePasa.Domain.Entities;

namespace UtrkePasa.ConsoleApp;

public class RaceSubscriber
{

    private HubConnection _connection;
    //Property HubConnection _connection {private set; get;}

    private readonly HandlerOnConnectionEvent _handlerOnConnectionEvent;



    public RaceSubscriber(HandlerOnConnectionEvent handlerOnConnectionEvent)
    {

        _handlerOnConnectionEvent = handlerOnConnectionEvent;

        _connection = new HubConnectionBuilder().WithUrl("http://localhost:5057/raceHubApi").WithAutomaticReconnect().Build();
        
        _connection.On("OpenForGambling", _handlerOnConnectionEvent.HandleOpenBettingAsync);
        _connection.On("raceStarted", _handlerOnConnectionEvent.HandleStartingRace);
        _connection.On("raceFinished", _handlerOnConnectionEvent.HandleFinishRace);

    }

    public async Task StartAsync()
    {
        await _connection.StartAsync();
    }

    public async Task StopAsync()
    {
        await _connection.DisposeAsync();
    }


}