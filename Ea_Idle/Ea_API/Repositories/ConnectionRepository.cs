using Ea_API.Data;
using Ea_API.Interfaces;
using Ea_API.Models;

namespace Ea_API.Repositories
{
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly EaIdleDbContext _context;

        public ConnectionRepository(EaIdleDbContext context)
        {
            _context = context;
        }

        public Connection? GetByParent(int parentId)
        {
            try
            {
                Connection connect = _context.Connections.Single(c => c.ParentId == parentId);
                return connect;
            }
            catch
            {
                return null;
            }
        }

        public Connection? GetByChild(int childId)
        {
            try
            {
                Connection connect = _context.Connections.Single(c => c.ChildId == childId);
                return connect;
            }
            catch
            {
                return null;
            }
        }

        public Connection Add(Connection connection)
        {
            try
            {
                _context.Add(connection);
                _context.SaveChanges();
                return connection;
            }
            catch
            {
                throw new Exception("Something went wrong when adding a new GameProgress.");
            }
        }

        public Connection? Update(Connection connection)
        {
            try
            {
                _context.Update(connection);
                _context.SaveChanges();
                return connection;
            }
            catch
            {
                return null;
            }
        }

        public Connection? Delete(Connection connection)
        {
            try
            {
                _context.Remove(connection);
                _context.SaveChanges();
                return connection;
            }
            catch
            {
                return null;
            }
        }
    }
}
