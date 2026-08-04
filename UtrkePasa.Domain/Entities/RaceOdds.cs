namespace UtrkePasa.Domain.Entities;
public class RaceOdds
{
    public int RaceOddsId{get; set;}
    public float Odds{get; set;}
    public string ExpectedResult{get; set;} = string.Empty;

    public int RaceId{get; set;}
    public Race? Race {get;set;} // = null!;


    
}