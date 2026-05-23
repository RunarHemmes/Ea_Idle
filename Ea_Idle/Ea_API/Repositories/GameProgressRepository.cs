using Ea_API.Models;
using Ea_API.Interfaces;
using Ea_API.Data;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                List<GameProgress> result = _context.GameProgresses.AsNoTracking().ToList();
                return result;
            }
            catch
            {
                throw new Exception("GetAll exception");
            }
        }

        public GameProgress? GetByAccountId(int accountId)
        {
            try
            {
                GameProgress result = _context.GameProgresses.AsNoTracking().Single(g => g.AccountId == accountId);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public GameProgress Add(GameProgress progress)
        {
            try
            {
                _context.Add(progress);
                _context.SaveChanges();
                return progress;
            }
            catch
            {
                throw new Exception("Something went wrong when adding a new GameProgress.");
            }
        }

        public GameProgress? Update(GameProgress progress)
        {
            try
            {
                _context.Update(progress);
                _context.SaveChanges();
                return progress;
            }
            catch
            {
                return null;
            }
        }

        public GameProgress? Delete(GameProgress progress)
        {
            try
            {
                _context.Remove(progress);
                _context.SaveChanges();
                return progress;
            }
            catch
            {
                return null;
            }
        }

    }
}
