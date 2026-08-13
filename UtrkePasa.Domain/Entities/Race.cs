using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtrkePasa.Domain.Entities;
public class Race
{
    [Key]
    [Column("race_Id")]
    public int RaceId{get; set;}

    [Column("race_Name")]
    public string RaceName{get; set;} = string.Empty;

    [Column("start_Of_The_Race")]
    public DateTimeOffset StartOfTheRace{get; set;}

    [Column("start_Of_Betting")]
    public DateTimeOffset StartOfBetting{get;set;}
    
    [Column("end_Of_The_Race")]
    public DateTimeOffset EndOfTheRace{get; set;}

    [Column("race_Status")]
    public string RaceStatus { get; set; } = string.Empty;//Created, Open, inProgress,Finished,Canceled
    
    [Column("result_Of_Race")]
    public string? ResultOfRace{get; set;} //? = null

    [Column("dog_Starting_Position")]
    public List<string> DogStartingPosition{get;set;} = new List<string>();
    public ICollection<Ticket> tickets {get; set;} = new List<Ticket>();
    public ICollection<RaceOdds> odds{get; set;} = new List<RaceOdds>();
    public ICollection<RaceHistory> raceHistory{get;set;} = new List<RaceHistory>();

}