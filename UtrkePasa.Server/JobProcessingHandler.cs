/* using UtrkePasa.Domain.Enum;

namespace UtrkePasa.Server;

public class JobProcessingHandler(IServiceScopeFactory scopeFactory, CommunicationSingleton _communicationSingleton, TicketProcessor _ticketProcessor, FiscalizeClosedRace _fiscalizeClosedRace, ILogger<JobProcessingHandler> logger)
{
    internal async Task CheckSteps(CancellationToken stoppingToken)
    {

        await foreach(var job in _communicationSingleton.ReadJobsAsync(stoppingToken))
        {
            if(job.JobType == ProcessingJobType.RaceFinished)
                {
                    await _ticketProcessor.Pro(job);
                    await _fiscalizeClosedRace.Process(job);
                }
            else if(job.JobType == ProcessingJobType.OtheJobs)
                Console.WriteLine("nepoznat posao");
        }
        
    }

} */