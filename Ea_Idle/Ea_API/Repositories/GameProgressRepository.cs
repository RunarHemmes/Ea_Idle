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
            try
            {
                List<GameProgress> result = _context.GameProgresses.ToList();
                return result;
            }
            catch
            {
                throw new Exception("GetAll exception");
            }
        }

        public int? GetHighestId()
        {
            try
            {
                int result = _context.GameProgresses.OrderBy(g => g.Id).First().Id;
                return result;
            }
            catch
            {
                return null;
            }
        }

        public GameProgress? GetById(int id)
        {
            try
            {
                GameProgress result = _context.GameProgresses.Single(g => g.Id == id);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public GameProgress? GetByAccountId(int accountId)
        {
            try
            {
                GameProgress result = _context.GameProgresses.Single(g => g.AccountId == accountId);
                return result;
            }
            catch
            {
                return null;
            }
        }

        //public GameProgress? GetByEmail(string email)
        //{
        //    try
        //    {
        //        GameProgress result = _context.GameProgresses.Single(a => a.Email == email);
        //        return result;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

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
