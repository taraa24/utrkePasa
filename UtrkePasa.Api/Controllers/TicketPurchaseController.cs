using Microsoft.AspNetCore.Mvc;
using UtrkePasa.Api.Dtos;
using UtrkePasa.Api.Handler;

namespace UtrkePasa.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketPurchaseController : ControllerBase
{
    
    private readonly IPayingHandler _payingHandler;

    public TicketPurchaseController(IPayingHandler payingHandler)
    {
        _payingHandler = payingHandler;
    }

    [HttpPost]
    public async Task<ActionResult> PurchaseTicket([FromBody]TicketPurchaseRequest request)
    {
        var ticket = await _payingHandler.HandlePaymentAsync(request);
        return Ok(ticket);
    }
}