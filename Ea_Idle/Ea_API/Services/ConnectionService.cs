using Ea_API.Interfaces;
using Ea_API.Models;

namespace Ea_API.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConnectionRepository _connectRepo;
        private readonly IAccountRepository _accountRepo;

        public ConnectionService(IConnectionRepository connect, IAccountRepository account)
        {
            _connectRepo = connect;
            _accountRepo = account;
        }

        public (bool succes, Connection? account, string? message) SetTimeLimit(int parentId, int hour, int min, int sec)
        {
            Account? account = _accountRepo.Get(parentId);
            if (account == null)
            {
                return (false, null, "This Id doesn't belong to an account.");
            }
            else if (account.Role != "Parent")
            {
                return (false, null, "This is not a parent account.");
            }

            Connection? connection = _connectRepo.GetByParent(parentId);
            if (connection != null)
            {
                connection.TimeLimit = new(hour, min, sec);
                Connection? newConnection = _connectRepo.Update(connection);
                if (newConnection != null)
                {
                    return (true, newConnection, null);
                }
            }
            return (false, null, "This parent account doesn't have a connection yet.");
        }

        public (bool succes, Account? parent, Account? child, TimeOnly? timeLimit, string? message) GetConnection(int accountId)
        {
            Account? account = _accountRepo.Get(accountId);
            if (account == null)
            {
                return (false, null, null, null, "This Id doesn't belong to an account.");
            }
            Connection? connection;
            if (account.Role == "Parent")
            {
                connection = _connectRepo.GetByParent(accountId);
            }
            else
            {
                connection = _connectRepo.GetByChild(accountId);
            }
            if (connection != null)
            {
                Account? parent = _accountRepo.Get(connection.ParentId);
                Account? child = _accountRepo.Get(connection.ChildId);
                if (parent != null && child != null)
                {
                    return (true, parent, child, connection.TimeLimit, null);
                }
            }
            return (false, null, null, null, "This account doesn't have a connection yet.");
        }
    }
}
