using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace UtrkePasa.Tests;

public class Testing
{
    static TimeSpan TimeDataflowComputations(int maxDegreeOfParallelism, int messageCount)
    {
        var workerBlock = new ActionBlock<int>(millisecondsTimeout => Thread.Sleep(millisecondsTimeout), 
            new ExecutionDataflowBlockOptions
            {
             MaxDegreeOfParallelism = maxDegreeOfParallelism   
            });

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for(int i = 0; i < messageCount; i++)
        {
            workerBlock.Post(1000);
        }
        workerBlock.Complete();

        workerBlock.Completion.Wait();

        stopwatch.Stop();
        
        return stopwatch.Elapsed;
    }

    public static void Main(string[] args)
    {
        int processorCount = Environment.ProcessorCount;
        int messageCount = processorCount;

        Console.WriteLine($"processor count = {processorCount}");

        TimeSpan elapsed;

        elapsed = TimeDataflowComputations(1, messageCount);
        Console.WriteLine("Degree of parallelism = {0}; message count = {1}; " +
         "elapsed time = {2}ms.", 1, messageCount, (int)elapsed.TotalMilliseconds);

        elapsed = TimeDataflowComputations(processorCount, messageCount);
        Console.WriteLine("Degree of parallelism = {0}; message count = {1}; " +
         "elapsed time = {2}ms.", processorCount, messageCount, (int)elapsed.TotalMilliseconds);

    }
}
