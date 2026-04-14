using Microsoft.AspNetCore.Mvc;
using Ea_API.Models;

namespace Ea_API.Controllers
{
    public class AccountController : ControllerBase
    {
        //private readonly [DBContext] _context;

        public AccountController()
        {
            //_context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
