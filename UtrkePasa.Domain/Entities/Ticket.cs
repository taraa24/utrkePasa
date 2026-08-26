using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace UtrkePasa.Domain.Entities;
public class Ticket
{
    [Key]
    [Column("ticket_Id")]
    public int TicketId { get; set; }

    [Column("placed_At")]
    public DateTimeOffset PlacedAt { get; set; }

    [Column("paid_For_Ticket")]
    public decimal PaidForTicket { get; set; }

    [Column("is_Winning_Ticket")]
    public bool? IsWinningTicket{get;set;}

    [Column("expected_Result")]
    public string ExpectedResult { get; set; } = string.Empty;

    [Column("odd_Type")]
    public string oddType{get;set;} = string.Empty;

    [Column("user_Id")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Column("race_Id")]
    [ForeignKey(nameof(Race))]
    public int RaceId { get; set; }
    public Race? Race { get; set; }

    [Column("race_Odds_Id")]
    [ForeignKey(nameof(RaceOdds))]
    public int RaceOddsId { get; set; }
    public RaceOdds? RaceOdds { get; set; }
}