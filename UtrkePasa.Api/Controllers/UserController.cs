using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using UtrkePasa.Infrastructure;

namespace UtrkePasa.Api.Controllers;

public class UserController 
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
    }

}