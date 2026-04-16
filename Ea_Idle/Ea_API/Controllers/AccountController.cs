using Ea_API.Interfaces;
using Ea_API.Models;
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
        private readonly IConfiguration _config;

        public AccountController(IAccountRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Account>> Login(string username, string password)
        {
            try
            {
                Account? userAccount = _repo.GetByUsername(username);

                if (userAccount != null)
                {
                    if (userAccount.Password == password)
                    {
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])); //Warning can be ignored, this (should) always get a string, not null.
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _config["Jwt:Issuer"],
                            audience: _config["Jwt:Audience"],
                            claims: new[] { new Claim(ClaimTypes.Name, username) },
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: credentials
                        );

                        var t = new JwtSecurityTokenHandler().WriteToken(token);

                        return Ok(new { User = userAccount, Token = t });
                    }
                }
                return BadRequest("The username or password is incorrect.");
            } catch
            {
                return StatusCode(500, "Something went wrong internally.");
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Account>> Register(string username, string email, string password)
        {
            try
            {
                if (_repo.GetByUsername(username) == null)
                {
                    if (_repo.GetByEmail(email) == null)
                    {
                        int? highestId = _repo.GetHighestId();
                        if (!highestId.HasValue)
                        {
                            highestId = 0;
                        }
                        Account newAccount = new(highestId.Value + 1, username, password, email);
                        newAccount = _repo.Add(newAccount);
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
    }
}
