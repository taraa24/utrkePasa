using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UtrkePasa.Api.Dtos;

public class TicketPurchaseRequest
{
    public int UserId{get;set;}
    public DateTime PLacedAt{get;set;}
    //public string expected_Result{get;set;} = string.Empty;
    public int? RaceId{get;set;}
    public int RaceOddsId{get;set;}
    public decimal PaidForTicket{get; set;}
}