using System.Threading.Channels;

namespace UtrkePasa.Server;

public class MessageBus : IPublisher
{
    private readonly Channel<JobProcessing> _channel = Channel.CreateUnbounded<JobProcessing>();

    private readonly List<ISubsriber> _subscribers = new();
    

    public MessageBus()
    {
        _ = ProcessQueue();
    }

    public void AddSubscriber(ISubsriber subsriber)
    {
        _subscribers.Add(subsriber);
    }
    public void RemoveSubscriber(ISubsriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }

    public async Task Publish(JobProcessing job)
    {
        await _channel.Writer.WriteAsync(job);
    }

    private async Task ProcessQueue()
    {
        await foreach (var job in _channel.Reader.ReadAllAsync())
        {
            await NotifySubscribers(job);
        }
    }

    private async Task NotifySubscribers(JobProcessing job)
    {
        var tasks = new List<Task>();
        foreach(var subscriber in _subscribers)
        {
            tasks.Add(subscriber.recieveJob(job));
        }

        await Task.WhenAll(tasks);
    }

}