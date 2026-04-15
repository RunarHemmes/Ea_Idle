using Ea_API.Models;
using Ea_API.Interfaces;
using Ea_API.Data;

namespace Ea_API.Repositories
{
    public class GameProgressRepository : IGameProgressRepository
    {
        private readonly EaIdleDbContext _context;

        public GameProgressRepository(EaIdleDbContext context)
        {
            _context = context;
        }

        public List<GameProgress> GetAll()
        {
            List<GameProgress> result = _context.GameProgresses.ToList();
            return result;
        }

        public GameProgress? Get(int id)
        {
            GameProgress result = _context.GameProgresses.Single(g => g.Id == id);
            return result;
        }

        public GameProgress Add(GameProgress progress)
        {
            _context.Add(progress);
            _context.SaveChanges();
            return progress;
        }

        public GameProgress? Update(GameProgress progress)
        {
            _context.Update(progress);
            _context.SaveChanges();
            return progress;
        }

        public GameProgress? Delete(GameProgress progress)
        {
            _context.Remove(progress);
            _context.SaveChanges();
            return progress;
        }

    }
}
