using System.ComponentModel.DataAnnotations;

namespace UtrkePasa.Domain.Entities;
public class Race
{
    [Key]
    public int race_Id{get; set;}

    
    public string race_Name{get; set;} = string.Empty;
    public DateTime start_Of_The_Race{get; set;}
    public DateTime end_Of_The_Race{get; set;}
    public string? result_Of_Race{get; set;} //? = null

    public ICollection<Ticket> tickets {get; set;} = new List<Ticket>();
    public ICollection<RaceOdds> odds{get; set;} = new List<RaceOdds>();
    public ICollection<RaceHistory> raceHistory{get;set;} = new List<RaceHistory>();

}