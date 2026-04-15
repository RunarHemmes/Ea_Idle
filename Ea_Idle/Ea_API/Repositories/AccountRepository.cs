using Ea_API.Data;
using Ea_API.Interfaces;
using Ea_API.Models;

namespace Ea_API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly EaIdleDbContext _context;

        public AccountRepository(EaIdleDbContext context)
        {
            _context = context;
        }

        public List<Account> GetAll()
        {
            try
            {
                List<Account> result = _context.Accounts.ToList();
                return result;
            } catch
            {
                throw new Exception("GetAll exception");
            }
        }

        public int? GetHighestId()
        {
            try
            {
                int result = _context.Accounts.OrderBy(a => a.Id).First().Id;
                return result;
            } catch
            {
                return null;
            }
        }

        public Account? Get(int id)
        {
            try
            {
                Account result = _context.Accounts.Single(a => a.Id == id);
                return result;
            } catch
            {
                return null;
            }
        }

        public Account? GetByUsername(string username)
        {
            try
            {
                Account result = _context.Accounts.Single(a => a.Username == username);
                return result;
            } catch
            {
                return null;
            }
        }

        public Account? GetByEmail(string email)
        {
            try
            {
                Account result = _context.Accounts.Single(a => a.Email == email);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public Account Add(Account account)
        {
            try
            {
                _context.Add(account);
                _context.SaveChanges();
                return account;
            } catch
            {
                throw new Exception("Something went wrong when adding a new Account.");
            }
        }

        public Account? Update(Account account)
        {
            try
            {
                _context.Update(account);
                _context.SaveChanges();
                return account;
            } catch
            {
                return null;
            }
        }

        public Account? Delete(Account account)
        {
            try
            {
                _context.Remove(account);
                _context.SaveChanges();
                return account;
            } catch
            {
                return null;
            }
        }
    }
}
