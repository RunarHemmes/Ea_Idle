using Ea_API.Models;

namespace Ea_API.Interfaces
{
    public interface IConnectionRepository
    {
        public Connection? GetByParent(int parentId);

        public Connection? GetByChild(int childId);

        public Connection Add(Connection connection);

        public Connection? Update(Connection connection);

        public Connection? Delete(Connection connection);


    }
}
