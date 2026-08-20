
using UtrkePasa.Domain.Enum;

namespace UtrkePasa.Server;

public class FiscalizeClosedRace : ISubsriber
{

    public FiscalizeClosedRace(MessageBus messageBus)
    {
        messageBus.AddSubscriber(this);
    }
    public async Task recieveJob(JobProcessing job)
    {
        if (job.JobType != ProcessingJobType.RaceFinished)
            return;

        await Process(job);
    }

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