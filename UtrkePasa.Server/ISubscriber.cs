namespace UtrkePasa.Server;

public interface ISubsriber
{
    Task recieveJob(JobProcessing job); 
}
