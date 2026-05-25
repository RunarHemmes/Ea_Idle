using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ea_API.Models;

namespace Ea_API.Interfaces
{
    public interface IConnectionService
    {
        public (bool succes, Connection? account, string? message) SetTimeLimit(int parentId, int hour, int min, int sec);

        public (bool succes, Account? parent, Account? child, TimeOnly? timeLimit, string? message) GetConnection(int accountId);

        public (bool succes, Connection? connection, string? message) SetConnect(int accountId, int connectCode);
    }
}
