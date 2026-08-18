using UtrkePasa.Domain.Enum;

namespace UtrkePasa.Server;


public class JobProcessing
{
    public int ProcessingJobId { get; set; }

    public ProcessingJobType JobType { get; set; }

    public string JobStatus { get; set; } = "Pending";
    public int RaceId { get; set; }
    public int RaceHistoryId { get; set; }


}