namespace Ea_API.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(string username, string role);

        public int GenerateConnectionCode();
    }
}
