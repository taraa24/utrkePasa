using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtrkePasa.Domain.Entities;

public class ProcessingTicket
{
    [Key]
    [Column("processing_Ticket_Id")]
    public int ProcessingTicketId{get;set;}

    [Column("processing_Ticket_Status")]
    public string ProcessingTicketStatus{get;set;} = string.Empty;

    [Column("race_Id")]
    [ForeignKey(nameof(Race))]
    public int RaceId{get;set;}
    public Race? Race{get;set;}

    [Column("race_History_Id")]
    [ForeignKey(nameof(RaceHistory))]
    public int RaceHistoryId{get;set;}
    public RaceHistory? RaceHistory{get;set;}


}