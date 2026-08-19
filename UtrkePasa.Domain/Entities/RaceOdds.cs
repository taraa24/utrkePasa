using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtrkePasa.Domain.Entities;
public class RaceOdds
{
    [Key]
    [Column("race_Odds_Id")]
    public int RaceOddsId{get; set;}

    [Column("odd")]
    public float Odd{get; set;}

    [Column("odd_Type")]
    public string oddType{get;set;} = string.Empty;

    [Column("race_Id")]
    [ForeignKey(nameof(Race))]
    public int RaceId{get; set;}
    public Race? Race {get;set;} // = null!;
    
}