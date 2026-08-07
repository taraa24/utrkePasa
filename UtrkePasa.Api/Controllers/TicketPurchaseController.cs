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

    /* public TicketPurchaseController(MyUserContext myUserContext)
    {
        _myUserContext = myUserContext;
    } */

    [HttpPost]
    public async Task<ActionResult> PurchaseTIcket([FromBody]TicketPurchaseRequest request)
    {
        //await payinHandler.Purchase(request);
        /* await ValidateTIcket(o);
        await FiscalizeTicket(o);
        await SaveTicket(o); */
        /* var result = await _ticketService.PurchaseTicketAsync(request);
        return Ok(result); */

        var ticket = await _payingHandler.HandlePaymentAsync(request);
        return Ok(ticket);
    }
}