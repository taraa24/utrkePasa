using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace UtrkePasa.Domain.Entities;
public class Ticket
{
    [Key]
    public int ticket_Id { get; set; }

    public DateTime placed_At { get; set; }
    public decimal paid_For_Ticket { get; set; }

    public int user_Id { get; set; }
    [ForeignKey(nameof(user_Id))]
    public User? user { get; set; }

    public int race_Id { get; set; }
    [ForeignKey(nameof(race_Id))]
    public Race? race { get; set; }

    public int race_Odds_Id { get; set; }
    [ForeignKey(nameof(race_Odds_Id))]
    public RaceOdds? race_Odds { get; set; }
}