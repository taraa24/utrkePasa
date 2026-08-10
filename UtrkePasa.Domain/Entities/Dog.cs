using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtrkePasa.Domain.Entities;

public class Dog
{
    [Key]
    public int dog_Id{get;set;}
    public string dog_Name{get; set;} = string.Empty;


    public int race_Id{get; set;}
    [ForeignKey(nameof(race_Id))]
    public Race? race {get; set;}

    public int race_Odds_Id{get; set;}
    [ForeignKey(nameof(race_Odds_Id))]
    public RaceOdds? race_Odds{get;set;}

}