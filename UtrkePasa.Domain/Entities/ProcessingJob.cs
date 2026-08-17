namespace UtrkePasa.Domain.Entities;


public class ProcessingJob
{
    public int ProcessingJobId{get;set;}

    public string JobName{get;set;} = string.Empty;
    public string JobStatus{get;set;} = "Pending"; // pending Inprogress completed fail
}