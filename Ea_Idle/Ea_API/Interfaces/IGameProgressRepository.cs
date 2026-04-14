using Ea_API.Models;

namespace Ea_API.Interfaces
{
    public interface IGameProgressRepository
    {
        public List<GameProgress> GetAll();

        public GameProgress? Get(int id);

        public GameProgress Add(GameProgress progress);

        public GameProgress? Update(GameProgress progress);

        public GameProgress? Delete(GameProgress progress);
    }
}
