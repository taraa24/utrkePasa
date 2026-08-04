using System.Numerics;

namespace UtrkePasa.Domain.Entities;
public class Ticket
{
    public int TicketId{get; set;}
    public DateTime PlacedAt{get; set;}
    public float PaidForTicket{get; set;}

    public int UserId{get; set;}
    public User? User{get; set;}

    public int RaceId{get; set;}
    public Race? Race{get; set;}

    public int RaceOddsId{get; set;}
    public RaceOdds? RaceOdds{get; set;}

}