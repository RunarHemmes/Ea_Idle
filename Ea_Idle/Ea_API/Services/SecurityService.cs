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
        public (bool succes, string? message) ValidateLoginValues(LoginModel loginRequest)
        {
            string? username = loginRequest.Username;
            string? password = loginRequest.Password;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
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
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(confirm) || string.IsNullOrWhiteSpace(role))
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
            Dictionary<string, Dictionary<string, float>> mu = progress.MiningUpgrades;
            if (string.IsNullOrWhiteSpace(sp))
            {
                return (false, "Important information is missing!");
            }
            if (sp.Contains('-'))
            {
                return (false, "Silver Pennies amount can not be negative.");
            }
            Dictionary<string, int[]> checks = new Dictionary<string, int[]> {
                { "Equipment", new int[]{0, 1} },
                { "Miners_Count", new int[]{0, 1} },
                { "Ore_Price", new int[]{0, 1} },
                { "Ore_Purity", new int[]{0, 1} },
                { "Lvl", new int[]{0, 4} },
                { "Price", new int[]{0, 4} },
                { "Current_Bonus", new int[]{0, 4} },
                { "Bonus_mult", new int[]{0, 2} },
                { "Bonus_add", new int[]{0, 2} } };

            foreach ((string key1, Dictionary<string, float> upgrade) in mu)
            {
                checks[key1][0]++;
                foreach ((string key2, float value) in upgrade)
                {
                    checks[key2][0]++;
                }
            }
            foreach (int[] values in checks.Values)
            {
                if (values[0] != values[1])
                {
                    return (false, "The upgrades were given in an incorrect format");
                }
            }
            return (true, null);
        }

        public (bool succes, string? message) ValidateConnectionCode(int code)
        {
            if (code.ToString().Length != 6)
            {
                return (false, "The connection code should be 6 digits.");
            }
            return (true, null);
        }
         
        public int GenerateConnectionCode()
        {
            Random random = new Random();
            string strCode = "";
            for (int i = 0; i < 6; i++)
            {
                strCode += random.Next(10).ToString();
            }
            while (strCode[0] == '0')
            {
                string part = strCode.Substring(1);
                strCode = random.Next(10).ToString() + part;
            }
            int code = int.Parse(strCode);
            return code;
        }
    }
}
