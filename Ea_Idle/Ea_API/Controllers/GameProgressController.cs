using Microsoft.AspNetCore.Mvc;
using Ea_API.Models;
using Ea_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Ea_API.Controllers
{
    public class GameProgressController : ControllerBase
    {
        private readonly EaIdleDbContext _context;

        public GameProgressController(EaIdleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameProgress>>> GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
