using System.Collections.Concurrent;
using System.Threading.Channels;

namespace UtrkePasa.Server;


public class CommunicationSingleton
{

    //private Guid _test = Guid.NewGuid(); 

    private readonly Channel<JobProcessing> _channel = Channel.CreateUnbounded<JobProcessing>();
    private ConcurrentQueue<JobProcessing> _queue = new ConcurrentQueue<JobProcessing>();
    public async Task AddJob(JobProcessing job)
    {

        await _channel.Writer.WriteAsync(job);
        //_queue.Enqueue(job);

    }

    public IAsyncEnumerable<JobProcessing> ReadJobsAsync(CancellationToken cancellationToken)
    {
        return _channel.Reader.ReadAllAsync(cancellationToken);
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