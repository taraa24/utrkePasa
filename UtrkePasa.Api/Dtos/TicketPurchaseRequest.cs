
namespace UtrkePasa.Api.Dtos;

public class TicketPurchaseRequest
{
    public int UserId{get;set;}
    public DateTimeOffset PlacedAt{get;set;}
    public string ExpectedResult{get;set;} = string.Empty;
    public string oddType{get;set;} = string.Empty;
    public int? RaceId{get;set;}
    public decimal PaidForTicket{get; set;}
}