namespace UtrkePasa.Api.Dtos;

public class TicketPurchaseResponse : Result
{
    public int UserId{get; set;}
    public int RaceId{get;set;}
    public int TicketId{get;set;}
    public DateTimeOffset PlacedAt{get;set;}
    public decimal PaidForTicket{get;set;}

}
