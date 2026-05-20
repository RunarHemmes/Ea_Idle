using Ea_API.Interfaces;
using Ea_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Connection = Ea_API.Models.Connection;

namespace Ea_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ISecurityService _securityService;
        private readonly IConnectionService _connectService;
        private readonly ITokenService _tokenService;

        public AccountController(IConnectionService connectService, IAccountService accountService, ISecurityService securityService, ITokenService tokenService)
        {
            _accountService = accountService;
            _securityService = securityService;
            _connectService = connectService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginModel>> Login([FromBody] LoginModel loginRequest)
        {
            try
            {
                (bool validateSucces, string? validateMsg) = _securityService.ValidateLoginValues(loginRequest);
                if (!validateSucces)
                {
                    return BadRequest(new { errMsg = validateMsg });
                }
                (bool succes, LoginModel? user, string? errMsg) = _accountService.Login(loginRequest);
                if (!succes)
                {
                    return BadRequest(new { errMsg = errMsg });
                }
                string token = _tokenService.GenerateToken(user.Username, user.Role);
                return Ok(new { user = user, token = token });
            } catch
            {
                return StatusCode(500, "Something went wrong with the API, please try again, or come back later.");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<Account>> Register([FromBody] LoginModel registerModel)
        {
            try
            {
                (bool validateSucces, string? validateMsg) = _securityService.ValidateRegisterValues(registerModel);
                if (!validateSucces)
                {
                    return BadRequest(new { errMsg = validateMsg });
                }
                (bool succes, LoginModel? user, string? errMsg) = _accountService.Register(registerModel);
                if (!succes)
                {
                    return BadRequest(new { errMsg = errMsg });
                }
                return Ok(user);
            }
            catch
            {
                return StatusCode(500, new{ errMsg = "Something went wrong with the API, please try again, or come back later." });
            }
        }

        [HttpPatch("SetTimeLimit{parentId}-{hour}:{min}:{sec}")]
        public async Task<ActionResult<Connection>> SetTimeLimit(int parentId, int hour, int min, int sec)
        {
            try
            {
                (bool validateSucces, string? validateMsg) = _securityService.ValidateTimeLimitValues(hour, min, sec);
                if (!validateSucces)
                {
                    return BadRequest(new { errMsg = validateMsg });
                }
                (bool succes, Connection? connect, string? errMsg) = _connectService.SetTimeLimit(parentId, hour, min, sec);
                if (!succes)
                {
                    return BadRequest(new { errMsg = errMsg });
                }
                return Ok(connect);
            }
            catch
            {
                return StatusCode(500, new { errMsg = "Something went wrong with the API, please try again, or come back later." });
            }
        }

        [HttpGet("GetConnect{accountId}")]
        public async Task<ActionResult<Connection>> GetConnect(int accountId)
        {
            try
            {
                (bool succes, Account? parent, Account? child, TimeOnly? timeLimit, string? errMsg) = _connectService.GetConnection(accountId);
                if (!succes)
                {
                    return BadRequest(new { errMsg = errMsg });
                }
                return Ok(new
                {
                    parentId = parent.Id,
                    parentName = parent.Username,
                    childId = child.Id,
                    childName = child.Username,
                    timeLimit = timeLimit
                });
            }
            catch
            {
                return StatusCode(500, new { errMsg = "Something went wrong with the API, please try again, or come back later." });
            }
        }

        [HttpPost("SetConnect{accountId}")]
        public async Task<ActionResult<Connection>> SetConnect([FromBody] int connectCode, int accountId)
        {
            try
            {
                (bool validateSucces, string? validateMsg) = _securityService.ValidateConnectionCode(connectCode);
                if (!validateSucces)
                {
                    return BadRequest(new { errMsg = validateMsg });
                }
                (bool succes, Connection? connect, string? errMsg) = _connectService.SetConnect(accountId, connectCode);
                if (!succes)
                {
                    return BadRequest(new { errMsg = errMsg });
                }
                return Ok(connect);
            }
            catch
            {
                return StatusCode(500, new { errMsg = "Something went wrong with the API, please try again, or come back later." });
            }
        }
    }
}
