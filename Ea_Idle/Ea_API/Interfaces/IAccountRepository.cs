using Ea_API.Models;

namespace Ea_API.Interfaces
{
    public interface IAccountRepository
    {
        public List<Account> GetAll();

        public Account? Get(int id);

        public Account Add(Account account);

        public Account? Update(Account account);

        public Account? Delete(Account account);
    }
}
