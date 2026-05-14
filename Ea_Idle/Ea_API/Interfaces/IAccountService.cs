using Ea_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ea_API.Interfaces
{
    public interface IAccountService
    {
        public StatusCodeHttpResult Login(LoginModel loginRequest);

        public StatusCodeHttpResult Register(LoginModel registerRequest);
    }
}
