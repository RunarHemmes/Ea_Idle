using Microsoft.AspNetCore.Mvc;
using Ea_API.Models;
using Ea_API.Interfaces;

namespace Ea_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameProgressController : ControllerBase
    {
        private readonly IGameProgressRepository _repo;

        public GameProgressController(IGameProgressRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("Get{accountId}")]
        public async Task<ActionResult<GameProgress>> Get(int accountId)
        {
            try
            {
                GameProgress? progress = _repo.GetById(accountId);
                if (progress == null)
                {
                    return BadRequest("This acount doesn't have a save yet.");
                }
                return Ok(progress);
            }
            catch
            {
                return StatusCode(500, "Something went wrong internally.");
            }
        }

        [HttpPut("Update{accountId}")]
        public async Task<ActionResult<GameProgress>> Update(GameProgress gameProgress)
        {
            try
            {
                GameProgress? newProgress = _repo.Update(gameProgress);
                if (newProgress == null)
                {
                    return BadRequest("Update save failed.");
                }
                return Ok(newProgress);
            }
            catch
            {
                return StatusCode(500, "Something went wrong internally.");
            }
        }

        [HttpPost("NewSave{accountId}")]
        public async Task<ActionResult<GameProgress>> NewSave(int accountId)
        {
            try
            {
                GameProgress? oldProgress = _repo.GetByAccountId(accountId);
                if (oldProgress != null)
                {
                    return BadRequest("This account already has a save.");
                }
                int? highestId = _repo.GetHighestId();
                if (highestId == null)
                {
                    highestId = 0;
                }
                GameProgress newProgress = new GameProgress(highestId.Value + 1, accountId, "0");
                newProgress = _repo.Add(newProgress);
                return Ok(newProgress);
            }
            catch
            {
                return StatusCode(500, "Something went wrong internally.");
            }
        }
    }
}
