using Ea_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ea_API.Interfaces
{
    public interface IAccountService
    {
        public (bool succes, LoginModel? account, string? message) Login(LoginModel loginRequest);

        public (bool succes, LoginModel? account, string? message) Register(LoginModel registerRequest);
    }
}
