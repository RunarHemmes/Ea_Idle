using Ea_API.Interfaces;
using Ea_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ea_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;
        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public (bool succes, LoginModel? account, string? message) Login(LoginModel loginRequest)
        {
            Account? userAccount = _repo.GetByUsername(loginRequest.Username);

            if (userAccount != null)
            {
                if (userAccount.Password == loginRequest.Password)
                {
                    LoginModel user = new(userAccount.Username, userAccount.Role, userAccount.Id);
                    return (true, user, null);
                }
            }
            return (false, null, "The username or password is incorrect.");
        }

        public (bool succes, LoginModel? account, string? message) Register(LoginModel registerRequest)
        {
            if (_repo.GetByUsername(registerRequest.Username) == null)
            {
                if (_repo.GetByEmail(registerRequest.Email) == null)
                {
                    int? highestId = _repo.GetHighestId();
                    if (!highestId.HasValue)
                    {
                        highestId = 0;
                    }
                    Account newAccount = new(highestId.Value + 1, registerRequest.Username, registerRequest.Password, registerRequest.Email, registerRequest.Role);
                    newAccount = _repo.Add(newAccount);
                    LoginModel registerReturn = new(newAccount.Username, newAccount.Role, newAccount.Id);
                    return (true, registerReturn, null);
                }
                else
                {
                    return (false, null, "This email is already linked to an account.");
                }
            }
            else
            {
                return (false, null, "This username is already taken.");
            }
        }
    }
}
