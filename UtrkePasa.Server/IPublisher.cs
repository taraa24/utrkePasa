
namespace UtrkePasa.Server;

public interface IPublisher
{
    void AddSubscriber(ISubsriber subsriber);
    void RemoveSubscriber(ISubsriber subscriber);
    Task Publish(JobProcessing job);
} 
