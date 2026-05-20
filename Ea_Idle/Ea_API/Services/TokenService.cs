using Ea_API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ea_API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string username, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])); //Warning can be ignored, this (should) always get a string, not null.
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: new[] { new Claim(ClaimTypes.Name, username, role) },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );
            var t = new JwtSecurityTokenHandler().WriteToken(token);
            return t;
        }

        public int GenerateConnectionCode()
        {
            Random random = new Random();
            string strCode = "";
            for (int i = 0; i < 6; i++)
            {
                strCode += random.Next(10).ToString();
            }
            int code = int.Parse(strCode);
            return code;
        }
    }
}
