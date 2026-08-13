using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtrkePasa.Domain.Entities;

public class Dog
{
    [Key]
    [Column("dog_Id")]
    public int DogId{get;set;}
    
    [Column("dog_Name")]
    public string dog_Name{get; set;} = string.Empty;
    public int race_Odds_Id{get; set;}
    [ForeignKey(nameof(race_Odds_Id))]
    public RaceOdds? race_Odds{get;set;}
    
    public ICollection<RaceHistory> raceHistory{get;set;} = new List<RaceHistory>();

}