namespace UtrkePasa.Server;

public class JobProcessing(IServiceScopeFactory scopeFactory, ILogger<JobProcessing> logger) : BackgroundService
{

    private readonly TicketProcessing _ticketProcessing = new(scopeFactory, logger);
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try 
            {
                await _ticketProcessing.CheckSteps();                
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