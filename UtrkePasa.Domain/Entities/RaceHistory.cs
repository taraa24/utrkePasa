using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UtrkePasa.Domain.Entities;

public class RaceHistory
{

    [Key]
    public int history_Race_Id{get;set;}

    public int dog_Id{get;set;}
    [ForeignKey(nameof(dog_Id))]
    public Dog? Dog{get;set;}

    public int race_Id{get;set;}
    [ForeignKey(nameof(race_Id))]
    public Race? Race{get;set;}

    public int finale_Position {get;set;}
    public bool is_Winner{get;set;}
}