using Microsoft.AspNetCore.Http.HttpResults;

namespace Ea_API.Interfaces
{
    public interface IConnectionService
    {
        public StatusCodeHttpResult SetTimeLimit(int parentId, int hour, int min, int sec);

        public StatusCodeHttpResult GetConnection(int accountId);
    }
}
