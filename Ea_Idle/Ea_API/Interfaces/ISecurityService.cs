using Ea_API.Models;

namespace Ea_API.Interfaces
{
    public interface ISecurityService
    {
        public string GenerateToken(string username, string role);

        public (bool succes, string? message) ValidateLoginValues(LoginModel loginRequest);

        public (bool succes, string? message) ValidateRegisterValues(LoginModel registerRequest);

        public (bool succes, string? message) ValidateTimeLimitValues(int hour, int min, int sec);

    }
}
