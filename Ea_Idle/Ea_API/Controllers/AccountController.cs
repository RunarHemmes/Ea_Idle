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

namespace Ea_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        private readonly IConnectionRepository _connectRepo;
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;
        private readonly ISecurityService _securityService;

        public AccountController(IAccountService accountService, ISecurityService securityService, IAccountRepository account, IConnectionRepository connect, IConfiguration config)
        {
            _accountService = accountService;
            _securityService = securityService;
            _repo = account;
            _connectRepo = connect;
            _config = config;
        }

        //[AllowAnonymous]
        //[HttpPost("Login")]
        //public async Task<ActionResult<Account>> Login([FromBody] LoginModel loginModel)
        //{
        //    try
        //    {
        //        Account? userAccount = _repo.GetByUsername(loginModel.Username);

        //        if (userAccount != null)
        //        {
        //            if (userAccount.Password == loginModel.Password)
        //            {
        //                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])); //Warning can be ignored, this (should) always get a string, not null.
        //                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //                var token = new JwtSecurityToken(
        //                    issuer: _config["Jwt:Issuer"],
        //                    audience: _config["Jwt:Audience"],
        //                    claims: new[] { new Claim(ClaimTypes.Name, userAccount.Username, userAccount.Role) },
        //                    expires: DateTime.Now.AddHours(1),
        //                    signingCredentials: credentials
        //                );

        //                var t = new JwtSecurityTokenHandler().WriteToken(token);

        //                LoginModel user = new(userAccount.Username, userAccount.Role, userAccount.Id);

        //                return Ok(new { user = user, token = t });
        //            }
        //        }
        //        return BadRequest(new { errMsg = "The username or password is incorrect." });
        //    } catch
        //    {
        //        return StatusCode(500, "Something went wrong internally.");
        //    }
        //}

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
                (bool loginSucces, LoginModel? user, string? loginMsg) = _accountService.Login(loginRequest);
                if (!loginSucces)
                {
                    return BadRequest(new { errMsg = loginMsg });
                }
                string token = _securityService.GenerateToken(user.Username, user.Role);
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
                (bool regSucces, LoginModel? user, string? regMsg) = _accountService.Register(registerModel);
                if (!regSucces)
                {
                    return BadRequest(new { errMsg = regMsg });
                }
                return Ok(user);
            }
            catch
            {
                return StatusCode(500, new{ errMsg = "Something went wrong internally."});
            }
        }

        [HttpPatch("SetTimeLimit{parentId}-{hour}:{min}:{sec}")]
        public async Task<ActionResult<Connection>> SetTimeLimit(int parentId, int hour, int min, int sec)
        {
            try
            {
                Account? account = _repo.Get(parentId);
                if (account == null)
                {
                    return BadRequest(new { errMsg = "This Id doesn't belong to an account." });
                } else if (account.Role != "Parent")
                {
                    return BadRequest(new { errMsg = "This is not a parent account." });
                }

                Connection? connection = _connectRepo.GetByParent(parentId);
                if (connection != null)
                {
                    connection.TimeLimit = new(hour, min, sec);
                    Connection? newConnection = _connectRepo.Update(connection);
                    if (newConnection != null)
                    {
                        return Ok(newConnection);
                    }
                }
                return BadRequest(new { errMsg = "This parent account doesn't have a connection yet." });
            } catch
            {
                return StatusCode(500, new { errMsg = "Something went wrong internally." });
            }
        }

        [HttpGet("GetConnect{accountId}")]
        public async Task<ActionResult<Connection>> GetConnect(int accountId)
        {
            try
            {
                Account? account = _repo.Get(accountId);
                if (account == null)
                {
                    return BadRequest(new { errMsg = "This Id doesn't belong to an account."});
                }
                Connection? connection;
                if (account.Role == "Parent")
                {
                    connection = _connectRepo.GetByParent(accountId);
                }
                else
                {
                    connection = _connectRepo.GetByChild(accountId);
                }
                if (connection != null)
                {
                    Account? parent = _repo.Get(connection.ParentId);
                    Account? child = _repo.Get(connection.ChildId);
                    if (parent != null && child != null)
                    {
                        return Ok(new
                        {
                            parentId = connection.ParentId,
                            parentName = parent.Username,
                            childId = connection.ChildId,
                            childName = child.Username,
                            timeLimit = connection.TimeLimit
                        });
                    }
                }
                return BadRequest(new { errMsg = "This parent account doesn't have a connection yet." });
            } catch
            {
                return StatusCode(500, new { errMsg = "Something went wrong internally." });
            }
        }
     }
}
