using Microsoft.AspNetCore.Mvc;
using UtrkePasa.Api.Dtos;
using UtrkePasa.Api.Handler;
using UtrkePasa.Api.Services;

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
    [RequestLimit(5)]
    public async Task<ActionResult> PurchaseTicket([FromBody]TicketPurchaseRequest request)
    {
        //await Task.Delay(5000);
        var ticket = await _payingHandler.HandlePaymentAsync(request);
        return Ok(ticket);
    }
}