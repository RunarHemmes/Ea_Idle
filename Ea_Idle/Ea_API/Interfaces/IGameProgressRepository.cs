using Ea_API.Models;

namespace Ea_API.Interfaces
{
    public interface IGameProgressRepository
    {
        public List<GameProgress> GetAll();

        public int? GetHighestId();

        public GameProgress? GetById(int id);

        public GameProgress? GetByAccountId(int accountId);

        public GameProgress Add(GameProgress progress);

        public GameProgress? Update(GameProgress progress);

        public GameProgress? Delete(GameProgress progress);
    }
}
