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
            List<Account> result = _context.Accounts.ToList();
            return result;
        }

        public Account? Get(int id)
        {
            Account result = _context.Accounts.Single(a => a.Id == id);
            return result;
        }

        public Account Add(Account account)
        {
            _context.Add(account);
            _context.SaveChanges();
            return account;
        }

        public Account? Update(Account account)
        {
            _context.Update(account);
            _context.SaveChanges();
            return account;
        }

        public Account? Delete(Account account)
        {
            _context.Remove(account);
            _context.SaveChanges();
            return account;
        }
    }
}
