using Ea_API.Interfaces;
using Ea_API.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Ea_API.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IConfiguration _config;
        public SecurityService(IConfiguration config)
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

        public (bool succes, string? message) ValidateLoginValues(LoginModel loginRequest)
        {
            string? username = loginRequest.Username;
            string? password = loginRequest.Password;
            if (password == null || username == null)
            {
                return (false, "Important information is missing!");
            }
            if (username.Length > 50)
            {
                return (false, "The username must be 50 characters or less.");
            }
            if (8 > password.Length || password.Length > 30)
            {
                return (false, "The password must be 8-30 characters.");
            }
            return (true, null);
        }

        public (bool succes, string? message) ValidateRegisterValues(LoginModel registerRequest)
        {
            string? username = registerRequest.Username;
            string? password = registerRequest.Password;
            string? confirm = registerRequest.PassConfirm;
            string? role = registerRequest.Role;
            string? email = registerRequest.Email;
            if (username == null || password == null || confirm == null || role == null || email == null)
            {
                return (false, "Important information is missing!");
            }
            if (username.Length > 50)
            {
                return (false, "The username must be 50 characters or less.");
            }
            if (8 > password.Length || password.Length > 30)
            {
                return (false, "The password must be 8-30 characters.");
            }
            if (password != confirm)
            {
                return (false, "The passwords are not the same.");
            }
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$";
            if (!Regex.IsMatch(email, pattern))
            {
                return (false, "The email is not valid.");
            }
            return (true, null);
        }

        public (bool succes, string? message) ValidateTimeLimitValues(int hour, int min, int sec)
        {
            if (hour > 23 || hour < 0)
            {
                return (false, "The hour must be within 0-23.");
            }
            if (min > 59 || sec > 59 || min < 0 || sec < 0)
            {
                return (false, "The minute and second must be within 0-59.");
            }
            return (true, null);
        }

        public (bool succes, string? message) ValidateProgressValues(GameProgress progress)
        {
            string sp = progress.SilverPennies;
            if (sp.Contains('-')) {
                return (false, "Silver Pennies amount can not be negative.");
            }
            return (true, null);
        }

    }
}
