using Ea_API.Models;

namespace Ea_API.Interfaces
{
    public interface IAccountRepository
    {
        public List<Account> GetAll();

        public int? GetHighestId();

        public Account? Get(int id);

        public Account? GetByUsername(string username);

        public Account? GetByEmail(string email);

        public Account Add(Account account);

        public Account? Update(Account account);

        public Account? Delete(Account account);
    }
}
