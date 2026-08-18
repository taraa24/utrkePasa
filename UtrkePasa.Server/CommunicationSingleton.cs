using System.Collections.Concurrent;

namespace UtrkePasa.Server;


public class CommunicationSingleton
{

    //private Guid _test = Guid.NewGuid(); 
    private ConcurrentQueue<JobProcessing> _queue = new ConcurrentQueue<JobProcessing>();
    public void AddJob(JobProcessing job)
    {
        _queue.Enqueue(job);

    }

    public JobProcessing? FetchJob()
    {
        JobProcessing? job = null;

       if(_queue.TryDequeue(out job))
        {
            return job;
        }
        return null;
    }
}