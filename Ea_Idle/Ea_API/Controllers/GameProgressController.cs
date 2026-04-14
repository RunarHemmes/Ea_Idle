using Microsoft.AspNetCore.Mvc;
using Ea_API.Models;

namespace Ea_API.Controllers
{
    public class GameProgressController : ControllerBase
    {
        //private readonly [DBContext] _context;

        public GameProgressController()
        {
            //_context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameProgress>>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
