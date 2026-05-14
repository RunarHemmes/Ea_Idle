using Ea_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ea_API.Interfaces
{
    public interface IGameProgressService
    {
        public Task<ActionResult<GameProgress>> GetProgress(int accountId);

        public Task<ActionResult<GameProgress>> UpdateProgress(GameProgress gameProgress);

        public Task<ActionResult<GameProgress>> NewProgress(int accountId);
    }
}
