using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ea_API.Models
{
    [NotMapped]
    public class LoginModel
    {
        public string Username { get; set; }

        public string Role { get; set; }

        public string? Password { get; set; }

        public int Id { get; set; }

        public LoginModel(string username, string role, string? password, int id)
        {
            Username = username;
            Role = role;
            Password = password;
            Id = id;
        }
    }
}
