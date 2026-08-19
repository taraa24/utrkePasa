using UtrkePasa.Domain.Enum;

namespace UtrkePasa.Server;

public class JobProcessingHandler(IServiceScopeFactory scopeFactory, CommunicationSingleton _communicationSingleton, TicketProcessor _ticketProcessor, ILogger<JobProcessingHandler> logger)
{

    internal async Task CheckSteps()
    {
        var job = _communicationSingleton.FetchJob();

        if(job == null) return;

        logger.LogInformation("posa:{JobId} tipa {JobType} u utrci {RaceId}", job.ProcessingJobId, job.JobType, job.RaceId);

        switch (job.JobType)
        {
            case ProcessingJobType.ProcessTicket:
                await _ticketProcessor.Process(job);
                break;

            case ProcessingJobType.OtheJobs:
                Console.WriteLine("nepoznat posao");
                break;
        }
        
    }

}