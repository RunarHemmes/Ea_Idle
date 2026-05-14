using Ea_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ea_API.Interfaces
{
    public interface IGameProgressService
    {
        public StatusCodeHttpResult GetProgress(int accountId);

        public StatusCodeHttpResult UpdateProgress(GameProgress gameProgress);

        public StatusCodeHttpResult NewProgress(int accountId);
    }
}
