using Ea_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ea_API.Interfaces
{
    public interface IGameProgressService
    {
        public (bool succes, GameProgress? progress, string? message) GetProgress(int accountId);

        public (bool succes, GameProgress? progress, string? message) UpdateProgress(GameProgress gameProgress);

        public (bool succes, GameProgress? progress, string? message) NewProgress(int accountId);
    }
}
