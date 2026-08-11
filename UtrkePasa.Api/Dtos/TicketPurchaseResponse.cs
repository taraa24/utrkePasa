namespace UtrkePasa.Api.Dtos;

public class TicketPurchaseResponse
{
    public int UserId{get; set;}
    public int RaceId{get;set;}
    public int TicketId{get;set;}
    public DateTime PlacedAt{get;set;}
    public decimal PaidForTicket{get;set;}

}