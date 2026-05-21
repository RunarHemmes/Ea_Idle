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
            if (connection == null)
            {
                return (false, null, null, null, "This account doesn't have a connection yet.");
            }
            if (connection.ChildStatus == "Pending" || connection.ParentStatus == "Pending")
            {
                return (false, null, null, null, "The connection for this account is still pending.");
            }
            Account? parent = _accountRepo.Get(connection.ParentId);
            Account? child = _accountRepo.Get(connection.ChildId);
            if (parent != null && child != null) 
            {
                return (true, parent, child, connection.TimeLimit, null);
            }
            return (false, null, null, null, "Either the parent or child account doesn't exist.");
        }

        public (bool succes, Connection? connection, string? message) SetConnect(int accountId, int connectCode)
        {
            Account? account = _accountRepo.Get(accountId);
            if (account == null)
            {
                return (false, null, "This Id doesn't belong to an account.");
            }
            Connection? existingConnect;
            if (account.Role == "Parent")
            {
                existingConnect = _connectRepo.GetByParent(accountId);
            } else
            {
                existingConnect = _connectRepo.GetByChild(accountId);
            }
            Account? otherAccount = _accountRepo.GetByConnectionCode(connectCode);
            if (otherAccount == null)
            {
                return (false, null, "There is no account with this code.");
            }
            if (otherAccount.Role == account.Role)
            {
                return (false, null, "The account you're trying to connect with has the same role as you.");
            }
            if (existingConnect == null)
            {
                Connection newConnection = new();
                if (account.Role == "Parent")
                {
                    newConnection.ParentId = account.Id;
                    newConnection.ParentStatus = "Connected";
                    newConnection.ChildId = otherAccount.Id;
                    newConnection.ChildStatus = "Pending";
                } else
                {
                    newConnection.ChildId = account.Id;
                    newConnection.ChildStatus = "Connected";
                    newConnection.ParentId = otherAccount.Id;
                    newConnection.ParentStatus = "Pending";
                }
                Connection succesfull = _connectRepo.Add(newConnection);
                return (true, succesfull, null);
            } else
            {
                if (account.Role == "Parent")
                {
                    if (existingConnect.ChildId != otherAccount.Id)
                    {
                        return (false, null, "You already have a connection with a different account!");
                    }
                    existingConnect.ParentStatus = "Connected";
                } else
                {
                    if (existingConnect.ParentId != otherAccount.Id)
                    {
                        return (false, null, "You already have a connection with a different account!");
                    }
                    existingConnect.ChildStatus = "Connected";
                }
                Connection? succesfull = _connectRepo.Update(existingConnect);
                if (succesfull == null)
                {
                    return (false, null, "Update connection failed.");
                }
                return (true, succesfull, null);
            }

        }
    }
}
