using Ea_API.Data;
using Ea_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ea_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly EaIdleDbContext _context;

        public AccountController(EaIdleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAll()
        {
            List<Account> result = await _context.Accounts.ToListAsync();
            return Ok(result);
        }
    }
}
