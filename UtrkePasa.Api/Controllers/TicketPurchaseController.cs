using Microsoft.AspNetCore.Mvc;
using UtrkePasa.Api.Dtos;
using UtrkePasa.Api.Services;

namespace UtrkePasa.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketPurchaseController : ControllerBase
{
    
    private readonly ITicketService _ticketService;

    public TicketPurchaseController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    /* public TicketPurchaseController(MyUserContext myUserContext)
    {
        _myUserContext = myUserContext;
    } */

    [HttpPost]
    public async Task PurchaseTIcket(TicketPurchaseRequest request)
    {
        /* await ValidateTIcket(o);
        await FiscalizeTicket(o);
        await SaveTicket(o); */
    }
}