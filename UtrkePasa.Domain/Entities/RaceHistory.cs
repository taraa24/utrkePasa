using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtrkePasa.Domain.Entities;

public class RaceHistory
{

    [Key]
    [Column("history_Race_Id")]
    public int HistoryRaceId{get;set;}

    public int dog_Id{get;set;}
    [ForeignKey(nameof(dog_Id))]
    public Dog? Dog{get;set;}

    public int race_Id{get;set;}
    [ForeignKey(nameof(race_Id))]
    public Race? Race{get;set;}

    public int finale_Position {get;set;}
    public bool is_Winner{get;set;}
}