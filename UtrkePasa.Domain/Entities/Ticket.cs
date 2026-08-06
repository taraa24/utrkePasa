using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace UtrkePasa.Domain.Entities;
public class Ticket
{   
    [Key]
    public int ticket_Id{get; set;}

    
    public DateTime placed_At{get; set;}
    public float paid_For_Ticket{get; set;}

    public int user_Id{get; set;}
    public User? user{get; set;}

    public int race_Id{get; set;}
    public Race? race{get; set;}

    public int race_Odds_Id{get; set;}
    public RaceOdds? race_Odds{get; set;}

}