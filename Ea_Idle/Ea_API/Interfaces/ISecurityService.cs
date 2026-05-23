using Ea_API.Models;

namespace Ea_API.Interfaces
{
    public interface ISecurityService
    {
        public (bool succes, string? message) ValidateLoginValues(LoginModel loginRequest);

        public (bool succes, string? message) ValidateRegisterValues(LoginModel registerRequest);

        public (bool succes, string? message) ValidateTimeLimitValues(int hour, int min, int sec);

        public (bool succes, string? message) ValidateProgressValues(GameProgress progress);

        public (bool succes, string? message) ValidateConnectionCode(int code);

        public int GenerateConnectionCode();
    }
}
