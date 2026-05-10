using Ea_API.Interfaces;
using Ea_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
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
        private readonly IAccountRepository _accountRepo;
        private readonly IConnectionRepository _connectRepo;
        private readonly IConfiguration _config;

        public AccountController(IAccountRepository account, IConnectionRepository connect, IConfiguration config)
        {
            _accountRepo = account;
            _connectRepo = connect;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<Account>> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                Account? userAccount = _accountRepo.GetByUsername(loginModel.Username);

                if (userAccount != null)
                {
                    if (userAccount.Password == loginModel.Password)
                    {
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])); //Warning can be ignored, this (should) always get a string, not null.
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _config["Jwt:Issuer"],
                            audience: _config["Jwt:Audience"],
                            claims: new[] { new Claim(ClaimTypes.Name, userAccount.Username, userAccount.Role) },
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: credentials
                        );

                        var t = new JwtSecurityTokenHandler().WriteToken(token);

                        LoginModel user = new(userAccount.Username, userAccount.Role, null, userAccount.Id);

                        return Ok(new { user = user, token = t });
                    }
                }
                return BadRequest(new { errMsg = "The username or password is incorrect." });
            } catch
            {
                return StatusCode(500, "Something went wrong internally.");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<Account>> Register(string username, string role, string email, string password)
        {
            try
            {
                if (_accountRepo.GetByUsername(username) == null)
                {
                    if (_accountRepo.GetByEmail(email) == null)
                    {
                        int? highestId = _accountRepo.GetHighestId();
                        if (!highestId.HasValue)
                        {
                            highestId = 0;
                        }
                        Account newAccount = new(highestId.Value + 1, role, username, password, email);
                        newAccount = _accountRepo.Add(newAccount);
                        return Ok(newAccount);
                    }
                    else
                    {
                        return BadRequest("This email is already linked to an account.");
                    }
                }
                else
                {
                    return BadRequest("This username is already taken.");
                }
            } catch
            {
                return StatusCode(500, "Something went wrong internally.");
            }
        }

        [HttpPatch("SetTimeLimit{parentId}-{hour}:{min}:{sec}")]
        public async Task<ActionResult> SetTimeLimit(int parentId, int hour, int min, int sec)
        {
            try
            {
                Account? account = _accountRepo.Get(parentId);
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
                Account? account = _accountRepo.Get(accountId);
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
                    Account? parent = _accountRepo.Get(connection.ParentId);
                    Account? child = _accountRepo.Get(connection.ChildId);
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
