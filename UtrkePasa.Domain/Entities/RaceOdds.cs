using System.ComponentModel.DataAnnotations;

namespace UtrkePasa.Domain.Entities;
public class RaceOdds
{
    [Key]
    public int race_Odds_Id{get; set;}

    
    public float odds{get; set;}
    public string expected_Result{get; set;} = string.Empty;

    public int race_Id{get; set;}
    public Race? race {get;set;} // = null!;


    
}