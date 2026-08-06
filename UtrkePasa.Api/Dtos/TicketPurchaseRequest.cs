namespace UtrkePasa.Api.Dtos;

public class TicketPurchaseRequest
{
    public string race_Name{get;set;} = string.Empty;
    public string expected_Result{get;set;} = string.Empty;
    public float paid_For_Ticket{get; set;}
}