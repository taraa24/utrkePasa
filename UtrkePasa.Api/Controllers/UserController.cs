using Microsoft.AspNetCore.Mvc;
using UtrkePasa.Api.Services;
using UtrkePasa.Api.Dtos;

namespace UtrkePasa.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
 
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
 
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.email) || string.IsNullOrWhiteSpace(request.password))
        {
            return BadRequest("Email i lozinka moraju biti upisani");
        }
 
        var result = await _userService.LoginAsync(request);
        if (result is null)
        {
            return Unauthorized("Pogrešan email ili lozinka");
        }
 
        return Ok(result);
    }
}
