namespace UtrkePasa.Server;

public class JobProcessingServer(IServiceScopeFactory scopeFactory,  JobProcessingHandler _jobProcessingHandler, ILogger<JobProcessingServer> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try 
            {
                await _jobProcessingHandler.CheckSteps();                
            }
            catch(Exception ex)
            {
                logger.LogError(ex,
                    "Greška u TicketProcessingu"
                );
            }
            finally
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}