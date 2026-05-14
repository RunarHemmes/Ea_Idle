using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ea_API.Models;

namespace Ea_API.Interfaces
{
    public interface IConnectionService
    {
        public Task<ActionResult<Connection>> SetTimeLimit(int parentId, int hour, int min, int sec);

        public Task<ActionResult<Connection>> GetConnection(int accountId);
    }
}
