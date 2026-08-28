namespace UtrkePasa.ConsoleApp;

public class HandlerOnConnectionEvent
{
    
    public Task HandleOpenBettingAsync()
    {
        Console.WriteLine("kladenje je krenilo mozete se kladiti sve dok ne krene utrka");
        Console.WriteLine("Upisi broj tiketa koji zelis uplatit");
        
        return Task.CompletedTask;
    }

    public Task HandleStartingRace()
    {
        Console.WriteLine("startt utrka je krenila nema vise kladenja");

        return Task.CompletedTask;
    }

    public Task HandleFinishRace()
    {
        Console.WriteLine("finishh utrka je zavrsila");

        return Task.CompletedTask;
    }


}