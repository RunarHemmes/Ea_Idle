using Ea_API.Interfaces;
using Ea_API.Models;
using Ea_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ea_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GameProgressController : ControllerBase
    {
        private readonly IGameProgressService _progressService;
        private readonly ISecurityService _securityService;

        public GameProgressController(IGameProgressService service, ISecurityService securityService)
        {
            _progressService = service;
            _securityService = securityService;
        }

        [HttpGet("Get{accountId}")]
        public async Task<ActionResult<GameProgress>> Get(int accountId)
        {
            try
            {
                (bool succes, GameProgress? progress, string? errMsg) = _progressService.GetProgress(accountId);
                if (!succes)
                {
                    return BadRequest(new { errMsg = errMsg });
                }
                return Ok(progress);
            }
            catch
            {
                return StatusCode(500, "Something went wrong internally.");
            }
        }

        [HttpPut("Update{accountId}")]
        public async Task<ActionResult<GameProgress>> Update([FromBody] GameProgress gameProgress)
        {
            try
            {
                (bool validateSucces, string? validateMsg) = _securityService.ValidateProgressValues(gameProgress);
                if (!validateSucces)
                {
                    return BadRequest(new { errMsg = validateMsg });
                }
                (bool succes, GameProgress? progress, string? errMsg) = _progressService.UpdateProgress(gameProgress);
                if (!succes)
                {
                    return BadRequest(new { errMsg = errMsg });
                }
                return Ok(progress);
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
                (bool succes, GameProgress? progress, string? errMsg) = _progressService.NewProgress(accountId);
                if (!succes)
                {
                    return BadRequest(new { errMsg = errMsg });
                }
                return Ok(progress);
            }
            catch
            {
                return StatusCode(500, "Something went wrong internally.");
            }
        }
    }
}
