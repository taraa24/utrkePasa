
namespace UtrkePasa.Server;

public class FiscalizeClosedRace
{
    internal async Task Process(JobProcessing job)
    {
        await LoadData(job);
    }

    private async Task LoadData(JobProcessing job)
    {
        await Task.Delay(500);
        Console.WriteLine("Fiskalizacija se obradila");
    }
}