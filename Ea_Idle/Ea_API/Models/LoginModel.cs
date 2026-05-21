using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ea_API.Models
{
    [NotMapped]
    public class LoginModel
    {
        public string Username {  get; set; }

        public string? Email { get; set; }

        public string Role { get; set; }

        public string? Password { get; set; }

        public string? PassConfirm { get; set; }

        public int Id { get; set; }

        public int? ConnectionCode { get; set; }

        public LoginModel(string username, string role, int id, string? email = null,  string? password = null, string? passConfirm = null, int? code = null)
        {
            Username = username;
            Email = email;
            Role = role;
            Password = password;
            PassConfirm = passConfirm;
            Id = id;
            ConnectionCode = code;
        }
    }
}
