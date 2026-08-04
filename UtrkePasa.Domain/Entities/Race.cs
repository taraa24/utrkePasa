namespace UtrkePasa.Domain.Entities;
public class Race
{
    public int RaceId{get; set;}
    public string RaceName{get; set;} = string.Empty;
    public DateTime StartOfTheRace{get; set;}
    public DateTime EndOfTheRace{get; set;}
    public string? ResultOfRace{get; set;} //? = null

    public ICollection<Tickets> Tickets {get; set;} = new List<Tickets>();
    public ICollection<RaceOdds> Odds{get; set;} = new List<RaceOdds>();
}